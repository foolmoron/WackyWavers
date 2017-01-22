using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestroy : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}