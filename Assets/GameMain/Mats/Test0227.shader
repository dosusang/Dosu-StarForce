// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/Test0227"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Diffuse ("Diffuse", Color) = (1,1,1,1)
        _Specular("Specular", Color) = (1,1,1,1)
        _Gloss("Gloss", range(8,256)) = 20
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed3 _Diffuse;
            fixed3 _Specular;
            float _Gloss;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal =  normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                fixed3 normal_world = i.normal;
                fixed3 light_world = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 diffuse = fixed3(1,1,1) * _Diffuse.rgb * saturate(dot(normal_world, light_world));
                col.rbg *= diffuse.rbg;
                
                fixed3 reflectLightDir = normalize(reflect(-light_world, normal_world));
                fixed3 specular = fixed3(1,1,1) * _Diffuse.rgb * pow(saturate(dot(reflectLightDir, normal_world)) , _Gloss);
                col.rgb += specular;
                return col;
            }
            ENDCG
        }
    }
}
