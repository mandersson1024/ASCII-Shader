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
		_LoResAlpha("Lo Res Alpha", Range(0, 1)) = 1
		_HiResAlpha("Hi Res Alpha", Range(0, 1)) = 1
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
			float _LoResAlpha;
			float _HiResAlpha;

			/*
				vec3 rgb2hsv(vec3 c)
				{
					vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
					vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
					vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));

					float d = q.x - min(q.w, q.y);
					float e = 1.0e-10;
					return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
				}

				vec3 hsv2rgb(vec3 c)
				{
					vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
					vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
					return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
				}

				vec3 hsv2rgb(vec3 c)
				{
					vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
					vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
					return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
				}			
			*/

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_ScaledTex, i.uv);
				fixed lum = Luminance(col);
				
				float3 uvz;
				uvz.x = i.uv.x * _TilesX;
				uvz.y = i.uv.y * _TilesY;
				uvz.z = lum * _TileArraySize;
				
				fixed4 result = col;
				//result.rgb = UNITY_SAMPLE_TEX2DARRAY(_Tiles, uvz);
				result.rgb = UNITY_SAMPLE_TEX2DARRAY(_Tiles, uvz) * col;
				result = lerp(col, result, _LoResAlpha);
				
				fixed4 hiResCol = tex2D(_MainTex, i.uv);
				result = lerp(hiResCol, result, _HiResAlpha);

                return result;
            }
            ENDCG
        }
    }
}
