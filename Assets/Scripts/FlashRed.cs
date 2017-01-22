using UnityEngine;
using System.Collections;

public class FlashRed : MonoBehaviour {

    [Range(0, 10f)]
    public float PulseTime = 0.3f;
    [Range(0, 2f)]
    public float PulseOffset = 0f;

    private SpriteRenderer spriteRenderer;
    Color originalColor;
    Color redColor;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer) {
            Destroy(this);
            return;
        }
        originalColor = spriteRenderer.color;
        redColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g * 0.2f, spriteRenderer.color.b * 0.2f);
    }

    void Update() {
        var offsetPulseTime = (Time.time + PulseOffset) % PulseTime;
        var interp = (offsetPulseTime / PulseTime) * 2 - 1;
        spriteRenderer.color = Color.Lerp(originalColor, redColor, interp);
    }
}
