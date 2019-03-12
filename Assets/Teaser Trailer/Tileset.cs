using UnityEngine;
using UnityEditor;

public class Tileset : ScriptableObject
{
    public Texture2D atlas;
    public int tileSize = 64;

    [MenuItem("ASCII/Create/Tileset")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<Tileset>();
    }

    public void DrawTile(Texture2D destination, Vector2Int tileIndex, int x, int y)
    {
        int tileX = tileIndex.x * tileSize;
        int tileY = (atlas.height - tileSize) - tileIndex.y * tileSize;
        Graphics.CopyTexture(atlas, 0, 0, tileX, tileY, tileSize, tileSize, destination, 0, 0, x, y);
    }

}
