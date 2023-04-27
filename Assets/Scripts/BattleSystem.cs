using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleSystem : MonoBehaviour
{
    public Action nextBattleState;
    RectTransform dialogueBox;

    Text dialogueText;
    Text actionText;
    Text moveText;
    //Energy cost and damage text UI
    Text energyText;
    Text damageText;

    public Text[] attackText;
    public Button[] attackButtons;
    [SerializeField] Button[] moveButtons;

    //UI elements
    GameObject moveSelector;
    GameObject actionSelector;
    GameObject moveDetails;
    [SerializeField] MoveSelectionUI moveSelectionUI;

    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;

    [SerializeField] BattleHUD playerHUD;
    [SerializeField] BattleHUD enemyHUD;

    int attack;
    int enemyChoice;

    EnemyDefeated enemy;
    public static EnemyDefeated enemyDefeated;
    AttackMoveBase newMove;

    string move;

    bool isDead;
    bool isPlayer = true;

    void Start() {
        Cursor.lockState = CursorLockMode.None;

        //Spawn player and enemy on platforms
        SpawnUnits();

<<<<<<< Updated upstream
        //Setup HUD
        SetUpHUD();
        
        //Set up player fight screen menu
        SetUpMenu();
        
        //Setup player moveset
        SetUpMoveset(playerUnit.Unit.Moves);
=======
        playerUnit = GameObject.Find("Player Unit").GetComponent<BattleUnit>();
        playerUnit.SetUpUnit();
        enemyUnit = GameObject.Find("Enemy Unit").GetComponent<BattleUnit>();
        enemyUnit.unitBase = enemies[PlayerCollisions.enemy];
        enemyUnit.SetUpUnit();

        //Choose a random move from enemy unit
        newMove = playerUnit.Unit.GetLearnableMove(playerUnit.Unit.Base.LearnableMoves);
        playerMoves = playerUnit.Unit.Moves;
        enemyMoves = enemyUnit.Unit.Moves;

        playerHUD = GameObject.Find("Player HUD").GetComponent<BattleHUD>();
        playerHUD.SetHUD(playerUnit.Unit);
        enemyHUD = GameObject.Find("Enemy HUD").GetComponent<BattleHUD>();
        enemyHUD.SetHUD(enemyUnit.Unit);

        energyText = playerHUD.transform.GetChild(2).gameObject.GetComponent<Text>();
        UpdateEnergy(10);

        buttons.SetActive(false);
        for(int i = 0; i < buttons.transform.childCount; i++) {
            button.Add(buttons.transform.GetChild(i).gameObject);
            buttonText.Add(button[i].transform.GetChild(0).gameObject.GetComponent<Text>());
        }
        button[4].SetActive(false);

        dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();
        dialogueText.text = string.Format("An alien {0} appeared...", enemyUnit.Unit.Base.Name);
>>>>>>> Stashed changes

        nextBattleState = PlayerTurn;
    }

    void SpawnUnits(){
        playerUnit.SetUpUnit();
        enemyUnit.SetUpUnit();
    }

    void SetUpHUD(){
        playerHUD.SetHUD(playerUnit.Unit);
        enemyHUD.SetHUD(enemyUnit.Unit);
    }

    void SetUpMenu(){
        actionSelector = GameObject.Find("Action Buttons");
        actionSelector.SetActive(false);

        moveSelector = GameObject.Find("Move Selector");
        moveSelector.SetActive(false);

        moveDetails = GameObject.Find("Move Details");
        moveDetails.SetActive(false);

        dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();
        dialogueText.text = string.Format("An alien {0} appeared...", enemyUnit.Unit.Base.Name);

    }

    void SetUpMoveset(List<AttackMove> moves){
        //Create index for each attack button
        for (int i = 0; i < attackButtons.Length; i++){
            Button button = attackButtons[i];
            var index = i;
            button.onClick.AddListener(() => OnClick(index));
        }
        
        //Create variable to hold UI Text elements for attack buttons
        for(int i = 0; i < 3; i++){
            attackText[i].text = moves[i].Base.Name;
        }
    }

<<<<<<< Updated upstream
    //Clicking attack buttons
    public void OnClick(int index){
        isPlayer = true;
        actionSelector.SetActive(false);
        moveSelector.SetActive(false);
        dialogueText.enabled = true;
        Attack(playerUnit.Unit.Moves, index, isPlayer);
=======
    public void OnPointer(int index) {
        if(nextBattleState == ChooseMove) {
            dialogueText.text = index == -1 ? "" : string.Format("Damage: {0}\nEnergy Cost: {1}",
                playerMoves[index].Base.Damage, playerMoves[index].Base.EnergyCost);
        } else if(nextBattleState == ReplaceMove) {
            dialogueText.text = index == -1 ? dialogueText.text = string.Format("Click a move to\nreplace with\n{0}", newMove.Name) : index <4 ? 
                string.Format("Damage: {0}\nEnergy Cost: {1}", playerMoves[index].Base.Damage, playerMoves[index].Base.EnergyCost) : 
                string.Format("Damage: {0}\nEnergy Cost: {1}", newMove.Damage, newMove.EnergyCost);
        }
>>>>>>> Stashed changes
    }

    

    void Attack(List<AttackMove> moves, int index, bool isPlayer){
        attack = moves[index].Base.Damage;
        move = moves[index].Base.Name;
        if(isPlayer){
            isDead = enemyUnit.Unit.TakeDamage(attack);
            enemyHUD.SetHP(enemyUnit.Unit.HP);

            dialogueText.text = string.Format("You used {0} and dealt {1} damage to {2}!", move, attack, enemyUnit.Unit.Base.Name);
            nextBattleState = (isDead) ? PlayerWon : EnemyTurn;
        }
        else{
            isDead = playerUnit.Unit.TakeDamage(attack);
            playerHUD.SetHP(playerUnit.Unit.HP);

            dialogueText.text = string.Format("{0} used {1}. You take {2} damage!", enemyUnit.Unit.Base.Name, move, attack);
            nextBattleState = (isDead) ? PlayerLost : PlayerTurn;
        }
    }
    //On mouse click move dialgoue forward
    void OnMouseDown() {
        if(!actionSelector.activeSelf || !moveSelector.activeSelf) {
            nextBattleState();
        }
    }

    void PlayerTurn() {
        actionSelector.SetActive(true);

        dialogueText.text = "Choose an action: ";
    }

    void Fight() {
        actionSelector.SetActive(false);
        moveSelector.SetActive(true);
        dialogueText.enabled = false;
    }

    void EnemyTurn() {
        //Randomly Choose enemy move
        enemyChoice = UnityEngine.Random.Range(0,3);
        isPlayer = false;
        switch(enemyChoice){
            case 0:  Attack(enemyUnit.Unit.Moves, 0, isPlayer);
                break;
            case 1: Attack(enemyUnit.Unit.Moves, 1, isPlayer);
                break;
            case 2: Attack(enemyUnit.Unit.Moves, 2, isPlayer);
                break;    
            case 3: Attack(enemyUnit.Unit.Moves, 3, isPlayer);
                break;    
        }
    }

    public void Heal() {
        actionSelector.SetActive(false);
        moveSelector.SetActive(false);
        moveDetails.SetActive(false);
        dialogueText.enabled = true;

        playerUnit.Unit.Heal(playerUnit.Unit.Base.MaxHP/5);
        playerHUD.SetHP(playerUnit.Unit.HP);

        dialogueText.text = string.Format("You healed {0} HP!", playerUnit.Unit.Base.MaxHP/5);
        nextBattleState = EnemyTurn;
    }

    public void Run() {
        actionSelector.SetActive(false);

        if(UnityEngine.Random.value >= 0.5) {
            dialogueText.text = string.Format("You got away safely!");
            nextBattleState = EndBattle;
        } else {
            dialogueText.text = string.Format("You failed to run away!");
            nextBattleState = EnemyTurn;
        }
    }

    void PlayerWon() {
        dialogueText.text = string.Format("{0} died. You won the battle!", enemyUnit.Unit.Base.Name);
        nextBattleState = FindNewMove;
    }

    void FindNewMove(){
        //Check what enemy was defeated to find which moves can be learned
        enemyDefeated = FindEnemy();
        var learnableMove = playerUnit.Unit.GetLearnableMove();
        newMove = learnableMove.Base;
        dialogueText.text = string.Format("After winning the battle, your suit has found one of {0}'s abilites to copy: {1}.", enemyUnit.Unit.Base.Name, newMove.Name);
        nextBattleState = AddNewMove;
    }

    void AddNewMove(){
        dialogueText.text = string.Format("You can replace one of your current abilities with the new ability.");
        moveSelectionUI.gameObject.SetActive(true);
        moveSelectionUI.SetMoveData(playerUnit.Unit.Base.MovesBase, newMove); 
        SetUpMoveButtons(); 
    }

    void SetUpMoveButtons(){
        for (int i = 0; i < 5; i++){
            Button movebutton = moveButtons[i];
            var moveIndex = i;
            movebutton.onClick.AddListener(() => OnClickMove(moveIndex));
        }
    }

    void OnClickMove(int index){
        if (index < 4){
            playerUnit.Unit.Base.AddMove(newMove, index);
            nextBattleState = EndBattle;
        }
    }


    public EnemyDefeated FindEnemy(){
        switch(PlayerCollisions.enemy){
            case 0: enemy = EnemyDefeated.OCTOCAT;
                    break;
        }
        return enemy;
    }

    void PlayerLost() {
        dialogueText.text = string.Format("You died...");
        nextBattleState = EndBattle;
    }

    void EndBattle() {
        ManageScenes sceneManager = GameObject.FindObjectOfType(typeof(ManageScenes)) as ManageScenes;
        sceneManager.TransitionToNextLevel();
    }

<<<<<<< Updated upstream
=======
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
        }
        buttonText[4].text = newMove.Name;
    }

    bool AlreadyLearned() {
        foreach(AttackMove move in playerMoves) {
            if(move.Base.Name == newMove.Name) return true;
        }
        return false;
    }
>>>>>>> Stashed changes
}
