using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour {
    
    public GameObject[] FallingPrefabs;
    public GameObject[] WaterPrefabs;

    public float TimeBetweenSpawn = 5;
    public float TimeToNextSpawn;

    void Start() {

    }
        
    void Update() {
        // spawn
        {
            TimeToNextSpawn -= Time.deltaTime;
            if (TimeToNextSpawn <= 0) {
                GameObject obj = null;
                if (Random.value > 0.5f) {
                    // falling
                    var thing = FallingPrefabs.Random();
                    obj = Instantiate(thing);
                    obj.transform.position = new Vector3(Mathf.Lerp(1, 6, Random.value), 8, -5);
                } else {
                    // water
                    var thing = WaterPrefabs.Random();
                    obj = Instantiate(thing);
                    obj.transform.position = new Vector3(15, Mathf.Lerp(0, 2, Random.value), -5);
                }
                obj.AddComponent<DieAfterTime>().Seconds = 30;
                obj.AddComponent<FlashRed>();
                foreach (var sprite in obj.GetComponentsInChildren<SpriteRenderer>()) {
                    sprite.gameObject.AddComponent<FlashRed>();
                }
                TimeToNextSpawn = TimeBetweenSpawn * (Random.value * 0.3f + 0.7f);
            }
        }
    }
}