using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {

    public float FixedZ;

    void Update() {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.withZ(FixedZ));
    }
}