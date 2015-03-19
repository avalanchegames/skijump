// Adapted from Normal Mapping example on http://docs.unity3d.com/Manual/SL-SurfaceShaderExamples.html (19/03/2015)

Shader "Blanking/Diffuse Bump" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
        float2 uv_MainTex;
        float2 uv_BumpMap;
      };
      sampler2D _MainTex;
      sampler2D _BumpMap;
      
      
      #define TARGETFPS 60
		
      
      void surf (Input IN, inout SurfaceOutput o) 
      {
      	if (unity_DeltaTime.y < TARGETFPS)
		{
			half4 blank = { 0.0f, 0.0f, 0.0f, 1.0f };
			o.Albedo = blank.rgb;
			o.Alpha = blank.a;
		}
		else
		{
	        o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
	        o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
	    }
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }