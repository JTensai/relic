// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4178,x:33405,y:32655,varname:node_4178,prsc:2|diff-6364-RGB,emission-7764-OUT,alpha-5767-OUT,clip-2000-OUT;n:type:ShaderForge.SFN_Panner,id:2680,x:31809,y:32576,varname:node_2680,prsc:2,spu:0,spv:1.5;n:type:ShaderForge.SFN_Tex2d,id:6364,x:32449,y:32593,ptovrint:False,ptlb:Diffuse Map,ptin:_DiffuseMap,varname:node_6364,prsc:2,tex:69be950c0a6be44419253999c4665e2f,ntxv:0,isnm:False|UVIN-4928-OUT;n:type:ShaderForge.SFN_Ceil,id:2000,x:32712,y:32873,varname:node_2000,prsc:2|IN-9384-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4853,x:31990,y:32576,varname:node_4853,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-2680-UVOUT;n:type:ShaderForge.SFN_Multiply,id:9665,x:32209,y:32841,varname:node_9665,prsc:2|A-4853-R,B-1747-OUT;n:type:ShaderForge.SFN_Vector1,id:1747,x:32013,y:32960,varname:node_1747,prsc:2,v1:1;n:type:ShaderForge.SFN_Append,id:4928,x:32264,y:32593,varname:node_4928,prsc:2|A-9665-OUT,B-4853-G;n:type:ShaderForge.SFN_Add,id:982,x:32469,y:32994,varname:node_982,prsc:2|A-6364-R,B-4240-OUT;n:type:ShaderForge.SFN_Clamp01,id:9384,x:32641,y:33052,varname:node_9384,prsc:2|IN-982-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9790,x:32028,y:33147,ptovrint:False,ptlb:Clip Threshold,ptin:_ClipThreshold,varname:node_9790,prsc:2,glob:False,v1:0.1;n:type:ShaderForge.SFN_Negate,id:4240,x:32264,y:33053,varname:node_4240,prsc:2|IN-9790-OUT;n:type:ShaderForge.SFN_Color,id:5293,x:32712,y:32683,ptovrint:False,ptlb:Emission Color,ptin:_EmissionColor,varname:node_5293,prsc:2,glob:False,c1:0.3,c2:0.9,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:7764,x:32902,y:32768,varname:node_7764,prsc:2|A-5293-RGB,B-2000-OUT,C-3564-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3564,x:32527,y:32805,ptovrint:False,ptlb:Emission Intensity,ptin:_EmissionIntensity,varname:node_3564,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:6128,x:32595,y:33221,varname:node_6128,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:325,x:32789,y:33221,varname:node_325,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-6128-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4604,x:32993,y:33182,varname:node_4604,prsc:2|A-325-OUT,B-2000-OUT;n:type:ShaderForge.SFN_Power,id:1390,x:33163,y:33135,varname:node_1390,prsc:2|VAL-4604-OUT,EXP-4927-OUT;n:type:ShaderForge.SFN_Vector1,id:4927,x:32993,y:33311,varname:node_4927,prsc:2,v1:1.8;n:type:ShaderForge.SFN_Multiply,id:5767,x:33203,y:32978,varname:node_5767,prsc:2|A-1390-OUT,B-6967-OUT;n:type:ShaderForge.SFN_Slider,id:6967,x:32807,y:33031,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_6967,prsc:2,min:0,cur:0.5,max:1;proporder:6364-9790-5293-3564-6967;pass:END;sub:END;*/

Shader "Shader Forge/ToxicWaterfall" {
    Properties {
        _DiffuseMap ("Diffuse Map", 2D) = "white" {}
        _ClipThreshold ("Clip Threshold", Float ) = 0.1
        _EmissionColor ("Emission Color", Color) = (0.3,0.9,0,1)
        _EmissionIntensity ("Emission Intensity", Float ) = 1
        _Opacity ("Opacity", Range(0, 1)) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float _ClipThreshold;
            uniform float4 _EmissionColor;
            uniform float _EmissionIntensity;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float4 node_3959 = _Time + _TimeEditor;
                float2 node_4853 = (i.uv0+node_3959.g*float2(0,1.5)).rg;
                float2 node_4928 = float2((node_4853.r*1.0),node_4853.g);
                float4 _DiffuseMap_var = tex2D(_DiffuseMap,TRANSFORM_TEX(node_4928, _DiffuseMap));
                float node_2000 = ceil(saturate((_DiffuseMap_var.r+(-1*_ClipThreshold))));
                clip(node_2000 - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _DiffuseMap_var.rgb;
////// Emissive:
                float3 emissive = (_EmissionColor.rgb*node_2000*_EmissionIntensity);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(finalColor,(pow((i.uv0.g*node_2000),1.8)*_Opacity));
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float _ClipThreshold;
            uniform float4 _EmissionColor;
            uniform float _EmissionIntensity;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float4 node_2170 = _Time + _TimeEditor;
                float2 node_4853 = (i.uv0+node_2170.g*float2(0,1.5)).rg;
                float2 node_4928 = float2((node_4853.r*1.0),node_4853.g);
                float4 _DiffuseMap_var = tex2D(_DiffuseMap,TRANSFORM_TEX(node_4928, _DiffuseMap));
                float node_2000 = ceil(saturate((_DiffuseMap_var.r+(-1*_ClipThreshold))));
                clip(node_2000 - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * _DiffuseMap_var.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * (pow((i.uv0.g*node_2000),1.8)*_Opacity),0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float _ClipThreshold;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 node_5240 = _Time + _TimeEditor;
                float2 node_4853 = (i.uv0+node_5240.g*float2(0,1.5)).rg;
                float2 node_4928 = float2((node_4853.r*1.0),node_4853.g);
                float4 _DiffuseMap_var = tex2D(_DiffuseMap,TRANSFORM_TEX(node_4928, _DiffuseMap));
                float node_2000 = ceil(saturate((_DiffuseMap_var.r+(-1*_ClipThreshold))));
                clip(node_2000 - 0.5);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform float _ClipThreshold;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 node_7321 = _Time + _TimeEditor;
                float2 node_4853 = (i.uv0+node_7321.g*float2(0,1.5)).rg;
                float2 node_4928 = float2((node_4853.r*1.0),node_4853.g);
                float4 _DiffuseMap_var = tex2D(_DiffuseMap,TRANSFORM_TEX(node_4928, _DiffuseMap));
                float node_2000 = ceil(saturate((_DiffuseMap_var.r+(-1*_ClipThreshold))));
                clip(node_2000 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
