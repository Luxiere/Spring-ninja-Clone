Shader "Unlit/Wave"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
        _Freq ("Frequency", Range(0, 10)) = 1
        _Amp ("Amplitude", Range(0, 10)) = 1
        _Speed ("Speed", float) = 10
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

            float4 _MainTex_ST;
            float4 _Color;
            float _Freq;
            float _Amp;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float waveHeight = sin((o.vertex.x + _Time.x * _Speed) * _Freq) * _Amp + sin((o.vertex.x + _Time.x * _Speed * 1.5) * _Freq * 1.5) * _Amp;
                o.vertex.y += waveHeight;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
                return col;
            }
            ENDCG
        }
    }
}
