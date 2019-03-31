using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class FlickerEffect : MonoBehaviour
{
    public Tileset tileset;
    public string variations = "*#+";
    public float maxIntervalSeconds = 0.8f;
    public float minIntervalSeconds = 0.2f;
    public Color highlight = Color.yellow;
    public Color faded = Color.red;
    public float fadeSpeed = 0.03f;
    Entity entity;
    float lastHighlightTime = 0;

    void Start()
    {
        entity = GetComponent<Entity>();
        StartCoroutine(HighlightAndFade());
    }

    IEnumerator HighlightAndFade()
    {
        while (true)
        {
            int rnd = Random.Range(0, variations.Length);
            char c = variations[rnd];
            entity.SetTile(tileset, c);

            entity.SetColor(highlight);
            entity.SetGlow(0.7f);
            lastHighlightTime = Time.time;
            float sleepTime = Random.Range(minIntervalSeconds, maxIntervalSeconds);

            yield return new WaitForSeconds(sleepTime);
        }
    }

    private void Update()
    {
        float fadeTime = Time.time - lastHighlightTime;
        Color c = Color.Lerp(entity.GetColor(), faded, fadeTime * fadeSpeed);
        entity.SetColor(c);
    }
}
