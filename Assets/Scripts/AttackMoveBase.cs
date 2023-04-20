using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Unit/Create new move")]

public class AttackMoveBase : ScriptableObject
{
    [SerializeField] string name;

    [SerializeField] int damage;
    [SerializeField] int energyCost;

    public string Name {
        get { return name; }
    }

    public int Damage {
        get { return damage; }
    }

    public int EnergyCost {
        get { return energyCost; }
    }

}


