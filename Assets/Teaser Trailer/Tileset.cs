using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tileset
{
    const int screenPixelHeight = 1080;
    const int tilePixelSize = 64;
    const int numAtlasTiles = 16;

    Texture2D atlas;

    public Tileset(Texture2D atlas)
    {
        this.atlas = atlas;
    }

    public void DrawTile(Texture2D destination, int tileIndex, int x, int y)
    {
        int srcX = tileIndex % numAtlasTiles;
        int srcY = tileIndex / numAtlasTiles;
        Graphics.CopyTexture(atlas, 0, 0, srcX * tilePixelSize, (atlas.height - tilePixelSize) - srcY * tilePixelSize, tilePixelSize, tilePixelSize, destination, 0, 0, x * tilePixelSize, (screenPixelHeight - tilePixelSize) - (y * tilePixelSize));
    }

}
