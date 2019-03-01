Shader "Hidden/Image Effect"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
		_ScaledTex("Scaled Texture", 2D) = "white" {}
		_Tiles("Tiles", 2DArray) = "" {}
		_TileArraySize("Tiles Array Size", int) = 0
		_TilesX("Tiles X", int) = 0
		_TilesY("Tiles Y", int) = 0
		_Alpha("Alpha", Range(0, 1)) = 1
	}
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			sampler2D _MainTex;
			sampler2D _ScaledTex;
			UNITY_DECLARE_TEX2DARRAY(_Tiles);
			int _TileArraySize;
			int _TilesX;
			int _TilesY;
			float _Alpha;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_ScaledTex, i.uv);
				fixed lum = Luminance(col);
				
				float3 uvz;
				uvz.x = i.uv.x * _TilesX;
				uvz.y = i.uv.y * _TilesY;
				uvz.z = lum * _TileArraySize;
				
				fixed4 result = col;
				result.rgb = UNITY_SAMPLE_TEX2DARRAY(_Tiles, uvz);
				result = lerp(col, result, _Alpha);

                return result;
            }
            ENDCG
        }
    }
}
