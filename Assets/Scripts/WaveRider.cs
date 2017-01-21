﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveRider : MonoBehaviour {

    public MusicWave MusicWave;
    [Range(0, 5)]
    public float HeightSpeed = 1;
    [Range(0, 360)]
    public float AngleSpeed = 90;
    public float Angle;

    public GameObject RotateObj;

    public const int ANGLE_SAMPLES = 4;
    QueueList<float> angles = new QueueList<float>(ANGLE_SAMPLES);

    public const int CORRECTNESS_SAMPLES = 30;
    QueueList<float> correctness = new QueueList<float>(CORRECTNESS_SAMPLES);
    
    public ParticleSystem SpeedParticles;
    public float Speed = 2;
    [Range(0, 10)]
    public float Acceleration = 1;
    [Range(0, 10)]
    public float Deceleration = 3;
    public float MinX = -0.5f;
    public float MaxX = 5;

    public bool Jumping;
    public float JumpY;
    public float JumpVelocity;
    public float JumpGravity = -10;
    public AnimationCurve SpeedToJump;

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
        while (correctness.Count < CORRECTNESS_SAMPLES) {
            correctness.Enqueue(0);
        }
    }

    void FixedUpdate() {
        // do angle
        {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                Angle -= AngleSpeed * Time.deltaTime;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                Angle += AngleSpeed * Time.deltaTime;
            }
        }
        // angles and positions
        var currentY = MusicWave.GetHeight(transform.position.x);
        {
            var dX = 0.0001f;
            var dY = MusicWave.GetSimpleHeight(transform.position.x + dX/2) - MusicWave.GetSimpleHeight(transform.position.x - dX/2);
            angles.Dequeue();
            angles.Enqueue(Mathf.Atan2(dY, dX) * Mathf.Rad2Deg);
        }
        var smoothedWaveAngle = Average(angles);
        // correctness
        {
            correctness.Dequeue();
            correctness.Enqueue(1 - (Mathf.Abs(Angle - smoothedWaveAngle) / 90));
            var averageCorrectness = Average(correctness);

            var speeding = averageCorrectness > 0.7f;
            SpeedParticles.enableEmission(speeding);
            if (!Jumping) {
                Speed += Time.deltaTime * (speeding ? Acceleration : -Deceleration);
                Speed = Mathf.Clamp(Speed, 0, 10);
            }
        }
        // do jump
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !Jumping) {
                Jumping = true;
                JumpY = currentY + 0.01f;
                JumpVelocity = SpeedToJump.Evaluate(Speed);
            }
        }
        // velocity/ gravity
        {
            if (Jumping) {
                JumpVelocity -= JumpGravity * Time.deltaTime;
                JumpY += JumpVelocity * Time.deltaTime;
            }
        }
        // landing
        {
            if (JumpY <= currentY) {
                Jumping = false;
            }
        }
        // position
        {
            var newPos = new Vector3(Mathf.Lerp(MinX, MaxX, Speed / 10), currentY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPos, HeightSpeed * Time.deltaTime);
            if (Jumping) {
                transform.position = transform.position.withY(JumpY);
            }
        }
        // rotate
        {
            RotateObj.transform.rotation = Quaternion.Euler(RotateObj.transform.rotation.eulerAngles.withZ(Angle));
        }
    }
}