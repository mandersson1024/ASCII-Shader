using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class FadeAndDieEffect : MonoBehaviour
{
    public float fadeTime = 2f;
    Entity entity;
    float spawnTime;

    void Start()
    {
        entity = GetComponent<Entity>();
        spawnTime = Time.time;
    }

    void Update()
    {
        float uptime = Time.time - spawnTime;
        float alpha = Mathf.Lerp(1f, 0f, uptime / fadeTime);
        Color c = entity.GetColor();
        c.a = alpha;
        entity.SetColor(c);

        if (uptime >= fadeTime)
        {
            Destroy(gameObject);
        }
    }
}
