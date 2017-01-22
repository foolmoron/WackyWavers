using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicWave : MonoBehaviour {

    public float BaseHeight = 3;

    public float Offset;

    new AudioSource audio;
    AudioClip clip;

    public const int NUM_POINTS = 32;
    float[] heights = new float[NUM_POINTS];
    float[] data = new float[NUM_POINTS];
    float[] spectrum = new float[NUM_POINTS];
    public float Step = 0.001f;
    
    public TiltBrushToolkit.VisualizerManager audioReactive;

    float timeSinceLastBeat;
    bool isBeating;
    public const int BEAT_WINDOW = 30;
    QueueList<float> beatTimes = new QueueList<float>(BEAT_WINDOW, 0);
    float beatAverage;
    float currentBeatTarget;


    void Start() {
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        // beats
        {
            timeSinceLastBeat += Time.deltaTime;
            if (audioReactive.BeatOutput.max() >= 0.9f) {
                if (!isBeating) {
                    isBeating = true;
                    beatTimes.Dequeue();
                    beatTimes.Enqueue(timeSinceLastBeat);
                    timeSinceLastBeat = 0;
                }
            } else {
                isBeating = false;
            }
        }
        // beat average smoothed
        {
            beatAverage = beatTimes.average();
            currentBeatTarget = Mathf.Clamp(Mathf.Lerp(currentBeatTarget, beatAverage, 0.03f), 0, 1);
        }
        // get current play position as offset
        {
            Offset = audio.time;
        }
        // win
        {
            if (audio.time >= audio.clip.length) {
                GameOver.It.Win();
            }
        }
    }

    public float GetHeight(float x) {
        var xOffset = x + Offset;
        var volumeOffset = (audioReactive.AudioVolume.x) * 0.2f;
        var beatOffset = (Mathf.Sin(12*audioReactive.AudioVolume.x*x) + 1) * Mathf.Clamp01(1 - timeSinceLastBeat) * Mathf.Clamp01(1/x);
        var audioOffset = volumeOffset + beatOffset;
        return GetSimpleHeight(xOffset - Offset) + audioOffset;
        //return BaseHeight + (Mathf.Sin(3.5f * x) * 0.4f + Mathf.Sin(1.2f * x) * 0.2f + Mathf.Sin(0.8f * x) * 0.5f);
    }

    public float GetSimpleHeight(float x) {
        x += Offset;
        var lerp = Mathf.Clamp01((x - 5f) / 2f);
        return BaseHeight + Mathf.Lerp(0, Mathf.Sin(Mathf.Lerp(currentBeatTarget, 0.5f, 0.8f) * 5 * x) * 0.5f, lerp);
    }
}