using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRider : MonoBehaviour {

    public WaveHeight WaveHeight;

    void Update() {
        var currentY = WaveHeight.GetHeight(transform.position.x);
        var dX = 0.01f;
        var dY = currentY - WaveHeight.GetHeight(transform.position.x + dX);
        var angle = Mathf.Atan2(dY, dX) * Mathf.Rad2Deg;
        transform.position = transform.position.withY(currentY);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.withZ(-angle));
    }
}