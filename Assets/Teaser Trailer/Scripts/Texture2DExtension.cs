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

    public static Texture2D CloneAndScale(this Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        Color[] rpixels = result.GetPixels(0);

        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);

        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
        }

        result.SetPixels(rpixels, 0);
        result.Apply();

        return result;
    }
}
