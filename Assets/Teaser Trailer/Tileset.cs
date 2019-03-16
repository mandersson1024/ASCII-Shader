using UnityEngine;
using UnityEditor;

public class Tileset : ScriptableObject
{
    public Texture2D atlas;
    public int tilePixelSize = 64;

    [MenuItem("ASCII/Create/Tileset")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Tileset>();
    }

    public void DrawTile(Texture2D destination, int tileIndex, int x, int y)
    {
        int tileX = (tileIndex % 16) * tilePixelSize;
        int tileY = (15 - (tileIndex / 16)) * tilePixelSize;
        Debug.Log("tileX=" + tileX + ", tileY=" + tileY);
        Graphics.CopyTexture(atlas, 0, 0, tileX, tileY, tilePixelSize, tilePixelSize, destination, 0, 0, x, y);
    }

}
