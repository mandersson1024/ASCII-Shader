using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ImageEffect : MonoBehaviour
{
    public Material material;

    public int tilesX = 64;
    public int tilesY = 36;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture scaled = RenderTexture.GetTemporary(64, 36);
        scaled.filterMode = FilterMode.Point;
        src.filterMode = FilterMode.Point;

        Graphics.Blit(src, scaled);
        material.SetInt("_TileArraySize", 5);
        material.SetInt("_TilesX", tilesX);
        material.SetInt("_TilesY", tilesY);
        material.SetTexture("_ScaledTex", scaled);

        Graphics.Blit(src, dest, material);
        RenderTexture.ReleaseTemporary(scaled);
    }
}
