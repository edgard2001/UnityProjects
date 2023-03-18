Shader "Custom/Terrain Shader"
{
    Properties
    {
        _Color1 ("Color", Color) = (1,1,1,1)
        _Color2 ("Color", Color) = (1,1,1,1)
        _FloorTex ("Albedo (RGB)", 2D) = "white" {}
        _WallTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _FloorTex;
        sampler2D _WallTex;

        struct Input
        {
            float2 uv_FloorTex;
            float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color1;
        fixed4 _Color2;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 worldScale = float3(
                length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x)), // scale x axis
                length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y)), // scale y axis
                length(float3(unity_ObjectToWorld[0].z, unity_ObjectToWorld[1].z, unity_ObjectToWorld[2].z))  // scale z axis
            );

            // Albedo comes from a texture tinted by color
            fixed4 c;
            if (abs(IN.worldNormal.y) > 0)
                c = tex2D(_FloorTex, IN.uv_FloorTex * float2(worldScale.x, worldScale.z)) * _Color1;
            else if (IN.worldNormal.x > 0)
                c = tex2D(_WallTex, (float2(1, 1) - IN.uv_FloorTex) * float2(worldScale.z, worldScale.y)) * _Color2;
            else if (IN.worldNormal.x < 0)
                c = tex2D(_WallTex, (float2(0, 1) - IN.uv_FloorTex) * float2(worldScale.z, worldScale.y)) * _Color2;
            else
                c = tex2D(_WallTex, IN.uv_FloorTex * float2(worldScale.x, worldScale.y)) * _Color2;

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
