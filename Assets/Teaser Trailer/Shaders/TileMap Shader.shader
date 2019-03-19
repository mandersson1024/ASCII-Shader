Shader "Unlit/TileMap Shader"
{
    Properties
    {
		_MainTex("Main Texture", 2D) = "white" {}
		_HiResBackgroundTex("Hi Res Background Texture", 2D) = "white" {}
		_LoResBackgroundTex("Lo Res Background Texture", 2D) = "white" {}
		_BackgroundBlend("Background Blend", Range(0, 1)) = 0.5
		_BackgroundIntensity("Background Intensity", Range(0, 1)) = 1
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
			sampler2D _HiResBackgroundTex;
			sampler2D _LoResBackgroundTex;
			fixed _BackgroundBlend;
			fixed _BackgroundIntensity;

            v2f vert (appdata v)
            {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 hiResBgCol = tex2D(_HiResBackgroundTex, i.uv);
				fixed4 loResBgCol = tex2D(_LoResBackgroundTex, i.uv);
				fixed4 bgCol = lerp(loResBgCol, hiResBgCol, _BackgroundBlend);
				bgCol *= _BackgroundIntensity;

				fixed4 mainCol = tex2D(_MainTex, i.uv);

				fixed4 col = saturate(bgCol + mainCol);
				
				return col;
            }
            ENDCG
        }
    }
}
