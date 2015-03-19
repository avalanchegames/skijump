// Adapted from Texture example on http://docs.unity3d.com/Manual/SL-SurfaceShaderExamples.html (18/03/2015)

Shader "Blanking/Diffuse" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		
		
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
				half4 c = tex2D (_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
