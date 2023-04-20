using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public static int enemy;
    void OnCollisionEnter2D(Collision2D other) {
        ManageScenes sceneManager = GameObject.FindObjectOfType(typeof(ManageScenes)) as ManageScenes;

        if(other.gameObject.tag == "Portal") {
            sceneManager.TransitionToNextLevel();
        }

        if(other.gameObject.tag == "Octocat") {
            sceneManager.TransitionToBattle();
            enemy = 0;
        }

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
