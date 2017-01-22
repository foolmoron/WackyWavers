using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour {

    public float Seconds;

    void Start() {
        Destroy(gameObject, Seconds);
    }
}