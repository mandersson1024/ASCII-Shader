Shader "Unlit/TileMap Shader"
{
    Properties
    {
		_MainTex("Main Texture", 2D) = "white" {}

		_HiResBackgroundTex("Hi Res Background Texture", 2D) = "white" {}
		_LoResBackgroundTex("Lo Res Background Texture", 2D) = "white" {}
		_BackgroundBlend("Background Blend", Range(0, 1)) = 0.5
		_BackgroundIntensity("Background Intensity", Range(0, 1)) = 1
		
		_HiResForegroundTex("Hi Res Foreground Texture", 2D) = "white" {}
		_LoResForegroundTex("Lo Res Foreground Texture", 2D) = "white" {}
		_ForegroundBlend("Foreground Blend", Range(0, 1)) = 0.5
		_ForegroundIntensity("Foreground Intensity", Range(0, 1)) = 1
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
			sampler2D _HiResForegroundTex;
			sampler2D _LoResForegroundTex;
			fixed _BackgroundBlend;
			fixed _BackgroundIntensity;
			fixed _ForegroundBlend;
			fixed _ForegroundIntensity;

            v2f vert (appdata v)
            {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 mainCol = tex2D(_MainTex, i.uv);

				fixed4 hiResBgCol = tex2D(_HiResBackgroundTex, i.uv);
				fixed4 loResBgCol = tex2D(_LoResBackgroundTex, i.uv);
				fixed4 bgCol = lerp(loResBgCol, hiResBgCol, _BackgroundBlend);
				bgCol *= _BackgroundIntensity;

				fixed4 hiResFgCol = tex2D(_HiResForegroundTex, i.uv);
				fixed4 loResFgCol = tex2D(_LoResForegroundTex, i.uv);
				fixed4 fgCol = lerp(loResFgCol, hiResFgCol, _ForegroundBlend);
				fgCol = lerp(mainCol, fgCol, _ForegroundIntensity);

				fixed4 col = lerp(bgCol, fgCol, mainCol);
				
				return col;
            }
            ENDCG
        }
    }
}
