using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileColorMapper : ScriptableObject
{
    public List<TileColor> colors;

    [MenuItem("ASCII/Create TileColorMapper")]
    public static void CreateMyAsset()
    {
        TileColorMapper asset = ScriptableObject.CreateInstance<TileColorMapper>();

        AssetDatabase.CreateAsset(asset, "Assets/Teaser Trailer/New TileColorMapper.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    public Color GetTileColor(char c)
    {
        foreach (TileColor tileColor in colors)
        {
            if (tileColor.chr == c)
            {
                return tileColor.color;
            }
        }

        return Color.magenta;
    }
}


[System.Serializable]
public class TileColor
{
    public char chr;
    public Color color;
}

