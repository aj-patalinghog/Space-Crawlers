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
    GameObject attackButtons;
    public Image playerPrefab;
    public Image enemyPrefab;
    Unit playerUnit;
    Unit enemyUnit;
    BattleHUD playerHUD;
    BattleHUD enemyHUD;

    void Start() {
        Cursor.lockState = CursorLockMode.None;

        Image player = Instantiate(playerPrefab, GameObject.Find("Player Station").transform);
        Image enemy = Instantiate(enemyPrefab, GameObject.Find("Enemy Station").transform);

        playerUnit = player.GetComponent<Unit>();
        enemyUnit = enemy.GetComponent<Unit>();

        playerHUD = GameObject.Find("Player HUD").GetComponent<BattleHUD>();
        enemyHUD = GameObject.Find("Enemy HUD").GetComponent<BattleHUD>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        attackButtons = GameObject.Find("Action Buttons");
        attackButtons.SetActive(false);

        dialogueText = GameObject.Find("Dialogue Text").GetComponent<Text>();
        dialogueText.text = string.Format("An alien {0} appeared...", enemyUnit.unitName);

        nextBattleState = PlayerTurn;
    }

    void OnMouseDown() {
        if(!attackButtons.activeSelf) {
            nextBattleState();
        }
    }

    void PlayerTurn() {
        attackButtons.SetActive(true);

        dialogueText.text = "Choose an action: ";
    }

    public void Attack() {
        attackButtons.SetActive(false);

        bool isDead = enemyUnit.TakeDamage(playerUnit.attack);
        enemyHUD.SetHP(enemyUnit.currentHP);

        dialogueText.text = string.Format("You dealt {0} damage to {1}!", playerUnit.attack, enemyUnit.unitName);
        nextBattleState = (isDead) ? PlayerWon : EnemyTurn;
    }

    public void Heal() {
        attackButtons.SetActive(false);

        playerUnit.Heal(playerUnit.maxHP/5);
        playerHUD.SetHP(playerUnit.currentHP);

        dialogueText.text = string.Format("You healed {0} HP!", playerUnit.maxHP/5);
        nextBattleState = EnemyTurn;
    }

    public void Run() {
        attackButtons.SetActive(false);

        if(UnityEngine.Random.value >= 0.5) {
            dialogueText.text = string.Format("You got away safely!");
            nextBattleState = EndBattle;
        } else {
            dialogueText.text = string.Format("You failed to run away!");
            nextBattleState = EnemyTurn;
        }
    }

    void EnemyTurn() {
        bool isDead = playerUnit.TakeDamage(enemyUnit.attack);
        playerHUD.SetHP(playerUnit.currentHP);

        dialogueText.text = string.Format("{0} attacks. You take {1} damage!", enemyUnit.unitName, enemyUnit.attack);
        nextBattleState = (isDead) ? PlayerLost : PlayerTurn;
    }

    void PlayerWon() {
        dialogueText.text = string.Format("{0} died. You won the battle!", enemyUnit.unitName);
        nextBattleState = EndBattle;
    }

    void PlayerLost() {
        dialogueText.text = string.Format("You died...");
        nextBattleState = EndBattle;
    }

    void EndBattle() {
        ManageScenes sceneManager = GameObject.FindObjectOfType(typeof(ManageScenes)) as ManageScenes;
        sceneManager.TransitionToNextLevel();
    }
}
