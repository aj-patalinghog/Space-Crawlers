using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text characterName;
    public Slider healthBar;

    public void SetHUD(Unit unit) {
        characterName.text = unit.Base.Name;
        healthBar.maxValue = unit.Base.MaxHP;
        healthBar.value = unit.HP;
    }

    public void SetHP(int hp) {
        healthBar.value = hp;
    }
}
