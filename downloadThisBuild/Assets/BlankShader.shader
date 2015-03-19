Shader "Custom/BlankShader" 
{
	SubShader 
	{
		Pass
		{
			CGPROGRAM
			
			#pragma vertex vert
            #pragma fragment frag
			
			float4 vert(float4 v:POSITION) : SV_POSITION 
			{
                return float4(0.0f, 0.0f, 0.0f, 1.0f);
            }
            
			fixed4 frag() : SV_Target 
			{
                return fixed4(0.0,0.0,0.0,1.0);
            }
			ENDCG
		}
	}
}
