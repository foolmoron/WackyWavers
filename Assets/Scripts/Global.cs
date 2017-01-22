using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour {

    public static Global It;

    public int CurrentCharacter;
    public int CurrentLevel;

    void Awake() {
        if (It == null) {
            It = this;
            DontDestroyOnLoad(gameObject);
        } else {
            DestroyImmediate(gameObject);
        }
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void NextCharacter() {
        var menuChars = FindObjectOfType<MenuChars>();
        CurrentCharacter = (CurrentCharacter + 1) % menuChars.Sprites.Length;
    }

    public void PrevCharacter() {
        var menuChars = FindObjectOfType<MenuChars>();
        CurrentCharacter = (CurrentCharacter - 1 + menuChars.Sprites.Length) % menuChars.Sprites.Length;
    }

    public void NextLevel() {
        var menuLevels = FindObjectOfType<MenuLevels>();
        CurrentLevel = (CurrentLevel + 1) % menuLevels.Levels.Length;
    }

    public void PrevLevel() {
        var menuLevels = FindObjectOfType<MenuLevels>();
        CurrentLevel = (CurrentLevel - 1 + menuLevels.Levels.Length) % menuLevels.Levels.Length;
    }

    public void LoadSelectedLevel() {
        var menuLevels = FindObjectOfType<MenuLevels>();
        LoadScene(menuLevels.Levels[CurrentLevel].SceneName);
    }
}