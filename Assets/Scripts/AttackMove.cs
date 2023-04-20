using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMove : MonoBehaviour
{
    public AttackMoveBase Base { get; set; }
    public int energyCost { get; set; }
    public string attackName;

    public AttackMove(AttackMoveBase pBase)
    {
        Base = pBase;
        energyCost = pBase.EnergyCost;
    }
}