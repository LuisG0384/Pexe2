Shader "Custom/Pexe"
{
    Properties
    {
        _MainTex ("Textura do Peixe", 2D) = "white" {}
        _Color ("Cor Principal", Color) = (1,1,1,1)
        _Speed ("Velocidade do Nado", Range(0.1, 10)) = 5.0
        _Frequency ("Frequencia (Ondas)", Range(0.1, 10)) = 2.0
        _Amplitude ("Força do Movimento", Range(0.0, 2.0)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow
        
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        float _Speed;
        float _Frequency;
        float _Amplitude;

        struct Input
        {
            float2 uv_MainTex;
        };


        void vert (inout appdata_full v) {
            float wave = sin(v.vertex.x * _Frequency + _Time.y * _Speed) * _Amplitude;
            v.vertex.z += wave;
        }


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}