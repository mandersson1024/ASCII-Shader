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

    [Range(0, 1)]
    public float loResAlpha = 1;

    [Range(0, 1)]
    public float hiResAlpha = 1;


    // Start is called before the first frame update
    void Start()
    {
        material.SetInt("_TileArraySize", 5);
    }

    // Update is called once per frame
    void Update()
    {
        material.SetInt("_TilesX", tilesX);
        material.SetInt("_TilesY", tilesY);
        material.SetFloat("_LoResAlpha", loResAlpha);
        material.SetFloat("_HiResAlpha", hiResAlpha);

        /*
        _MainTex("Texture", 2D) = "white" {}
		_ScaledTex("Scaled Texture", 2D) = "white" {}
		_Tiles("Tiles", 2DArray) = "" {}
		_TileArraySize("Tiles Array Size", int) = 0
		_TilesX("Tiles X", int) = 0
		_TilesY("Tiles Y", int) = 0
		_LoResAlpha("Lo Res Alpha", Range(0, 1)) = 1
		_HiResAlpha("Hi Res Alpha", Range(0, 1)) = 1 
         */
    }

    public void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        RenderTexture scaled = RenderTexture.GetTemporary(tilesX, tilesY);
        scaled.filterMode = FilterMode.Point;
        src.filterMode = FilterMode.Point;

        Graphics.Blit(src, scaled);
        material.SetTexture("_ScaledTex", scaled);

        Graphics.Blit(src, dest, material);
        RenderTexture.ReleaseTemporary(scaled);
    }
}
