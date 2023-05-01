using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public static int enemy;
    public static bool isCollided = false;
    void OnCollisionEnter2D(Collision2D other) {
        ManageScenes sceneManager = GameObject.FindObjectOfType(typeof(ManageScenes)) as ManageScenes;
        if(!isCollided){
            if(other.gameObject.tag == "Octocat") {
                StartCoroutine(sceneManager.LoadScene("Battle"));
                StartCoroutine(DestroyEnemy(other.gameObject));
                enemy = 0;
                isCollided = true;
            }

            if(other.gameObject.tag == "Worm") {
                StartCoroutine(sceneManager.LoadScene("Battle"));
                StartCoroutine(DestroyEnemy(other.gameObject));
                enemy = 1;
                isCollided = true;
            }

            if(other.gameObject.tag == "Crab") {
                StartCoroutine(sceneManager.LoadScene("Battle"));
                StartCoroutine(DestroyEnemy(other.gameObject));
                enemy = 2;
                isCollided = true;
            }

            if(other.gameObject.tag == "Coral") {
                StartCoroutine(sceneManager.LoadScene("Battle"));
                StartCoroutine(DestroyEnemy(other.gameObject));
                enemy = 3;
                isCollided = true;
            }

            if(other.gameObject.tag == "Dragon") {
                StartCoroutine(sceneManager.LoadScene("Battle"));
                StartCoroutine(DestroyEnemy(other.gameObject));
                enemy = 4;
                isCollided = true;
            }
        }
    }

    IEnumerator DestroyEnemy(GameObject enemy) {
        yield return new WaitForSeconds(2f);
        Destroy(enemy.gameObject);
    }
}
