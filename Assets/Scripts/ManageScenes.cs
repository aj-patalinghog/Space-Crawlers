using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void TransitionToBattle() {
        StartCoroutine(LoadScene("Battle"));
    }

    IEnumerator LoadScene(string scene) {
        SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioListener>().enabled = false;
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        yield return new WaitForSeconds(3f);
        SceneManager.UnloadSceneAsync("Transition");
    }

    public void TransitionToLevel() {
        StartCoroutine(UnloadScene("Battle"));
    }

    IEnumerator UnloadScene(string scene) {
        SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync(scene);
        FindObjectOfType<AudioListener>().enabled = true;
        yield return new WaitForSeconds(3f);
        SceneManager.UnloadSceneAsync("Transition");
    }
}
