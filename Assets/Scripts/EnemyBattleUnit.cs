using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleUnit : MonoBehaviour
{
    public UnitBase unitBase;
    private Animator animator;
    AudioSource audio;
    public GameObject planet;

    public AudioClip[] audioClips;

    public Unit Unit { get; set; }

    void Start(){
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void SelectAnimation(EnemyDefeated enemy){
        switch(enemy){
            case EnemyDefeated.OCTOCAT: animator.SetBool("OctoEnemy", true);
                                        break;
            case EnemyDefeated.WORM: animator.SetBool("WormEnemy", true);
                                     break;
            case EnemyDefeated.CRAB: animator.SetBool("CrabEnemy", true);
                                     break;
            case EnemyDefeated.CORAL: animator.SetBool("CoralEnemy", true);
                                     break;
            case EnemyDefeated.DRAGON: animator.SetBool("DragonEnemy", true);
                                     break;
        }
    }

    public void DeactivateAnimationState(){
        animator.SetBool("OctoEnemy", false);
        animator.SetBool("OctoDead", false);
        animator.SetBool("WormEnemy", false);
        animator.SetBool("WormDead", false);
        animator.SetBool("CrabEnemy", false);
        animator.SetBool("CrabDead", false);
        animator.SetBool("CoralEnemy", false);
        animator.SetBool("CoralDead", false);
        animator.SetBool("DragonEnemy", false);
        animator.SetBool("DragonDead", false);
    }

    public void EnemyDealDamage(EnemyDefeated enemy){
        switch(enemy){
            case EnemyDefeated.OCTOCAT: animator.SetTrigger("OctoAttack");
                                        break;
            case EnemyDefeated.WORM: animator.SetTrigger("WormAttack");
                                     break;
            case EnemyDefeated.CRAB: animator.SetTrigger("CrabAttack");
                                     break;
            case EnemyDefeated.CORAL: animator.SetTrigger("CoralAttack");
                                     break;
            case EnemyDefeated.DRAGON: if(BattleSystem.isPlanetAttack){
                                        animator.SetTrigger("DragonPlanetAttack");
                                        planet.SetActive(true);
                                        BattleSystem.isPlanetAttack = false;
                                       } else if(BattleSystem.isEarthAttack){
                                        animator.SetTrigger("DragonEarthAttack");
                                        BattleSystem.isEarthAttack = false;
                                       } else{
                                        animator.SetTrigger("DragonAttack");
                                       }
                                     break;                                                 
        }
    }

    public void EnemyTakeDamage(EnemyDefeated enemy){
        planet.SetActive(false);
        switch(enemy){
            case EnemyDefeated.OCTOCAT: animator.SetTrigger("OctoDamage");
                                        break;
            case EnemyDefeated.WORM: animator.SetTrigger("WormDamage");
                                     break;
            case EnemyDefeated.CRAB: animator.SetTrigger("CrabDamage");
                                     break;
            case EnemyDefeated.CORAL: animator.SetTrigger("CoralDamage");
                                     break;
            case EnemyDefeated.DRAGON: animator.SetTrigger("DragonDamage");
                                     break;                                                                           
        }
    }

    public void EnemyDead(EnemyDefeated enemy){
        switch(enemy){
            case EnemyDefeated.OCTOCAT: animator.SetBool("OctoDead", true);
                                        break;
            case EnemyDefeated.WORM: animator.SetBool("WormDead", true);
                                     break;
            case EnemyDefeated.CRAB: animator.SetBool("CrabDead", true);
                                     break;
            case EnemyDefeated.CORAL: animator.SetBool("CoralDead", true);
                                     break;
            case EnemyDefeated.DRAGON: animator.SetBool("DragonDead", true);
                                     break;

        }
    }

    public void OctoAttackSound(){
        audio.Play();
    }

    public void WormAttackSound(){
        audio.PlayOneShot(audioClips[0]);
    }

    public void CrabAttackSound(){
        audio.PlayOneShot(audioClips[1]);
    }

    public void CoralAttackSound(){
        audio.PlayOneShot(audioClips[2]);
    }

    public void DragonPlanetAttackSound(){
        audio.PlayOneShot(audioClips[3]);
    }

    public void DragonEarthAttackSound(){
        audio.PlayOneShot(audioClips[4]);
    }

    public void DragonAttackSound(){
        audio.PlayOneShot(audioClips[5]);
    }

    public void SetUpEnemyUnit()
    {
        Unit = new Unit(unitBase);

        GetComponent<Image>().enabled = true;
        GetComponent<Image>().sprite = Unit.Base.SpriteImage;
    }
}

