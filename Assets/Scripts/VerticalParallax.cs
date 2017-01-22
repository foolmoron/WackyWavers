using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalParallax : MonoBehaviour {

    public float BaseCameraY = 3;
    [Range(-1, 1)]
    public float Factor;

    float originalY;

    void Start() {
        originalY = transform.position.y;
    }
    
    void Update() {
        var offset = Camera.main.transform.position.y - BaseCameraY;
        transform.position = transform.position.withY(originalY + offset * Factor);
    }
}