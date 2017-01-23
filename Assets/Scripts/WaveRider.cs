using System.Collections;
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
    public float Speed = 5;
    public float SpeedTarget = 5;
    [Range(0, 10)]
    public float Acceleration = 1;
    [Range(0, 10)]
    public float Deceleration = 3;
    public float MinX = -0.5f;
    public float MaxX = 5;

    public bool Jumping;
    public float JumpY;
    public float JumpVelocity;
    public float JumpGravity = 10;
    public AnimationCurve SpeedToJump;
    [Range(0, 10)]
    public float AccelerationBonus = 2;

    public GameObject[] GoodObjs;
    public GameObject[] HurtObjs;

    bool init;

    void Start() {
        while (angles.Count < ANGLE_SAMPLES) {
            angles.Enqueue(0);
        }
        while (correctness.Count < CORRECTNESS_SAMPLES) {
            correctness.Enqueue(0);
        }
    }

    void FixedUpdate() {
        if (!init) {
            init = true;
            return;
        }
        // do angle
        {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                Angle += AngleSpeed * Time.deltaTime;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                Angle -= AngleSpeed * Time.deltaTime;
            }
        }
        // angles and positions
        var currentY = MusicWave.GetSimpleHeight(transform.position.x);
        {
            var dX = 0.0001f;
            var dY = MusicWave.GetSimpleHeight(transform.position.x + dX/2) - MusicWave.GetSimpleHeight(transform.position.x - dX/2);
            angles.Dequeue();
            angles.Enqueue(Mathf.Atan2(dY, dX) * Mathf.Rad2Deg);
        }
        var smoothedWaveAngle = angles.average();
        // correctness
        var currentCorrectness = 1 - (Mathf.Abs(Angle - smoothedWaveAngle) / 90);
        {
            if (!Jumping) {
                correctness.Dequeue();
                correctness.Enqueue(currentCorrectness);
            }
            var averageCorrectness = correctness.average();

            var speeding = averageCorrectness > 0.7f;
            SpeedParticles.enableEmission(speeding && !Jumping);
            if (!Jumping) {
                SpeedTarget += Time.deltaTime * (speeding ? Acceleration : -Deceleration);
                SpeedTarget = Mathf.Clamp(SpeedTarget, 0, 10);
            }
        }
        // do jump
        {
            if (Input.GetKey(KeyCode.UpArrow) && !Jumping) {
                Jumping = true;
                JumpY = currentY + 0.01f;
                JumpVelocity = SpeedToJump.Evaluate(SpeedTarget);
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
            if (Jumping && JumpY <= currentY) {
                Jumping = false;
                // modify speed by landing
                if (currentCorrectness > 0.7f) {
                    SpeedTarget += currentCorrectness * AccelerationBonus;
                    Instantiate(GoodObjs.Random(), transform.position.withZ(-7), Quaternion.identity);
                } else {
                    GetHurt();
                }
            }
        }
        // speed
        {
            Speed = Mathf.Lerp(Speed, SpeedTarget, .1f);
        }
        // position
        {
            var newPos = new Vector3(Mathf.Lerp(MinX, MaxX, Speed / 10), currentY, transform.position.z);
            transform.position = newPos;
            if (Jumping) {
                transform.position = transform.position.withY(JumpY);
            }
        }
        // rotate
        {
            RotateObj.transform.rotation = Quaternion.Euler(RotateObj.transform.rotation.eulerAngles.withZ(Angle));
        }
        // game over
        {
            if (Speed <= 0.25f) {
                GameOver.It.Lose();
            }
        }
    }

    public void GetHurt() {
        SpeedTarget -= 2f;
        Instantiate(HurtObjs.Random(), transform.position.withZ(-7), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other) {
        GetHurt();
        Destroy(other.gameObject);
    }
}