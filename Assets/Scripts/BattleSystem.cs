using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleSystem : MonoBehaviour
{
    public Action nextBattleState;
    Text dialogueText;
    Text energyText;
    public int energy;
    public GameObject buttons;
    public List<GameObject> button;
    public List<Text> buttonText;
    BattleUnit playerUnit;
    BattleUnit enemyUnit;
    List<AttackMove> playerMoves;
    List<AttackMove> enemyMoves;
    BattleHUD playerHUD;
    BattleHUD enemyHUD;
    public static EnemyDefeated enemyDefeated;
    AttackMoveBase newMove;
    EnemyDefeated enemy;

    void Start() {
        Cursor.lockState = CursorLockMode.None;

        playerUnit = GameObject.Find("Player Unit").GetComponent<BattleUnit>();
        playerUnit.SetUpUnit();
        enemyUnit = GameObject.Find("Enemy Unit").GetComponent<BattleUnit>();
        enemyUnit.SetUpUnit();

        playerMoves = playerUnit.Unit.Moves;
        enemyMoves = enemyUnit.Unit.Moves;

        playerHUD = GameObject.Find("Player HUD").GetComponent<BattleHUD>();
        playerHUD.SetHUD(playerUnit.Unit);
        enemyHUD = GameObject.Find("Enemy HUD").GetComponent<BattleHUD>();
        enemyHUD.SetHUD(enemyUnit.Unit);

        energyText = playerHUD.transform.GetChild(2).gameObject.GetComponent<Text>();
        UpdateEnergy(3);

        buttons.SetActive(false);
        for(int i = 0; i < buttons.transform.childCount; i++) {
            button.Add(buttons.transform.GetChild(i).gameObject);
            buttonText.Add(button[i].transform.GetChild(0).gameObject.GetComponent<Text>());
        }
        button[4].SetActive(false);

        enemyDefeated = FindEnemy();
        newMove = playerUnit.Unit.GetLearnableMove().Base;

        dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();
        dialogueText.text = string.Format("An alien {0} appeared...", enemyUnit.Unit.Base.Name);

        nextBattleState = PlayerTurn;
    }

    public void OnClick(int index) {
        if(nextBattleState == PlayerTurn) {
            switch(index) {
                case 0: ChooseMove(); break;
                case 1: Heal();       break;
                case 3: Run();        break;
            }
            nextBattleState = ChooseMove;
        } else if(nextBattleState == ChooseMove) {
            Attack(index, true);
        } else if(nextBattleState == ReplaceMove && index < 4) {
            buttons.SetActive(false);
            dialogueText.text = string.Format("You replaced {0} with {1}!", playerMoves[index].Base.Name, newMove.Name);
            playerUnit.Unit.Base.AddMove(newMove, index);
            nextBattleState = EndBattle;
        }
    }

    public void OnPointer(int index) {
        if(nextBattleState == ChooseMove) {
            dialogueText.text = index == -1 ? "" : string.Format("Damage: {0}\nEnergy Cost: {1}",
                playerMoves[index].Base.Damage, playerMoves[index].Base.EnergyCost);
        } else if(nextBattleState == ReplaceMove) {
            dialogueText.text = index == -1 ? dialogueText.text = string.Format("Click a move to\nreplace with {0}", newMove.Name) : index <4 ? 
                string.Format("Damage: {0}\nEnergy Cost: {1}", playerMoves[index].Base.Damage, playerMoves[index].Base.EnergyCost) : 
                string.Format("Damage: {0}\nEnergy Cost: {1}", newMove.Damage, newMove.EnergyCost);
        }
    }

    void OnMouseUp() {
        Debug.Log("Click");
        if(!buttons.activeSelf) {
            nextBattleState();
        }
    }

    void UpdateEnergy(int newEnergy) {
        energy = newEnergy;
        energyText.text = Convert.ToString(newEnergy);
    }

    // Battle Mechanics

    void PlayerTurn() {
        buttons.SetActive(true);
        button[2].SetActive(false);

        for(int i = 0; i < 4; i++) {
            button[i].GetComponent<Button>().interactable = true;
        }

        buttonText[0].text = "Attack";
        buttonText[1].text = "Heal";
        buttonText[3].text = "Run";

        if(energy < 10) UpdateEnergy(energy + 1);

        dialogueText.text = "Choose an action: ";
    }

    void ChooseMove() {
        button[2].SetActive(true);

        for(int i = 0; i < 4; i++) {
            buttonText[i].text = playerMoves[i].Base.Name;
            button[i].GetComponent<Button>().interactable = playerMoves[i].Base.EnergyCost > energy ? false : true;
        }
    }

    void Attack(int index, bool isPlayer) {
        buttons.SetActive(false);
        if(isPlayer){
            int attack = playerMoves[index].Base.Damage;
            string move = playerMoves[index].Base.Name;

            UpdateEnergy(energy - playerMoves[index].Base.EnergyCost);

            bool isDead = enemyUnit.Unit.TakeDamage(attack);
            enemyHUD.SetHP(enemyUnit.Unit.HP);

            dialogueText.text = string.Format("You used {0} and dealt {1} damage to {2}!", move, attack, enemyUnit.Unit.Base.Name);
            nextBattleState = (isDead) ? PlayerWon : EnemyTurn;
        } else{
            int attack = enemyMoves[index].Base.Damage;
            string move = enemyMoves[index].Base.Name;

            bool isDead = playerUnit.Unit.TakeDamage(attack);
            playerHUD.SetHP(playerUnit.Unit.HP);

            dialogueText.text = string.Format("{0} used {1}. You take {2} damage!", enemyUnit.Unit.Base.Name, move, attack);
            nextBattleState = (isDead) ? PlayerLost : PlayerTurn;
        }
    }

    void Heal() {
        buttons.SetActive(false);

        playerUnit.Unit.Heal(playerUnit.Unit.Base.MaxHP/5);
        playerHUD.SetHP(playerUnit.Unit.HP);

        dialogueText.text = string.Format("You healed {0} HP!", playerUnit.Unit.Base.MaxHP/5);
        nextBattleState = EnemyTurn;
    }

    void Run() {
        buttons.SetActive(false);

        if(UnityEngine.Random.value >= 0.5) {
            dialogueText.text = string.Format("You got away safely!");
            nextBattleState = EndBattle;
        } else {
            dialogueText.text = string.Format("You failed to run away!");
            nextBattleState = EnemyTurn;
        }
    }

    void EnemyTurn() {
        Attack(UnityEngine.Random.Range(0, 3), false);
    }

    void PlayerWon() {
        dialogueText.text = string.Format("{0} died. You won the battle!", enemyUnit.Unit.Base.Name);
        nextBattleState = !AlreadyLearned() && UnityEngine.Random.value >= 0.75 ? FindNewMove : EndBattle;
    }

    void PlayerLost() {
        dialogueText.text = string.Format("You died...");
        nextBattleState = EndBattle;
    }

    void EndBattle() {
        ManageScenes sceneManager = GameObject.FindObjectOfType(typeof(ManageScenes)) as ManageScenes;
        sceneManager.TransitionToNextLevel();
    }

    // Learnable Move Mechanics

    void FindNewMove() {
        dialogueText.text = string.Format("After winning the battle, your suit has found one of {0}'s abilites to copy: {1}.", enemyUnit.Unit.Base.Name, newMove.Name);
        nextBattleState = ReplaceMove;
    }

    void ReplaceMove() {
        dialogueText.text = string.Format("Click a move to\nreplace with {0}", newMove.Name);
        buttons.SetActive(true);
        button[4].SetActive(true);
        for(int i = 0; i < 4; i++) {
            buttonText[i].text = playerMoves[i].Base.Name;
        }
        buttonText[4].text = newMove.Name;
    }

    bool AlreadyLearned() {
        foreach(AttackMove move in playerMoves) {
            if(move.Base.Name == newMove.Name) return true;
        }
        return false;
    }

    EnemyDefeated FindEnemy() {
        switch(PlayerCollisions.enemy){
            case 0: enemy = EnemyDefeated.OCTOCAT;
                    break;
        }
        return enemy;
    }
}
