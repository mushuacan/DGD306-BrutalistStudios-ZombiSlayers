Shader "Custom/DamageEffectRed"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {} // SpriteRenderer'ýn ana dokusu
        _Color ("Tint Color", Color) = (1,1,1,1) // Varsayýlan sprite rengi
        _DamageColor ("Damage Color", Color) = (1,0,0,1) // Hasar rengi (mat kýrmýzý)
        _AlphaThreshold ("Alpha Threshold", Range(0, 1)) = 0.01 // Sprite'ýn ne kadar saydamlýðýný önemseyeceðimiz. Çok küçük bir deðer býrakalým.
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" // Saydam nesneler gibi renderla
            "RenderType"="Transparent" // Saydam render türü
            "RenderPipeline" = "UniversalPipeline" // URP kullanýyorsanýz bu önemli
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha // Normal alfa harmanlamasý (normalde bu olur)

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA // ETC1 için gerekli olabilir

            #include "UnityCG.cginc"
            #include "UnitySpritePixelSnap.cginc" // SpriteRenderer için gerekli

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            fixed4 _Color;
            fixed4 _DamageColor;
            float _AlphaThreshold;
            sampler2D _MainTex;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color; // SpriteRenderer'ýn kendi rengiyle çarp
                UNITY_APPLY_SPRITEPXSN_VP(OUT.vertex); // Pixel Perfect için gerekli
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, IN.texcoord); // Orijinal doku rengi
                texColor.rgb *= IN.color.rgb; // SpriteRenderer'ýn varsayýlan rengiyle çarp

                // Eðer pikselin orijinal alfa deðeri eþiðin altýndaysa (yani saydamsa)
                if (texColor.a < _AlphaThreshold)
                {
                    // Orayý _DamageColor'ýn RGB'si ve tam opak (1.0) alfa deðeriyle boya
                    return fixed4(_DamageColor.rgb, 1.0);
                }
                else
                {
                    // Aksi takdirde, orijinal pikselin rengini al
                    // Ve DamageColor'ýn RGB'sini ekle (burada alpha'yý da 1.0 yapýyoruz ki mat olsun)
                    // Veya sadece RGB'yi DamageColor ile deðiþtir, alpha'yý orijinal býrak.
                    // Seçenek 1: Orijinal rengi koru, sadece saydamlýðý gider (en iyisi)
                    // return fixed4(texColor.rgb, 1.0);
                    
                    // Seçenek 2: Orijinal rengi _DamageColor ile karýþtýr (daha kýzýl)
                    return fixed4(lerp(texColor.rgb, _DamageColor.rgb, _DamageColor.a), 1.0);
                }
            }
            ENDCG
        }
    }
}