using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public WaveRider Rider;
    public float BaseScrollSpeed = 1f;
    public float Scale = 1f;
    public List<Transform> Items = new List<Transform>(50);
    public GameObject[] StuffPrefabs;

    public float TimeBetweenSpawn = 5;
    public float TimeToNextSpawn;

    void Start() {

    }
        
    void Update() {
        // spawn
        {
            TimeToNextSpawn -= Time.deltaTime;
            if (TimeToNextSpawn <= 0 && StuffPrefabs.Length > 0) {
                TimeToNextSpawn = TimeBetweenSpawn * (Random.value * 0.5f + 0.5f);

                var z = Random.value;

                var thing = StuffPrefabs.Random();
                var newObj = Instantiate(thing);
                newObj.transform.parent = transform;
                newObj.transform.localPosition = new Vector3(30, 0, 0);
                newObj.transform.localScale = newObj.transform.localScale * Scale * Mathf.Lerp(0.9f, 1.1f, Random.value);
                Items.Add(newObj.transform);

                var parallax = newObj.GetComponent<VerticalParallax>();
                if (parallax) {
                    parallax.Factor = z * z;
                }
            }
        }
        // scroll
        {
            var scrollAmount = BaseScrollSpeed * Mathf.Max(Rider.Speed, 1f) * Time.deltaTime;
            foreach (var item in Items) {
                item.localPosition = item.localPosition.plusX(-scrollAmount);
            }
        }
        // delete items
        {
            for (int i = 0; i < Items.Count; i++) {
                if (Items[i].position.x <= -100) {
                    Destroy(Items[i].gameObject);
                    Items.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}