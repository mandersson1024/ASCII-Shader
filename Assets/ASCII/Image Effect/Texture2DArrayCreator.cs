using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Texture2DArrayCreator : MonoBehaviour
{
    public Texture2D[] tiles;

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

    [MenuItem("ASCII/Create Texture2DArray From List")]
    static void CreateTexture2DArrayFromSpriteSheet()
    {
        Texture2D[] tileTextures = GameObject.Find("Texture2DArrayCreator").GetComponent<Texture2DArrayCreator>().tiles;

        Texture2DArray array = new Texture2DArray(tileTextures[0].width, tileTextures[0].height, tileTextures.Length, tileTextures[0].format, false);
        for (int i = 0; i < tileTextures.Length; i++)
            array.SetPixels(tileTextures[i].GetPixels(), i);

        array.Apply();
        AssetDatabase.CreateAsset(array, "Assets/ASCII/Image Effect/Tile Texture2DArray.asset");
    }


}
