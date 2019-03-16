using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class Map : MonoBehaviour
{
    readonly int pixelWidth = 2560;
    readonly int pixelHeight = 1440;
    readonly int pixelsPerUnit = 30;

    public Tileset tileset;
    Texture2D texture;
    Sprite sprite;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        texture = new Texture2D(pixelWidth, pixelHeight, TextureFormat.RGBA32, false);
        texture.FillWithColor(new Color32(250, 158, 51, 255));

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0f, 0f, pixelWidth, pixelHeight), new Vector2(0.5f, 0.5f), pixelsPerUnit);

        //mapTexture = MapTextureFactory.CreateMapTexture(64, 36, 40, null, null, null); // todo: fill this in

        /*
        int i = 0;
        for (int y = 0; y < 16; ++y)
        {
            for (int x = 0; x < 16; ++x)
            {
                tileset.DrawTile(texture, i, x * tileset.tilePixelSize, pixelHeight - (y + 1) * tileset.tilePixelSize);
                ++i;
            }
        }
        */

        //*
        int i = 0;
        for (int y = 0; y < 36; ++y)
        {
            for (int x = 0; x < 64; ++x)
            {
                char c = CharacterMapper.sampleCharacterMapSource[i];
                int index = CharacterMapper.GetIndex(c);
                tileset.DrawTile(texture, index, x * tileset.tilePixelSize, pixelHeight - (y + 1) * tileset.tilePixelSize);
                ++i;
            }
        }
        //*/


        //PopulateFromCharacterMap(CharacterMapper.sampleCharacterMapSource);
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

    private void PopulateFromCharacterMap(string characterMap)
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                char c = characterMap[y*width + x];
                int index = CharacterMapper.GetIndex(c);
                Sprite sprite = tileset.GetTile(index);
                renderers[x,y].sprite = sprite;
            }
        }
    }
    */

    void Update()
    {
        //StressTest.Do(tileset, width, height, renderers);
    }

}


