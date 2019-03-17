using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class Tylemap : MonoBehaviour
{
    readonly int pixelsPerUnit = 1;

    Tileset tileset;
    public Texture2D tileAtlas;
    public Vector2Int numTiles = new Vector2Int(64, 36);
    public int tileSizePixels = 30;

    Texture2D mapTexture;
    readonly Sprite mapSprite;


    SpriteRenderer spriteRenderer;

    [Header("FYI (Do not edit)")]
    public Vector2Int mapTextureSize;
    public Texture2D scaledTileAtlas;

    void Start()
    {
        mapTextureSize.Set(numTiles.x * tileSizePixels, numTiles.y * tileSizePixels);

        mapTexture = new Texture2D(mapTextureSize.x, mapTextureSize.y, TextureFormat.RGBA32, false);
        mapTexture.FillWithColor(new Color32(250, 158, 51, 255));

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(mapTexture, new Rect(0f, 0f, mapTextureSize.x, mapTextureSize.y), new Vector2(0.5f, 0.5f), pixelsPerUnit);

        scaledTileAtlas = tileAtlas.CloneAndScale(tileSizePixels * 16, tileSizePixels * 16);
        tileset = new Tileset(scaledTileAtlas, tileSizePixels);
        
        PopulateFromCharacterMap(CharacterMapper.sampleCharacterMapSource);
    }

    void CopyTexture(Texture2D src, Texture2D dst, int x, int y)
    {
        Graphics.CopyTexture(src, 0, 0, 0, 0, src.width, src.height, dst, 0, 0, x, y);
    }

    /*
    private SpriteRenderer CreateRendererAt(int x, int y)
    {
        string name = "Tile[" + x + "," + y + "]";
        GameObject go = new GameObject(name);
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        SetPosition(x, y, sr);
        return sr;
    }

    private void SetPosition(int x, int y, SpriteRenderer rend)
    {
        float _x = x - (width / 2) + 0.5f;
        float _y = -y + (height / 2) - 0.5f;
        rend.transform.localPosition = new Vector3(_x, _y, 0f);
    }
    */

    private void PopulateFromCharacterMap(string characterMap)
    {
        int i = 0;
        for (int y = 0; y < numTiles.y; ++y)
        {
            for (int x = 0; x < numTiles.x; ++x)
            {
                char c = characterMap[i];
                int index = CharacterMapper.GetIndex(c);
                tileset.DrawTile(mapTexture, index, x * tileset.tileSizePixels, mapTextureSize.y - (y + 1) * tileset.tileSizePixels);
                ++i;
            }
        }
    }

    void Update()
    {
        //StressTest.Do(tileset, width, height, renderers);
    }

}


