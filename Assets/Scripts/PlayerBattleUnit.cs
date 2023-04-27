using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleUnit : MonoBehaviour
{
    public UnitBase unitBase;
    private Animator animator;

    private float timer = 0f;
    private float delay = 1f;

    private bool isDealDamage = false;

    public Unit Unit { get; set; }

    void Start(){
        animator = GetComponent<Animator>();
    }
    
    public void SetUpPlayerUnit()
    {
        Unit = new Unit(unitBase);

        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = Unit.Base.SpriteImage;
    }
}
