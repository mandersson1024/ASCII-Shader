using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class FlickerTile : MonoBehaviour
{
    public Tileset tileset;
    public string variations;
    public float intervalSeconds = 0.1f;
    Entity entity;

    void Start()
    {
        entity = GetComponent<Entity>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            int rnd = Random.Range(0, variations.Length);
            char c = variations[rnd];
            entity.SetTile(tileset, c);
            yield return new WaitForSeconds(intervalSeconds);
        }
    }
}
