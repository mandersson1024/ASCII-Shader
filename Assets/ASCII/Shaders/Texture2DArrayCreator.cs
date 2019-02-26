using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Texture2DArrayCreator : MonoBehaviour
{

    [MenuItem("ASCII/Create Luminosity Texture2DArray")]
    static void CreateTexture2DArray()
    {
        const int depth = 256;
        const int tileSize = 32;

        Texture2DArray array = new Texture2DArray(tileSize, tileSize, depth, TextureFormat.RGB24, false);

        for (int i = 0; i < depth; ++i)
        {
            Color[] pixels = new Color[tileSize * tileSize];
            for (int j = 0; j < tileSize * tileSize; ++j)
            {
                int value = i * (256 / depth);
                Color32 col = new Color32((byte)i, (byte)i, (byte)i, 0);
                pixels[j] = col;
            }
            
            array.SetPixels(pixels, i);
        }

        array.Apply();
        AssetDatabase.CreateAsset(array, "Assets/ASCII/Shaders/Luminosity Texture2D Array.asset");
    }

}
