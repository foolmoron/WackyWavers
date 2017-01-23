using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public static GameOver It;

    public GameObject[] LoseObjs;
    public GameObject[] WinObjs;

    void Awake() {
        It = this;
        LoseObjs.ForEach(o => o.SetActive(false));
        WinObjs.ForEach(o => o.SetActive(false));
    }

    public void Lose() {
        FindObjectOfType<WaveRider>().gameObject.SetActive(false);
        FindObjectsOfType<AudioSource>().ForEach(a => a.Stop());
        LoseObjs.ForEach(o => o.SetActive(true));
    }

    public void Win() {
        FindObjectOfType<WaveRider>().gameObject.SetActive(false);
        FindObjectsOfType<AudioSource>().ForEach(a => a.Stop());
        WinObjs.ForEach(o => o.SetActive(true));
    }

    public void LoadSameScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}