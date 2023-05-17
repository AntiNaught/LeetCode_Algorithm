/*
Shader "Custom/Lambert_ChitGPT"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
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

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
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
*/

Shader "Custom/Lambert_ChitGPT" {
    Properties 
    {
        _Color ("Color", Color) = (1, 1, 1, 1)    
    }    
    SubShader 
    {        
        Tags {"RenderType"="Opaque"}        
        CGPROGRAM

fixed4 _Color;
        //#pragma surface surf Lambert
        #pragma surface surf Standard fullforwardshadows    
        //定义表面属性        
        struct Input 
        {            
            float2 uv_MainTex;            
            float3 worldPos;        
        };

        //定义表面函数        
        void surf (Input IN, inout SurfaceOutputStandard o) 
        {            
            //设置基本属性            
            o.Albedo = _Color.rgb;            
            o.Metallic = 0.0;            
            o.Smoothness = 0.5;            
            o.Normal = float3(0,0,1);            
            //计算光照            
            float3 lightDir = normalize(_WorldSpaceLightPos0.xyz - IN.worldPos);            float diffuse = max(0, dot(IN.worldNormal, lightDir));            
            o.Emission = _Color.rgb * diffuse;        
        }
 
        ENDCG    
    }    
    FallBack "Diffuse"
}