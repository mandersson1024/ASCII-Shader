using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeTesting : MonoBehaviour
{
    public Config config;
    public Texture2D texture;

    // Start is called before the first frame update
    void Start()
    {
        PlaceTile(0, 0);
        PlaceTile(63, 0);
        PlaceTile(0, 35);
        PlaceTile(63, 35);
    }

    void PlaceTile(int x, int y)
    {
        GameObject go = new GameObject();
        SpriteRenderer rend = go.AddComponent<SpriteRenderer>();
        Rect size = new Rect(0, 0, texture.width, texture.height);
        Vector2 center = new Vector2(texture.width / 2f, texture.height / 2f);
        rend.sprite = Sprite.Create(texture, size, center, 15);

        go.transform.position = CalculateTransformPosition(x, y);
    }

    static Vector3Int CalculateTransformPosition(int x, int y)
    {
        int _x = x * 2 - 63;
        int _y = y * 2 - 35;
        return new Vector3Int(_x, _y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
