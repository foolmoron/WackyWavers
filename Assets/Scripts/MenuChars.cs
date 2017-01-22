using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChars : MonoBehaviour {
    public SpriteRenderer[] Sprites;
    
    void Update() {
        for (int i = 0; i < Sprites.Length; i++) {
            if (Global.It.CurrentCharacter == i) {
                Sprites[i].color = Color.white;
            } else {
                Sprites[i].color = new Color(0.3f, 0.3f, 0.3f);
            }
        }
    }
}