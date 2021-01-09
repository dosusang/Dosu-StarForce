Shader "Hidden/Dispel"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Noice ("NoiceTex", 2D) = "white" {}
        _RatioUp ("RatioUp", Range(0,1)) = 0.8
        _EdgeColor ("EdgeColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth
        //Cull Off ZWrite Off ZTest Always

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
            sampler2D _Noice;
            float _RatioUp;
            fixed4 _EdgeColor;


            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // just invert the colors
                float gray = Luminance(tex2D(_Noice, i.uv));
                if (gray > _RatioUp){
                    clip(-1);
                }
                if (gray > _RatioUp - 0.15) {
                    col.rgb = col.rgb * (-(gray - _RatioUp) * 15);
                    col =col * _EdgeColor;
                }
                return col;
            }
            ENDCG
        }
    }
}
