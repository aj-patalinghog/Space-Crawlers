using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int attack;
    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int damage) {
        currentHP -= damage;
        if(currentHP < 0) currentHP = 0;
        return (currentHP == 0) ? true : false;
    }

    public void Heal(int heal) {
        currentHP += heal;
        if(currentHP > maxHP) currentHP = maxHP;
    }
}
