using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour {
    
    public static int TheCurrentCharacter;
    public static int TheCurrentLevel;

    public int CurrentCharacter;
    public int CurrentLevel;

    void Awake() {
        CurrentCharacter = TheCurrentCharacter;
        CurrentLevel = TheCurrentLevel;
        Screen.SetResolution(900, 600, false);
    }

    void FixedUpdate() {
        TheCurrentCharacter = CurrentCharacter;
        TheCurrentLevel = CurrentLevel;
    }

    public void DestroyObj(string name) {
        Destroy(GameObject.Find(name));
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void NextCharacter() {
        var menuChars = FindObjectOfType<MenuChars>();
        CurrentCharacter = (CurrentCharacter + 1) % menuChars.Sprites.Length;
        TheCurrentCharacter = CurrentCharacter;
    }

    public void PrevCharacter() {
        var menuChars = FindObjectOfType<MenuChars>();
        CurrentCharacter = (CurrentCharacter - 1 + menuChars.Sprites.Length) % menuChars.Sprites.Length;
        TheCurrentCharacter = CurrentCharacter;
    }

    public void NextLevel() {
        var menuLevels = FindObjectOfType<MenuLevels>();
        CurrentLevel = (CurrentLevel + 1) % menuLevels.Levels.Length;
        TheCurrentLevel = CurrentLevel;
    }

    public void PrevLevel() {
        var menuLevels = FindObjectOfType<MenuLevels>();
        CurrentLevel = (CurrentLevel - 1 + menuLevels.Levels.Length) % menuLevels.Levels.Length;
        TheCurrentLevel = CurrentLevel;
    }

    public void LoadSelectedLevel() {
        var menuLevels = FindObjectOfType<MenuLevels>();
        LoadScene(menuLevels.Levels[CurrentLevel].SceneName);
    }
}