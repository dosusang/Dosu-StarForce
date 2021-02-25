Shader "Hidden/wakeng"
{
    Properties
    {
        _ColorBg("ColorBg", Color) = (1,1,1,1)
        _ColorHole("ColorHole", Color) = (1,1,1,0)
        _HoleUV("HoleUV", vector) = (0,0,0,0)
        _HoleR("HoleR", float) = 10
    }
    SubShader
    {
        Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "CanUseSpriteAtlas"="true" "PreviewType"="Plane" }
        Pass
        {
            ZWrite Off
            Cull Off
            Blend One OneMinusSrcAlpha
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

            float4 _HoleUV;
            float _HoleR;
            float4 _ColorBg;
            float4 _ColorHole;
            


            fixed4 frag (v2f i) : SV_Target
            {
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                if (distance(i.uv, _HoleUV.xy) <= _HoleR) {
                    return _ColorHole;
                }
                return _ColorBg;
            }
            ENDCG
        }
    }
}
