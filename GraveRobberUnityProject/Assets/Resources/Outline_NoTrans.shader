﻿Shader "Custom/Outline_NoTrans" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_InsetColor ("Inset Color", Color) = (1,1,1,1)
		_InsetAlpha ("Inset Alpha", Range (0.0, 1.0)) = 1
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_BumpMap ("Bumpmap", 2D) = "bump" {}
	}
 
CGINCLUDE
	#include "UnityCG.cginc"
	 
	//struct appdata {
	//	float4 vertex : POSITION;
	//	float3 normal : NORMAL;
	//};
	 
	//struct v2f {
	//	float4 pos : POSITION;
	//	float4 color : COLOR;
	//};
	 
	//uniform float _Outline;
	//uniform float4 _OutlineColor;
	 
	//v2f vert(appdata v) {
		// just make a copy of incoming vertex data but scaled according to normal direction
	//	v2f o;
	//o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	 
	//	float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	//	float2 offset = TransformViewToProjection(norm.xy);
	 
	//	o.pos.xy += offset * o.pos.z * _Outline;
	//    o.color = _OutlineColor;
	//	return o;
		
		
	//}
	
	
ENDCG
 
	SubShader {
		Tags { "RenderType" = "Opaque" }
 
		// note that a vertex shader is specified here but its using the one above
		//Pass {
		//	Name "OUTLINE"
		//	Tags { "LightMode" = "Always" }
		//	Cull Back
		//	ZWrite On
		//	ZTest LEqual
 
			// you can choose what kind of blending mode you want for the outline
			//Blend SrcAlpha OneMinusSrcAlpha // Normal
			//Blend One One // Additive
			//Blend One OneMinusDstColor // Soft Additive
			//Blend DstColor Zero // Multiplicative
			//Blend DstColor SrcColor // 2x Multiplicative
 
			//CGPROGRAM
			//	#pragma vertex vert
			//	#pragma fragment frag
			//	 
			//	half4 frag(v2f i) : COLOR {
			//		return i.color;
			//	}
			//ENDCG
		//}
 
 
		CGPROGRAM
			#pragma surface surf Lambert
			struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
				float3 viewDir;
			};
			sampler2D _MainTex;
			sampler2D _BumpMap;
			uniform float3 _Color;
			uniform float3 _InsetColor;
			uniform half _InsetAlpha;
			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
				half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          		//o.Emission = (1.0,1.0,1.0) * pow (rim, 3.0);
          		o.Emission = _InsetColor * pow (rim, 3.0) * _InsetAlpha;
			}
		ENDCG
 
	}
 
	Fallback "Diffuse"
}
