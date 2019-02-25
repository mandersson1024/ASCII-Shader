using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kalaskod
{
    public class StressTest
    {
        static int spriteIndex = 0;

        public static void Do(Tileset tileset, int width, int height, SpriteRenderer[,] renderers)
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
