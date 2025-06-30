Shader "Custom/DamageEffectRed"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {} // SpriteRenderer'�n ana dokusu
        _Color ("Tint Color", Color) = (1,1,1,1) // Varsay�lan sprite rengi
        _DamageColor ("Damage Color", Color) = (1,0,0,1) // Hasar rengi (mat k�rm�z�)
        _AlphaThreshold ("Alpha Threshold", Range(0, 1)) = 0.01 // Sprite'�n ne kadar saydaml���n� �nemseyece�imiz. �ok k���k bir de�er b�rakal�m.
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent" // Saydam nesneler gibi renderla
            "RenderType"="Transparent" // Saydam render t�r�
            "RenderPipeline" = "UniversalPipeline" // URP kullan�yorsan�z bu �nemli
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha // Normal alfa harmanlamas� (normalde bu olur)

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA // ETC1 i�in gerekli olabilir

            #include "UnityCG.cginc"
            #include "UnitySpritePixelSnap.cginc" // SpriteRenderer i�in gerekli

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
                OUT.color = IN.color * _Color; // SpriteRenderer'�n kendi rengiyle �arp
                UNITY_APPLY_SPRITEPXSN_VP(OUT.vertex); // Pixel Perfect i�in gerekli
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, IN.texcoord); // Orijinal doku rengi
                texColor.rgb *= IN.color.rgb; // SpriteRenderer'�n varsay�lan rengiyle �arp

                // E�er pikselin orijinal alfa de�eri e�i�in alt�ndaysa (yani saydamsa)
                if (texColor.a < _AlphaThreshold)
                {
                    // Oray� _DamageColor'�n RGB'si ve tam opak (1.0) alfa de�eriyle boya
                    return fixed4(_DamageColor.rgb, 1.0);
                }
                else
                {
                    // Aksi takdirde, orijinal pikselin rengini al
                    // Ve DamageColor'�n RGB'sini ekle (burada alpha'y� da 1.0 yap�yoruz ki mat olsun)
                    // Veya sadece RGB'yi DamageColor ile de�i�tir, alpha'y� orijinal b�rak.
                    // Se�enek 1: Orijinal rengi koru, sadece saydaml��� gider (en iyisi)
                    // return fixed4(texColor.rgb, 1.0);
                    
                    // Se�enek 2: Orijinal rengi _DamageColor ile kar��t�r (daha k�z�l)
                    return fixed4(lerp(texColor.rgb, _DamageColor.rgb, _DamageColor.a), 1.0);
                }
            }
            ENDCG
        }
    }
}