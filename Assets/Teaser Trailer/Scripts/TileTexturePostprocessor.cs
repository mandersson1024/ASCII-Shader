using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileTexturePostprocessor : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        TileMap tilemap = GameObject.Find("TileMap").GetComponent<TileMap>();
        tilemap.RefreshTileTexture();
    }

}
