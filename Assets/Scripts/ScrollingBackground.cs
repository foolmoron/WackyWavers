using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public WaveRider Rider;
    public float BaseScrollSpeed = 1f;
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
                newObj.transform.localPosition = new Vector3(30, 4*z + (Random.value - 0.5f), z);
                newObj.transform.localScale = newObj.transform.localScale * Mathf.Lerp(2, 0.2f, z);
                Items.Add(newObj.transform);

                var parallax = newObj.GetComponent<VerticalParallax>();
                if (parallax) {
                    parallax.Factor = z * z;
                }
            }
        }
        // scroll
        {
            var scrollAmount = BaseScrollSpeed * Rider.Speed * Time.deltaTime;
            foreach (var item in Items) {
                item.localPosition = item.localPosition.plusX(-scrollAmount * ((1 - item.localPosition.z) * (1 - item.localPosition.z) + 0.08f));
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