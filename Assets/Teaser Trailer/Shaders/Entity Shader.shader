Shader "Unlit/Entity Shader"
{
    Properties
    {
		_MainTex("Main Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Glow("Glow", Range(0, 1)) = 1

	}
    SubShader
    {
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
		
		ZWrite Off
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha

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
			fixed4 _Color;
			fixed _Glow;

            v2f vert (appdata v)
            {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 inputCol = tex2D(_MainTex, i.uv);
				
				fixed4 fgCol = _Color * inputCol;
				fgCol.a = inputCol.r * _Color.a;

				fixed4 bgCol = _Color * _Glow;

				fixed4 col = lerp(bgCol, fgCol, inputCol.r);

				return col;
            }
            ENDCG
        }
    }
}
