using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Config : ScriptableObject
{
    public List<TilesetConfig> tilesets;

    [MenuItem("ASCII/Create Config Scriptable Object")]
    public static void CreateMyAsset()
    {
        Config asset = ScriptableObject.CreateInstance<Config>();
        AssetDatabase.CreateAsset(asset, "Assets/New Config.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
