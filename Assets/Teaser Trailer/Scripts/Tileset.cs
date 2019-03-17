using UnityEngine;
using UnityEditor;

public class Tileset
{
    readonly Texture2D atlas;
    readonly public int tileSizePixels;

    public Tileset(Texture2D atlas, int tileSizePixels)
    {
        this.atlas = atlas;
        this.tileSizePixels = tileSizePixels;
    }

    public void DrawTile(Texture2D destination, int tileIndex, int x, int y)
    {
        int tileX = (tileIndex % 16) * tileSizePixels;
        int tileY = (15 - (tileIndex / 16)) * tileSizePixels;
        //Debug.Log("tileX=" + tileX + ", tileY=" + tileY);
        Graphics.CopyTexture(atlas, 0, 0, tileX, tileY, tileSizePixels, tileSizePixels, destination, 0, 0, x, y);
    }

}
