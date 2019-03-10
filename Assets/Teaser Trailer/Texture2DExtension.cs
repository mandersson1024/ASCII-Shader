using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Texture2DExtension
{
    public static void FillWithColor(this Texture2D texture, Color color)
    {
        Color[] pixels = new Color[texture.width * texture.height];

        for (int i = 0; i < pixels.Length; ++i)
        {
            pixels[i] = color;
        }

        texture.SetPixels(pixels);
        texture.Apply();
    }
}
