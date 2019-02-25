using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Kalaskod
{
    public class Map : MonoBehaviour
    {
        const int width = 64;
        const int height = 36;
        const int tileSize = 40;

        Tileset tileset;
        readonly SpriteRenderer[,] renderers = new SpriteRenderer[width, height];

        void Start()
        {
            tileset = new Tileset("Textures/Spritesheet");

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    renderers[x, y] = CreateRendererAt(x, y);
                }
            }

            PopulateFromCharacterMap(CharacterMapper.sampleCharacterMapSource);
        }

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
            rend.transform.localPosition = new Vector3((x * 2f) - (width - 1f), (y * 2f) - (height - 1f), 0f);
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

        void Update()
        {
            //StressTest.Do(tileset, width, height, renderers);
        }

    }
}


