Shader "Outlined/Silhouetted Diffuse" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (0.0, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" { }
	}
 
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
        Fog { Mode Off }
 
		// note that a vertex shader is specified here but its using the one above
		Pass 
		{
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Off
			ZWrite Off
			ZTest Always
			ColorMask RGB // alpha not used
 
			// you can choose what kind of blending mode you want for the outline
			Blend SrcAlpha OneMinusSrcAlpha // Normal
			//Blend One One // Additive
			//Blend One OneMinusDstColor // Soft Additive
			//Blend DstColor Zero // Multiplicative
			//Blend DstColor SrcColor // 2x Multiplicative
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
 
			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			 
			struct v2f {
				float4 pos : POSITION;
				float4 color : COLOR;
			};
			 
			uniform float _Outline;
			uniform float4 _OutlineColor;
			 
			v2f vert(appdata v) {
				// just make a copy of incoming vertex data but scaled according to normal direction
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			 
				float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float2 offset = TransformViewToProjection(norm.xy);
			 
				o.pos.xy += offset * o.pos.z * _Outline;
				o.color = _OutlineColor;
				return o;
			}
			 
			half4 frag(v2f i) :COLOR 
			{
				return i.color;
			}
			ENDCG
		}
 
		Pass 
		{
            Name "FORWARD"
            Tags { "LightMode" = "ForwardBase" }
           
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"               
   
 
                struct v2f {
                    half4   pos : SV_POSITION;
                    half2   uv : TEXCOORD0;
                    fixed3  vlight : TEXCOORD1;
                };
               
                v2f vert (appdata_full v)
                {
                    v2f o;
                    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
                    o.uv = v.texcoord;
                    half3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);
                    half3 shlight = ShadeSH9 (float4(worldN,1.0));
                    o.vlight = shlight;
                    return o;
                }
               
                sampler2D _MainTex;
               
                fixed4 frag (v2f i) : COLOR
                {
                    fixed4 mainTextColor = tex2D(_MainTex,i.uv);
                    fixed4 c;
                    c.rgb = i.vlight;
                    return mainTextColor*c;
                }
            ENDCG
        }
	}
 
 
	Fallback "Diffuse"
}