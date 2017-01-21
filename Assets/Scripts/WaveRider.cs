using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRider : MonoBehaviour {

    public MusicWave MusicWave;
    [Range(0, 5)]
    public float HeightSpeed = 1;

    public const int ANGLE_SAMPLES = 12;
    QueueList<float> angles = new QueueList<float>(ANGLE_SAMPLES);

    float Average(QueueList<float> list) {
        var sum = 0f;
        foreach (var item in list) {
            sum += item;
        }
        return sum / list.Count;
    }

    void Start() {
        while (angles.Count < ANGLE_SAMPLES) {
            angles.Enqueue(0);
        }
    }

    void FixedUpdate() {
        var currentY = MusicWave.GetHeight(transform.position.x);
        var dX = 0.01f;
        var dY = currentY - MusicWave.GetHeight(transform.position.x + dX);
        angles.Dequeue();
        angles.Enqueue(Mathf.Atan2(dY, dX) * Mathf.Rad2Deg);
        var angle = Average(angles);
        transform.position = Vector3.MoveTowards(transform.position, transform.position.withY(currentY), HeightSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.withZ(-angle));
    }
}