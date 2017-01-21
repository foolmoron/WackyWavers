using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHeight : MonoBehaviour {

    public float BaseHeight = 3;

    public float Offset;
    public float OffsetVelocity = 1;

    void Update() {
        // offset
        {
            Offset += OffsetVelocity * Time.deltaTime;
        }
    }

    public float GetHeight(float x) {
        x += Offset;
        return BaseHeight + (Mathf.Sin(3.5f*x)*0.4f + Mathf.Sin(1.2f*x)*0.2f + Mathf.Sin(0.8f*x)*0.5f);
    }
}