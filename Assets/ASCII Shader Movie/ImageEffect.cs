using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ImageEffect : MonoBehaviour
{
    public Sprite spriteSheet;
    public Material material;

    public int tilesX = 64;
    public int tilesY = 36;

    [Range(0, 1)]
    public float loResAlpha = 1;

    [Range(0, 1)]
    public float hiResAlpha = 1;

    [Range(0, 10)]
    public float characterBrightness = 0;


    // Start is called before the first frame update
    void Start()
    {
        //material.SetTexture("_Tiles", spriteSheet.texture); does not work
        material.SetInt("_TileArraySize", spriteSheet.texture.width / spriteSheet.texture.height);

        tilesX = 32;
        tilesY = 18;

        //tilesX = 64;
        //tilesY = 36;

        //tilesX = 128;
        //tilesY = 72;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tilesX = 16;
            tilesY = 9;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tilesX = 32;
            tilesY = 18;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            tilesX = 64;
            tilesY = 36;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            tilesX = 128;
            tilesY = 72;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            tilesX = 192;
            tilesY = 108;
        }

        material.SetInt("_TilesX", tilesX);
        material.SetInt("_TilesY", tilesY);
        material.SetFloat("_LoResAlpha", loResAlpha);
        material.SetFloat("_HiResAlpha", hiResAlpha);
        material.SetFloat("_CharacterBrightness", characterBrightness);

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
