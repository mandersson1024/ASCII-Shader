using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTextureTester : MonoBehaviour
{
    public Texture2D[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        Texture2D texture = new Texture2D(1920, 1080, TextureFormat.ARGB32, false);
        texture.FillWithColor(new Color32(255, 0, 255, 255));

        var sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0f, 0f), 30);
        spriteRenderer.sprite = sprite;



        //Graphics.CopyTexture(tiles[0], texture);

        /*
        int numTilesX = 64;
        int numTilesY = 36;

        for (int y = 0; y < numTilesY; ++y)
        {
            for (int x = 0; x < numTilesY; ++x)
            {

            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
