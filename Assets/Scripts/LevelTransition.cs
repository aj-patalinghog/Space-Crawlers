using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelTransition : MonoBehaviour
{
    public string playerTag = "Player";
    public SceneAsset newScene;
    public float time;
    public GameObject canvasPrefab;
    private Animator animator;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(playerTag)) {
            animator = FindObjectOfType<Canvas>().GetComponent<Animator>();
            animator.Play("TransitionLeave");
        }
    }

    void Start() {
        Instantiate(canvasPrefab);
    }

    void Update(){
        if(animator == null) return;
        time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("TransitionLeave") && time > 1f) {
            SceneManager.LoadScene(newScene.name, LoadSceneMode.Single);
        }
    }
}
