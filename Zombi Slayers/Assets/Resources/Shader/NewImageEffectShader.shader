Shader "Hidden/UIWhiteOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineSize ("Outline Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }

        Cull Off ZWrite Off ZTest Always Blend SrcAlpha OneMinusSrcAlpha

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
            float4 _MainTex_TexelSize; // otomatik geliyor: (1/width, 1/height, width, height)
            fixed4 _OutlineColor;
            float _OutlineSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Eðer alpha düþükse etrafýna bak (outline efekti)
                if (col.a < 0.1)
                {
                    // Kenarlarý kontrol et
                    float alphaAround = 0.0;
                    float2 offset = _OutlineSize * _MainTex_TexelSize.xy;

                    alphaAround += tex2D(_MainTex, i.uv + float2( offset.x, 0)).a;
                    alphaAround += tex2D(_MainTex, i.uv + float2(-offset.x, 0)).a;
                    alphaAround += tex2D(_MainTex, i.uv + float2(0,  offset.y)).a;
                    alphaAround += tex2D(_MainTex, i.uv + float2(0, -offset.y)).a;

                    if (alphaAround > 0.0)
                    {
                        return _OutlineColor;
                    }
                }

                return col;
            }
            ENDCG
        }
    }
}
