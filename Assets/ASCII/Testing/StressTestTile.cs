using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressTestTile : MonoBehaviour
{
    SpriteRenderer rend;
    Vector3 originalPosition;
    Vector3 originalScale;

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = sprites[0];
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }

    int spriteIndex = 0;

    void Update()
    {
        
        spriteIndex++;
        spriteIndex %= sprites.Length;
        rend.sprite = sprites[spriteIndex];

        rend.color = Random.ColorHSV();
        transform.localPosition = originalPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        transform.localScale = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), 0f);
        transform.Rotate(0f, 0f, 1f);
    }
}
