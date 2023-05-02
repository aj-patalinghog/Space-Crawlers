using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleUnit : MonoBehaviour
{
    public UnitBase unitBase;
    private Animator animator;

    AudioSource audio;

    public Unit Unit { get; set; }

    void Start(){
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        animator.SetBool("PlayerDead", false);
    }

    public void PlayerDealDamage(){
        animator.SetTrigger("Attack");
    }

    public void PlayerTakeDamage(){
        animator.SetTrigger("TakeDamage");
    }

    public void PlayerDead(){
        animator.SetBool("PlayerDead", true);
    }

    public void AttackSound(){
        audio.Play();
    }
    
    public void SetUpPlayerUnit()
    {
        Unit = new Unit(unitBase);

        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = Unit.Base.SpriteImage;
    }
}
