using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTextureTester : MonoBehaviour
{
    readonly int pixelsPerUnit = 30;

    public Color[] colors;
    Texture2D texture;
    Texture2D tile;

    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(1920, 1080, TextureFormat.ARGB32, false);
        tile = new Texture2D(pixelsPerUnit, pixelsPerUnit, TextureFormat.ARGB32, false);

        texture.FillWithColor(new Color32(255, 0, 255, 255));

        var sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
        GetComponent<SpriteRenderer>().sprite = sprite;

        int numTilesX = 64;
        int numTilesY = 36;
        int colorIndex = 0;

        for (int y = 0; y < numTilesY; ++y)
        {
            for (int x = 0; x < numTilesX; ++x)
            {
                tile.FillWithColor(colors[colorIndex]);

                CopyTexture(tile, texture, x * pixelsPerUnit, y * pixelsPerUnit);
                ++colorIndex;
                colorIndex %= colors.Length;
            }
        }
    }

    void CopyTexture(Texture2D src, Texture2D dst, int x, int y)
    {
        Graphics.CopyTexture(src, 0, 0, 0, 0, src.width, src.height, dst, 0, 0, x, y);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        int numTilesX = 64;
        int numTilesY = 36;
        int colorIndex = 0;

        for (int y = 0; y < numTilesY; ++y)
        {
            for (int x = 0; x < numTilesX; ++x)
            {
                tile.FillWithColor(colors[colorIndex]);

                CopyTexture(tile, texture, x * 30, y * 30);
                ++colorIndex;
                colorIndex %= colors.Length;
            }
        }
        */
    }
}
