using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleUnit : MonoBehaviour
{
    public UnitBase unitBase;
    private Animator animator;

    public Unit Unit { get; set; }

    void Start(){
        animator = GetComponent<Animator>();
    }

    public void SelectAnimation(EnemyDefeated enemy){
        switch(enemy){
            case EnemyDefeated.OCTOCAT: animator.SetBool("OctoEnemy", true);
                                        break;
            case EnemyDefeated.WORM: animator.SetBool("WormEnemy", true);
                                     break;
        }
    }

    public void DeactivateAnimationState(){
        animator.SetBool("OctoEnemy", false);
        animator.SetBool("OctoDead", false);
        animator.SetBool("WormEnemy", false);
        animator.SetBool("WormDead", false);
    }

    public void EnemyDealDamage(EnemyDefeated enemy){
        switch(enemy){
            case EnemyDefeated.OCTOCAT: animator.SetTrigger("OctoAttack");
                                        break;
            case EnemyDefeated.WORM: animator.SetTrigger("WormAttack");
                                     break;
        }
    }

    public void EnemyTakeDamage(EnemyDefeated enemy){
        switch(enemy){
            case EnemyDefeated.OCTOCAT: animator.SetTrigger("OctoDamage");
                                        break;
            case EnemyDefeated.WORM: animator.SetTrigger("WormDamage");
                                     break;
        }
    }

    public void EnemyDead(EnemyDefeated enemy){
        switch(enemy){
            case EnemyDefeated.OCTOCAT: animator.SetBool("OctoDead", true);
                                        break;
            case EnemyDefeated.WORM: animator.SetBool("WormDead", true);
                                     break;
        }
    }

    public void SetUpEnemyUnit()
    {
        Unit = new Unit(unitBase);

        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = Unit.Base.SpriteImage;
    }
}
