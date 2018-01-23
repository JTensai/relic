Shader "Custom/Dissolve" {
	Properties
	{
		_MainTex("Main Texture", 2D) = "white"{}
		_DissolveMap("Dissolve Shape", 2D) = "white"{}
		
		_DissolveVal("Dissolve Value", Range(-0.2, 1.2)) = 1.2
		_LineWidth("Line Width", Range(0.0, 0.2)) = 0.1
		
		_LineColor("Line Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf Lambert
		
		sampler2D _MainTex;
		sampler2D _DissolveMap;
		
		float4 _LineColor;
		float _DissolveVal;
		float _LineWidth;
		
		struct Input 
		{
     			half2 uv_MainTex;
     			half2 uv_DissolveMap;
    		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;

			half4 dissolve = tex2D(_DissolveMap, IN.uv_DissolveMap);

			half4 clear = half4(0.0);

		int isClear = int(dissolve.r - (_DissolveVal + _LineWidth) + 0.99);
		int isAtLeastLine = int(dissolve.r - (_DissolveVal) + 0.99);

			half4 altCol = lerp(_LineColor, clear, isClear);

			o.Albedo = lerp(o.Albedo, altCol, isAtLeastLine);
			
			o.Alpha = lerp(1.0, 0.0, isClear);
			
		}
		ENDCG
	}
}
