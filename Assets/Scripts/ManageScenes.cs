using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    AudioSource audio;
    void Start(){
        audio = GetComponent<AudioSource>();
    }
    public IEnumerator LoadScene(string scene) {
        SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
        audio.Stop();
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioListener>().enabled = false;
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        yield return new WaitForSeconds(3f);
        SceneManager.UnloadSceneAsync("Transition");
    }

    public IEnumerator UnloadScene(string scene) {
        SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync(scene);
        FindObjectOfType<AudioListener>().enabled = true;
        Invoke("FinishUnloading", 3f);
    }

    void FinishUnloading() {
        audio.Play();
        SceneManager.UnloadSceneAsync("Transition");
    }

    public void ExitMenu() {
        StartCoroutine(ReplaceScene());
    }

    public void EnterMenu(){
        StartCoroutine(LoadScene("MainMenu"));
    }

    public IEnumerator ReplaceScene() {
        Canvas menu = FindObjectOfType<Canvas>();
        AudioListener audio = FindObjectOfType<AudioListener>();
        SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        audio.enabled = false;
        menu.enabled = false;
        SceneManager.LoadScene("Level0", LoadSceneMode.Additive);
        yield return new WaitForSeconds(3f);
        SceneManager.UnloadSceneAsync("Transition");
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void LoadSingle(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
