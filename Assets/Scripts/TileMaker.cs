using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class TileMaker : MonoBehaviour
{
    public Config config;

    /*
    Dictionary<int, int> fontSizes = new Dictionary<int, int>()
        {
            { 20, 18 },
            { 30, 27 },
            { 40, 37 },
        };
    */

    TextMeshProUGUI Text
    {
        get { return GameObject.Find("TextMeshPro Text").GetComponent<TextMeshProUGUI>(); }
    }

    IEnumerator Start()
    {
        Debug.Log("Creating tiles...");

        foreach (TilesetConfig tileset in config.tilesets)
        {
            Text.rectTransform.sizeDelta = new Vector2Int(tileset.pixelSize, tileset.pixelSize);
            Text.fontSize = tileset.fontSize;

            Texture2D tex = new Texture2D(tileset.pixelSize, tileset.pixelSize, TextureFormat.ARGB32, false);
                
            string characters = " •-+OX#";

            for (int i = 0; i < characters.Length; ++i)
            {
                string chr = characters[i].ToString();

                Debug.Log("Creating tile " + i + " '" + chr + "'");

                Text.SetText(chr);
                yield return new WaitForEndOfFrame();

                tex.ReadPixels(new Rect(0, 0, tileset.pixelSize, tileset.pixelSize), 0, 0);
                tex.Apply();

                byte[] bytes = tex.EncodeToPNG();
                File.WriteAllBytes(Application.dataPath + "/Textures/tiles/" + tileset.id + "/" + i + ".png", bytes);
            }
        }

        Debug.Log("Done creating tiles...");
        EditorApplication.isPlaying = false;
    }

}

