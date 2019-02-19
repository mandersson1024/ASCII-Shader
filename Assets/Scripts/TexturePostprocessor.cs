using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TexturePostprocessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        //textureImporter.spritePixelsPerUnit = 10;
    }
}
