using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text characterName;
    public Slider healthBar;

    public void SetHUD(Unit unit) {
        characterName.text = unit.unitName;
        healthBar.maxValue = unit.maxHP;
        healthBar.value = unit.currentHP;
    }

    public void SetHP(int hp) {
        healthBar.value = hp;
    }
}
