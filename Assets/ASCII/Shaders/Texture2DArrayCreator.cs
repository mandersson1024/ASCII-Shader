using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Texture2DArrayCreator : MonoBehaviour
{

    [MenuItem("ASCII/Create Luminosity Texture2DArray")]
    static void CreateTexture2DArray()
    {
        const int depth = 5;
        const int tileSize = 40;

        Texture2DArray array = new Texture2DArray(tileSize, tileSize, depth, TextureFormat.RGB24, false);

        for (int i = 0; i < depth; ++i)
        {
            Color[] pixels = new Color[tileSize * tileSize];
            for (int j = 0; j < tileSize * tileSize; ++j)
            {
                byte value = (byte) ((i * 256) / depth);
                Color32 col = new Color32(value, value, value, 0);
                pixels[j] = col;
            }
            
            array.SetPixels(pixels, i);
        }

        array.Apply();
        AssetDatabase.CreateAsset(array, "Assets/ASCII/Shaders/Luminosity Texture2D Array.asset");
    }

    [MenuItem("ASCII/Create Texture2DArray From Spritesheet")]
    static void CreateTexture2DArrayFromSpriteSheet()
    {
        Object[] selection = Selection.objects;
        Texture2D[] textures = new Texture2D[selection.Length];
        for (int i = 0; i < textures.Length; i++)
        {
            textures[i] = (Texture2D)selection[i];
        }

        Texture2DArray array = new Texture2DArray(textures[0].width, textures[0].height, textures.Length, textures[0].format, false);
        for (int i = 0; i < textures.Length; i++)
            array.SetPixels(textures[i].GetPixels(), i);

        array.Apply();
        AssetDatabase.CreateAsset(array, "Assets/ASCII/Shaders/Spritesheet Texture2D Array.asset");
    }


}
