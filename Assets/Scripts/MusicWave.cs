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

    QueueList<float> Volumes = new QueueList<float>();

    public TiltBrushToolkit.VisualizerManager audioReactive;

    void Start() {
        audio = GetComponent<AudioSource>();
        // volumes
        var volumeSamples = Mathf.FloorToInt(9 / Time.fixedDeltaTime);
        for (int i = 0; i < volumeSamples; i++) {
            Volumes.Add(0);
        }
    }

    void FixedUpdate() {
        // check for new clip
        {
            Volumes.Dequeue();
            Volumes.Enqueue(Mathf.Pow(audioReactive.AudioVolume.x, 5) * 10);
        }
        // get current play position as offset
        {
            Offset = audio.time;
        }
    }

    public float GetHeight(float x) {
        x += Offset;
        var i = Mathf.FloorToInt((x - Offset) / Time.fixedDeltaTime);
        var remainder = x - i;
        var audioOffset = (i > 0 && i < Volumes.Count - 1 ? Mathf.Lerp(Volumes[i], Volumes[i + 1], remainder) : 0);
        return GetSimpleHeight(x - Offset) + audioOffset;
        //return BaseHeight + (Mathf.Sin(3.5f * x) * 0.4f + Mathf.Sin(1.2f * x) * 0.2f + Mathf.Sin(0.8f * x) * 0.5f);
    }

    public float GetSimpleHeight(float x) {
        x += Offset;
        var lerp = Mathf.Clamp01((x - 5f) / 2f);
        return BaseHeight + Mathf.Lerp(0, Mathf.Sin(audioReactive.BeatOutputAccum.x / 300 * x) * 0.5f, lerp);
    }
}