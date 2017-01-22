using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithJumper : MonoBehaviour {

    public WaveRider Rider;
    [Range(0, 3)]
    public float Factor;
    public float BaseY;
    public float TargetY;
    
    void Update() {
        if (Rider.Jumping) {
            TargetY = BaseY + Rider.transform.position.y * Factor;
        } else {
            TargetY = BaseY;
        }
        var newY = Mathf.Lerp(transform.position.y, TargetY, 0.05f);
        transform.position = transform.position.withY(newY);
    }
}