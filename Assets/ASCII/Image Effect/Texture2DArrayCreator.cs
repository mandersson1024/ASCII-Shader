using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Texture2DArrayCreator : MonoBehaviour
{
    public Texture2D[] tiles;
    public Sprite[] sprites;

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

    [MenuItem("ASCII/Create Texture2DArray From Sprites")]
    static void CreateTexture2DArray()
    {
        Sprite[] tileSprites = GameObject.Find("Texture2DArrayCreator").GetComponent<Texture2DArrayCreator>().sprites;

        Texture2DArray array = new Texture2DArray((int)tileSprites[0].textureRect.width, (int)tileSprites[0].textureRect.height, tileSprites.Length, tileSprites[0].texture.format, false);
        for (int i = 0; i < tileSprites.Length; i++)
        {
            var sprite = tileSprites[i];
            var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                        (int)sprite.textureRect.y,
                                        (int)sprite.textureRect.width,
                                        (int)sprite.textureRect.height);
            array.SetPixels(pixels, i);
        }

        array.Apply();
        AssetDatabase.CreateAsset(array, "Assets/ASCII/Image Effect/Tile Texture2DArray.asset");
    }



}
