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

        int spriteIndex = 0;

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

        void Update()
        {
            spriteIndex++;
            spriteIndex %= tileset.Length;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    SpriteRenderer rend = renderers[x, y];
                    rend.sprite = tileset.GetTile(spriteIndex);
                    rend.color = Random.ColorHSV();
                    //transform.localPosition = originalPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                    rend.transform.localScale = new Vector3(Random.Range(0.5f, 1.5f), Random.Range(0.5f, 1.5f), 0f);
                    rend.transform.Rotate(0f, 0f, 1f);
                }
            }
        }
    }
}


