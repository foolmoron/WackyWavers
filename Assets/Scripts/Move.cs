using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public Vector3 MoveSpeed;

    void Update() {
        transform.position += MoveSpeed * Time.deltaTime;
    }
}