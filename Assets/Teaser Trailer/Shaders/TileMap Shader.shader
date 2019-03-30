Shader "Unlit/TileMap Shader"
{
    Properties
    {
		_MainTex("Raw Tile Texture", 2D) = "white" {}
		_TileColorTex("Tile Color Texture", 2D) = "white" {}
		_BackgroundColorTex("Background Color Texture", 2D) = "white" {}
	}
    SubShader
    {
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }

		ZWrite Off
		Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

			sampler2D _MainTex;
			sampler2D _TileColorTex;
			sampler2D _BackgroundColorTex;

            v2f vert (appdata v)
            {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 rawTileCol = tex2D(_MainTex, i.uv);
				fixed4 tileCol = tex2D(_TileColorTex, i.uv);
				fixed4 bgCol = tex2D(_BackgroundColorTex, i.uv);

				fixed4 col = lerp(bgCol, tileCol, rawTileCol);
				
				return col;
            }
            ENDCG
        }
    }
}
