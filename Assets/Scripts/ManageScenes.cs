using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public GameObject canvasPrefab;
    public float time;
    private Animator animator;
    public static int newScene = -1;

    public void TransitionToBattle() {
        newScene = 1;
        animator.Play("TransitionLeave");
    }

    public void TransitionToLevel() {
        newScene = 2;
        animator.Play("TransitionLeave");
    }

    void Start() {
        Instantiate(canvasPrefab);
        animator = FindObjectOfType<Canvas>().GetComponent<Animator>();
    }

    void Update(){
        if(newScene == -1) return;
        time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("TransitionLeave") && time > 1f) {
            SceneManager.LoadScene(newScene, LoadSceneMode.Single);
            newScene = -1;
        }
    }
}
