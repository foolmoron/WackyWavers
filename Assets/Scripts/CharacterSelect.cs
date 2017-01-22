using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {

    public GameObject[] CharacterPrefabs;
    GameObject[] chars;

    WaveRider rider;

    int currentCharacter;

    void Start() {
        rider = GetComponent<WaveRider>();

        chars = new GameObject[CharacterPrefabs.Length];
        for (int i = 0; i < CharacterPrefabs.Length; i++) {
            chars[i] = Instantiate(CharacterPrefabs[i]);
            chars[i].SetActive(false);
            chars[i].transform.parent = transform;
            chars[i].transform.localPosition = Vector3.zero;
            chars[i].transform.localEulerAngles = Vector3.zero;
        }
    }

    void Update() {
        // toggles
        {
            if (Input.GetKeyDown(KeyCode.Q)) {
                currentCharacter = (currentCharacter - 1 + chars.Length) % chars.Length;
            } else if (Input.GetKeyDown(KeyCode.W)) {
                currentCharacter = (currentCharacter + 1 + chars.Length) % chars.Length;
            }
        }
        for (int i = 0; i < chars.Length; i++) {
            if (i == currentCharacter) {
                chars[i].SetActive(true);
                rider.RotateObj = chars[i];
                rider.SpeedParticles = chars[i].GetComponentInChildren<ParticleSystem>();
            } else {
                chars[i].SetActive(false);
            }
        }
    }
}