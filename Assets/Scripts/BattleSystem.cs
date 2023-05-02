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
    PlayerBattleUnit playerUnit;
    EnemyBattleUnit enemyUnit;
    List<AttackMove> playerMoves;
    List<AttackMove> enemyMoves;
    List<AttackMoveBase> playerBaseMoves;
    BattleHUD playerHUD;
    BattleHUD enemyHUD;
    public static EnemyDefeated enemy;
    AttackMoveBase newMove;
    public List<UnitBase> enemies;
    public static bool isPlanetAttack;
    public static bool isEarthAttack;

    void Start() {
        Cursor.lockState = CursorLockMode.None;

        enemy = (EnemyDefeated)(PlayerCollisions.enemy + 1);

        playerUnit = GameObject.Find("Player Unit").GetComponent<PlayerBattleUnit>();
        playerUnit.SetUpPlayerUnit();
        enemyUnit = GameObject.Find("Enemy Unit").GetComponent<EnemyBattleUnit>();
        enemyUnit.SelectAnimation(enemy);
        enemyUnit.unitBase = enemies[PlayerCollisions.enemy];
        enemyUnit.SetUpEnemyUnit();

        newMove = playerUnit.Unit.GetLearnableMove(playerUnit.Unit.Base.LearnableMoves);
        playerBaseMoves = playerUnit.Unit.GetBaseMove(playerUnit.Unit.Base.LearnableMoves);
        playerMoves = playerUnit.Unit.Moves;
        enemyMoves = enemyUnit.Unit.Moves;

        playerHUD = GameObject.Find("Player HUD").GetComponent<BattleHUD>();
        playerHUD.SetHUD(playerUnit.Unit);
        enemyHUD = GameObject.Find("Enemy HUD").GetComponent<BattleHUD>();
        enemyHUD.SetHUD(enemyUnit.Unit);

        energyText = playerHUD.transform.GetChild(2).gameObject.GetComponent<Text>();
        UpdateEnergy(playerUnit.Unit.Base.Energy);

        buttons.SetActive(false);
        for(int i = 0; i < buttons.transform.childCount; i++) {
            button.Add(buttons.transform.GetChild(i).gameObject);
            buttonText.Add(button[i].transform.GetChild(0).gameObject.GetComponent<Text>());
        }
        button[4].SetActive(false);

        dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();
        dialogueText.text = string.Format("An alien {0} appeared...", enemyUnit.Unit.Base.Name);

        nextBattleState = PlayerTurn;
    }

    public void OnClick(int index) {
        if(nextBattleState == PlayerTurn) {
            nextBattleState = ChooseMove;
            switch(index) {
                case 0: ChooseMove(); break;
                case 1: Heal();       break;
                case 3: Run();        break;
            }
        } else if(nextBattleState == ChooseMove) {
            Attack(index, true);
        } else if(nextBattleState == ReplaceMove && index < 4) {
            buttons.SetActive(false);
            dialogueText.text = string.Format("You replaced {0} with {1}!", playerMoves[index].Base.Name, newMove.Name);
            playerUnit.Unit.Base.AddMove(newMove, index);
            nextBattleState = EndBattle;
        } else if(nextBattleState == ReplaceMove && index == 4) {
            buttons.SetActive(false);
            dialogueText.text = string.Format("You choose to not take the new ability: {0}", newMove.Name);
            nextBattleState = EndBattle;
        }
    }

    public void OnPointer(int index) {
        if(nextBattleState == ChooseMove) {
            if(index < 4){
                dialogueText.text = index == -1 ? "" : string.Format("Damage: {0}\nEnergy Cost: {1}",
                    playerMoves[index].Base.Damage, playerMoves[index].Base.EnergyCost);
            } else{
                dialogueText.text = string.Format("Damage: 0\nEnergy Cost: 0");
            }
        } else if(nextBattleState == ReplaceMove) {
            dialogueText.text = index == -1 ? dialogueText.text = string.Format("Click a move to\nreplace with\n{0}", newMove.Name) : index <4 ? 
                string.Format("Damage: {0}\nEnergy Cost: {1}", playerMoves[index].Base.Damage, playerMoves[index].Base.EnergyCost) : 
                string.Format("Damage: {0}\nEnergy Cost: {1}", newMove.Damage, newMove.EnergyCost);
        }
    }

    void OnMouseUp() {
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
        button[4].SetActive(false);

        for(int i = 0; i < 4; i++) {
            button[i].GetComponent<Button>().interactable = true;
        }

        buttonText[0].text = "Attack";
        buttonText[1].text = "Heal";
        buttonText[3].text = "Run";

        if(energy < playerUnit.Unit.Base.Energy) UpdateEnergy(energy + 1);

        dialogueText.text = "Choose an action: ";
    }

    void ChooseMove() {
        button[2].SetActive(true);
        for(int i = 0; i < 4; i++) {
            buttonText[i].text = playerMoves[i].Base.Name;
            button[i].GetComponent<Button>().interactable = playerMoves[i].Base.EnergyCost > energy ? false : true;
        }
        if(!(button[0].GetComponent<Button>().interactable) && !(button[1].GetComponent<Button>().interactable) && !(button[2].GetComponent<Button>().interactable) && !(button[3].GetComponent<Button>().interactable)){
            button[4].SetActive(true);
            buttonText[4].text = "Rest";
        }
    }

    void Attack(int index, bool isPlayer) {
        buttons.SetActive(false);
        if(isPlayer){
            if(index == 4){
                dialogueText.text = string.Format("You are too exhausted to do anything! Wait to recover energy.");
                nextBattleState = EnemyTurn;
            } else{
                int attack = playerMoves[index].Base.Damage;
                string move = playerMoves[index].Base.Name;

                UpdateEnergy(energy - playerMoves[index].Base.EnergyCost);

                bool isDead = enemyUnit.Unit.TakeDamage(attack);
                enemyHUD.SetHP(enemyUnit.Unit.HP);

                dialogueText.text = string.Format("You used {0} and dealt {1} damage to {2}!", move, attack, enemyUnit.Unit.Base.Name);
                nextBattleState = (isDead) ? PlayerWon : EnemyTurn;
                if(nextBattleState == PlayerWon){
                    enemyUnit.EnemyDead(enemy);
                } 
                enemyUnit.EnemyTakeDamage(enemy);
                playerUnit.PlayerDealDamage();
                if(nextBattleState == PlayerWon) enemyHUD.gameObject.SetActive(false);
            }
        } else{
            int attack = enemyMoves[index].Base.Damage;
            string move = enemyMoves[index].Base.Name;
            if(enemyMoves[index].Base.Name == "Planet Throw") isPlanetAttack = true;
            if(enemyMoves[index].Base.Name == "Earthquake") isEarthAttack = true;

            bool isDead = playerUnit.Unit.TakeDamage(attack);
            playerHUD.SetHP(playerUnit.Unit.HP);

            dialogueText.text = string.Format("{0} used {1}. You take {2} damage!", enemyUnit.Unit.Base.Name, move, attack);
            nextBattleState = (isDead) ? PlayerLost : PlayerTurn;
            playerUnit.PlayerTakeDamage();
            enemyUnit.EnemyDealDamage(enemy);
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
        playerUnit.Unit.Base.AddStats();
        if (UnitBase.enemiesDefeated == 2) dialogueText.text = string.Format("{0} died. You won the battle! You gained 2 health and increased energy by 1.\nMax Health: {1}, Max Energy: {2}", enemyUnit.Unit.Base.Name, playerUnit.Unit.Base.MaxHP, playerUnit.Unit.Base.Energy);
        else dialogueText.text = string.Format("{0} died. You won the battle! You gained 2 health.\nMax Health: {1}, Max Energy: {2}", enemyUnit.Unit.Base.Name, playerUnit.Unit.Base.MaxHP, playerUnit.Unit.Base.Energy);
        PlayerCollisions.isCollided = false;
        nextBattleState = !AlreadyLearned() ? FindNewMove : EndBattle;
    }

    void PlayerLost() {
        dialogueText.text = string.Format("You died...");
        PlayerCollisions.isCollided = false;
        playerUnit.Unit.Base.ResetPlayer(playerBaseMoves);
        nextBattleState = EndBattle;
    }

    void EndBattle() {
        ManageScenes sceneManager = GameObject.FindObjectOfType(typeof(ManageScenes)) as ManageScenes;
        StartCoroutine(sceneManager.UnloadScene("Battle"));
    }

    // Learnable Move Mechanics

    void FindNewMove() {
        dialogueText.text = string.Format("After winning the battle, your suit has found one of {0}'s abilites to copy: {1}.", enemyUnit.Unit.Base.Name, newMove.Name);
        nextBattleState = ReplaceMove;
    }

    void ReplaceMove() {
        dialogueText.text = string.Format("Click a move to\nreplace with\n{0}", newMove.Name);
        buttons.SetActive(true);
        button[4].SetActive(true);
        for(int i = 0; i < 4; i++) {
            buttonText[i].text = playerMoves[i].Base.Name;
            button[i].GetComponent<Button>().interactable = true;
        }
        buttonText[4].text = newMove.Name;
    }

    bool AlreadyLearned() {
        foreach(AttackMove move in playerMoves) {
            if(move.Base.Name == newMove.Name) return true;
        }
        return false;
    }
}
