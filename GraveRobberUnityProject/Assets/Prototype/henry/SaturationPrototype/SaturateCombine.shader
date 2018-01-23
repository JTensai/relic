Shader "Custom/SaturateCombine" {
	Properties {
		_Ambient ("Ambient (RGB)", 2D) = "white" {}
		_Lit ("Lit (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _Ambient;
		sampler2D _Lit;

		struct Input {
			float2 uv_Ambient;
			float2 uv_Lit;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 ambientColor = tex2D (_Ambient, IN.uv_Ambient);
			half4 litColor = tex2D (_Lit, IN.uv_Lit);
			
			half grey = (ambientColor.r * 0.333 + ambientColor.g * 0.333 + ambientColor.b * 0.333);
			half lightAmount = saturate(litColor.r * 0.333 + litColor.g * 0.333 + litColor.b * 0.333);
			
			o.Emission = ambientColor.rgb * lightAmount + half3(grey, grey, grey) * (1 - lightAmount);
			o.Alpha = 1;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
