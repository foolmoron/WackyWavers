using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelObj {
    public string SceneName;
    public Vector3 DotPos;
    public Sprite NameSprite;
}
public class MenuLevels : MonoBehaviour {
    public LevelObj[] Levels;
    public Transform Dot;
    public SpriteRenderer Name;
    
    void Update() {
        for (int i = 0; i < Levels.Length; i++) {
            if (Global.TheCurrentLevel == i) {
                Dot.position = Levels[i].DotPos;
                Name.sprite = Levels[i].NameSprite;
            }
        }
    }
}