Shader "Unlit/Lightning"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend One OneMinusSrcAlpha

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
                fixed4 color : COLOR0;
};

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float emission = 50.0;
                // sample the texture
                fixed2 UV= i.uv;
                UV.x /= 4;
                UV.x+=-(_Time.x*100.0)%4.0 *0.25;
                fixed4 col = tex2D(_MainTex, UV) * i.color;
                col.rbg *= emission;
                // apply fog
                return col;
            }
            ENDCG
        }
    }
}
