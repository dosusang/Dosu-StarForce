﻿Shader "Unlit/Test0215"
{
    Properties
    {
        _MainTex("MainTex", 2D) = "white" {}
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

            sampler2D _MainTex;

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

            float circle(float2 uv, float2 pos, float size) {
                float d = length(uv - pos);
                return smoothstep(size, size - 0.2, d) * smoothstep(size - 0.05, size + 0.05, d) * 100;
            }

            float circle2(float2 uv, float2 pos, float size) {
                float d = length(uv - pos);
                float m1 = clamp(0,1,size / d);
                m1 = 4*m1 -3;
                m1 -= smoothstep(size, size - 0.1, d);
                return m1;
            }



            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv - 0.5;
                // sample the t
                fixed4 col = float4(0,0,0,1);
                uv.x *= -1;
                float ratio = abs(uv.x - uv.y) * 3.5;
                ratio  = smoothstep(0.6, 1, ratio);
                col.r  = circle2(uv, float2(0, 0), 0.2);
                col.g  = circle2(uv, float2(0, 0), 0.18);
                col.b  = circle2(uv, float2(0, 0), 0.16);
                col.rgb  += circle(uv, float2(0, 0), 0.19) * 0.3;
                //float r = _Time.y % 1;
                //col += tex2D(_MainTex, uv -= circle(uv, float2(0.5, 0.5),  r * 0.5 ) * 0.01/r); 
                
                //col += circle2(uv, float2(0,0), 0.2);

                return col;
            }
            ENDCG
        }
    }
}
