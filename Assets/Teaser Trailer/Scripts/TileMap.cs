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
    public Texture2D tileTexture;
    public Texture2D backgroundColorTexture;
    public Material entityMaterial;
    public TileColorMapper tileColorMapper;


    [Header("Debug info")]
    public SpriteRenderer mapTextureRenderer;
    public Texture2D generatedColorTexture;
    public Texture2D mapTexture;
    public Vector2Int numTiles;
    public Vector2Int mapTextureSize;
    public Texture2D scaledTileAtlas;

    private void Start()
    {
        Assert.IsNotNull(entityMaterial);

        mapTextureRenderer = GetComponent<SpriteRenderer>();
        scaledTileAtlas = tileAtlas.CloneAndScale(tileSizePixels * 16, tileSizePixels * 16);
        tileset = new Tileset(scaledTileAtlas, tileSizePixels);

        //mapTexture = CreateTextureFromCharacterMap(CharacterMapper.testMap);
        //PopulateFromCharacterMap(CharacterMapper.testMap);
        CreateTileTexture();
        mapTextureRenderer.material.SetTexture("_BackgroundColorTex", backgroundColorTexture);
        mapTextureRenderer.material.SetTexture("_TileColorTex", generatedColorTexture);

        // Corners of the map
        /*
        Entity.Create(mapTextureRenderer.transform, entityMaterial, tileset, '*', this, 0, 0, Color.magenta);
        Entity.Create(mapTextureRenderer.transform, entityMaterial, tileset, '*', this, numTiles.x - 1, 0, Color.cyan);
        Entity.Create(mapTextureRenderer.transform, entityMaterial, tileset, '*', this, 0, numTiles.y - 1, Color.yellow);
        Entity.Create(mapTextureRenderer.transform, entityMaterial, tileset, '*', this, numTiles.x - 1, numTiles.y - 1, Color.red);
        */
        
        //FireEffect();
        //StartCoroutine(GhostWalker());
    }

    public void CreateTileTexture()
    {
        char[,] charMap = CharacterMapper.FromImage(tileTexture);
        mapTexture = CreateTextureFromCharacterMap(charMap);
        generatedColorTexture = CreateColorTextureFromCharacterMap(charMap);
        PopulateFromCharacterMap(charMap);
        mapTextureRenderer.sprite = Sprite.Create(mapTexture, new Rect(0f, 0f, mapTextureSize.x, mapTextureSize.y), new Vector2(0.5f, 0.5f), pixelsPerUnit);
    }

    void FireEffect()
    {
        Vector2Int[] positions = {
            new Vector2Int(63, 13),
            new Vector2Int(64, 13),
            new Vector2Int(65, 13),
            new Vector2Int(66, 13),
            new Vector2Int(61, 14),
            new Vector2Int(63, 14),
            new Vector2Int(64, 14),
            new Vector2Int(65, 14),
            new Vector2Int(66, 14),
            new Vector2Int(60, 15),
            new Vector2Int(61, 15),
            new Vector2Int(62, 15),
            new Vector2Int(63, 15),
            new Vector2Int(64, 15),
            new Vector2Int(65, 15),
            new Vector2Int(66, 15),
            new Vector2Int(61, 16),
            new Vector2Int(65, 16),
            new Vector2Int(61, 17),
            new Vector2Int(63, 17),
            new Vector2Int(64, 17),
            new Vector2Int(65, 17),
            new Vector2Int(66, 17),
            new Vector2Int(63, 18),
            new Vector2Int(64, 18),
            new Vector2Int(65, 18),
            new Vector2Int(66, 18),
        };

        foreach (Vector2Int pos in positions)
        {
            Entity e = Entity.Create(mapTextureRenderer.transform, entityMaterial, tileset, '*', this, pos.x, pos.y, Color.magenta);
            FlickerEffect fx = e.gameObject.AddComponent<FlickerEffect>();
            fx.tileset = tileset;
        }
    }

    void PlaceGhost(int x, int y)
    {
        // FadeAndDie Effect
        Entity e = Entity.Create(mapTextureRenderer.transform, entityMaterial, tileset, '*', this, x, y, Color.cyan);
        FadeAndDieEffect fx = e.gameObject.AddComponent<FadeAndDieEffect>();
    }

    IEnumerator GhostWalker()
    {
        float delay = 0.13f;

        Vector2Int[] positions =
        {
            new Vector2Int(47, 23),
            new Vector2Int(57, 23),
            new Vector2Int(57, 29),
            new Vector2Int(47, 29),
        };

        int index = 0;

        while (true)
        {
            int nextIndex = (index + 1) % positions.Length;
            Vector2Int startPos = positions[index];
            Vector2Int endPos = positions[nextIndex];
            Vector2Int direction = endPos - startPos;
            direction.x = Math.Sign(direction.x);
            direction.y = Math.Sign(direction.y);

            for (Vector2Int pos = startPos; pos != endPos; pos += direction)
            {
                PlaceGhost(pos.x, pos.y);
                yield return new WaitForSeconds(delay);
            }

            index = (index + 1) % positions.Length;
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

    public Texture2D CreateTextureFromCharacterMap(char[,] characterMap)
    {
        numTiles.x = characterMap.GetLength(0);
        numTiles.y = characterMap.GetLength(1);

        mapTextureSize.x = numTiles.x * tileSizePixels;
        mapTextureSize.y = numTiles.y * tileSizePixels;

        return new Texture2D(mapTextureSize.x, mapTextureSize.y, TextureFormat.RGBA32, false);
    }

    public Texture2D CreateColorTextureFromCharacterMap(char[,] characterMap)
    {
        numTiles.x = characterMap.GetLength(0);
        numTiles.y = characterMap.GetLength(1);

        return new Texture2D(numTiles.x, numTiles.y, TextureFormat.RGBA32, false)
        {
            filterMode = FilterMode.Point
        };
    }

    private void PopulateFromCharacterMap(char[,] characterMap)
    {
        for (int y = 0; y < numTiles.y; ++y)
        {
            for (int x = 0; x < numTiles.x; ++x)
            {
                char c = characterMap[x,y];
                int index = CharacterMapper.GetIndex(c);
                tileset.DrawTile(mapTexture, index, x * tileset.tileSizePixels, mapTextureSize.y - (y + 1) * tileset.tileSizePixels);

                Color color = tileColorMapper.GetTileColor(c);
                generatedColorTexture.SetPixel(x, mapTextureSize.y - y - 1, color);
            }
        }

        generatedColorTexture.Apply();
    }

    private void Update()
    {
        mapTextureRenderer.material.SetTexture("_BackgroundColorTex", backgroundColorTexture);
        mapTextureRenderer.material.SetTexture("_TileColorTex", generatedColorTexture);
    }

}


