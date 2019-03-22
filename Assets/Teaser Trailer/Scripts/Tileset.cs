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
        RectInt rect = GetRectForTileIndex(tileIndex);
        Graphics.CopyTexture(atlas, 0, 0, rect.x, rect.y, rect.width, rect.height, destination, 0, 0, x, y);
    }

    public RectInt GetRectForTileIndex(int index)
    {
        int tileX = (index % 16) * tileSizePixels;
        int tileY = (15 - (index / 16)) * tileSizePixels;
        return new RectInt(tileX, tileY, tileSizePixels, tileSizePixels);
    }

}
