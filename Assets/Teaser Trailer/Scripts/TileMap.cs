using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class TileMap : MonoBehaviour
{
    readonly int pixelsPerUnit = 1;

    Tileset tileset;
    public Texture2D tileAtlas;
    public int tileSizePixels = 30;
    public Texture2D mapImage;
    public Texture2D hiResBackground;
    public Texture2D hiResForeground;

    [Range(0, 1)]
    public float backgroundBlend = 0.5f;

    [Range(0, 1)]
    public float backgroundIntensity = 1f;

    [Range(0, 1)]
    public float foregroundBlend = 0.5f;

    [Range(0, 1)]
    public float foregroundIntensity = 1f;

    Texture2D mapTexture;
    SpriteRenderer mapTextureRenderer;

    [Header("Debug info")]
    public Vector2Int numTiles;
    public Vector2Int mapTextureSize;
    public Texture2D scaledTileAtlas;
    public RenderTexture loResBackground;
    public RenderTexture loResForeground;

    private void Awake()
    {
        mapTextureRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        scaledTileAtlas = tileAtlas.CloneAndScale(tileSizePixels * 16, tileSizePixels * 16);
        tileset = new Tileset(scaledTileAtlas, tileSizePixels);
        
        mapTexture = CreateTextureFromCharacterMap(CharacterMapper.testMap);
        //PopulateFromCharacterMap(CharacterMapper.testMap);
        string[] str = CharacterMapper.FromImage(mapImage);
        PopulateFromCharacterMap(str);

        loResBackground = new RenderTexture(numTiles.x, numTiles.y, 1)
        {
            filterMode = FilterMode.Point
        };

        loResForeground = new RenderTexture(numTiles.x, numTiles.y, 1)
        {
            filterMode = FilterMode.Point
        };

        mapTextureRenderer.sprite = Sprite.Create(mapTexture, new Rect(0f, 0f, mapTextureSize.x, mapTextureSize.y), new Vector2(0.5f, 0.5f), pixelsPerUnit);
        mapTextureRenderer.material.SetTexture("_HiResBackgroundTex", hiResBackground);
        mapTextureRenderer.material.SetTexture("_LoResBackgroundTex", loResBackground);
        mapTextureRenderer.material.SetTexture("_HiResForegroundTex", hiResForeground);
        mapTextureRenderer.material.SetTexture("_LoResForegroundTex", loResForeground);

        // Corners of the map
        Entity.Create(mapTextureRenderer.transform, tileset, '*', this, 0, 0, Color.magenta);
        Entity.Create(mapTextureRenderer.transform, tileset, '*', this, numTiles.x - 1, 0, Color.cyan);
        Entity.Create(mapTextureRenderer.transform, tileset, '*', this, 0, numTiles.y - 1, Color.yellow);
        Entity.Create(mapTextureRenderer.transform, tileset, '*', this, numTiles.x - 1, numTiles.y - 1, Color.red);

        // Fire
        Vector2Int[] positions = { new Vector2Int(18, 20), new Vector2Int(18, 21), new Vector2Int(19, 20), new Vector2Int(19, 21), };
        foreach (Vector2Int pos in positions)
        {
            Entity e = Entity.Create(mapTextureRenderer.transform, tileset, '*', this, pos.x, pos.y, Color.magenta);
            FlickerEffect colorFade = e.gameObject.AddComponent<FlickerEffect>();
            colorFade.tileset = tileset;
        }
    }

    public Vector2 TileCoordsToSpritePosition(int x, int y)
    {
        return new Vector2((x - numTiles.x / 2) * tileSizePixels, (-1 - y + numTiles.y / 2) * tileSizePixels);
    }

    void CopyTexture(Texture2D src, Texture2D dst, int x, int y)
    {
        Graphics.CopyTexture(src, 0, 0, 0, 0, src.width, src.height, dst, 0, 0, x, y);
    }

    private Texture2D CreateTextureFromCharacterMap(string[] characterMap)
    {
        numTiles.x = characterMap[0].Length;
        numTiles.y = characterMap.Length;

        mapTextureSize.x = numTiles.x * tileSizePixels;
        mapTextureSize.y = numTiles.y * tileSizePixels;

        return new Texture2D(mapTextureSize.x, mapTextureSize.y, TextureFormat.RGBA32, false);
    }

    private void PopulateFromCharacterMap(string[] characterMap)
    {
        for (int y = 0; y < numTiles.y; ++y)
        {
            for (int x = 0; x < numTiles.x; ++x)
            {
                char c = characterMap[y][x];
                int index = CharacterMapper.GetIndex(c);
                tileset.DrawTile(mapTexture, index, x * tileset.tileSizePixels, mapTextureSize.y - (y + 1) * tileset.tileSizePixels);
            }
        }
    }

    private void Update()
    {
        mapTextureRenderer.material.SetFloat("_BackgroundBlend", backgroundBlend);
        mapTextureRenderer.material.SetFloat("_BackgroundIntensity", backgroundIntensity);

        mapTextureRenderer.material.SetFloat("_ForegroundBlend", foregroundBlend);
        mapTextureRenderer.material.SetFloat("_ForegroundIntensity", foregroundIntensity);

        Graphics.Blit(hiResBackground, loResBackground);
        Graphics.Blit(hiResForeground, loResForeground);
    }

}


