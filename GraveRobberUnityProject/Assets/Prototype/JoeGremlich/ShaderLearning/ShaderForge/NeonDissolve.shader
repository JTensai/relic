// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:5613,x:33524,y:32613,varname:node_5613,prsc:2|diff-5989-RGB,clip-5165-OUT;n:type:ShaderForge.SFN_Tex2d,id:758,x:32593,y:32441,ptovrint:False,ptlb:Clouds1,ptin:_Clouds1,varname:node_758,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-1614-UVOUT;n:type:ShaderForge.SFN_Color,id:5989,x:33004,y:32636,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5989,prsc:2,glob:False,c1:1,c2:0,c3:0.7655172,c4:1;n:type:ShaderForge.SFN_Tex2d,id:8818,x:32593,y:32622,ptovrint:False,ptlb:Clouds2,ptin:_Clouds2,varname:node_8818,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-3896-UVOUT;n:type:ShaderForge.SFN_Panner,id:1614,x:32377,y:32427,varname:node_1614,prsc:2,spu:0.05,spv:0.05;n:type:ShaderForge.SFN_Panner,id:3896,x:32390,y:32622,varname:node_3896,prsc:2,spu:-0.05,spv:-0.05;n:type:ShaderForge.SFN_Time,id:4763,x:32275,y:32779,varname:node_4763,prsc:2;n:type:ShaderForge.SFN_Sin,id:6937,x:32642,y:32828,varname:node_6937,prsc:2|IN-5668-OUT;n:type:ShaderForge.SFN_RemapRange,id:1908,x:32818,y:32821,varname:node_1908,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1.2|IN-6937-OUT;n:type:ShaderForge.SFN_Multiply,id:5668,x:32470,y:32828,varname:node_5668,prsc:2|A-4763-T,B-9253-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9253,x:32275,y:32927,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_9253,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:6105,x:32818,y:32698,varname:node_6105,prsc:2|A-758-R,B-8818-R;n:type:ShaderForge.SFN_Subtract,id:5165,x:33127,y:32920,varname:node_5165,prsc:2|A-1908-OUT,B-6105-OUT;proporder:758-5989-8818-9253;pass:END;sub:END;*/

Shader "Shader Forge/NeonDissolve" {
    Properties {
        _Clouds1 ("Clouds1", 2D) = "white" {}
        _Color ("Color", Color) = (1,0,0.7655172,1)
        _Clouds2 ("Clouds2", 2D) = "white" {}
        _Speed ("Speed", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Clouds1; uniform float4 _Clouds1_ST;
            uniform float4 _Color;
            uniform sampler2D _Clouds2; uniform float4 _Clouds2_ST;
            uniform float _Speed;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float4 node_4763 = _Time + _TimeEditor;
                float node_1908 = (sin((node_4763.g*_Speed))*0.6+0.6);
                float4 node_7753 = _Time + _TimeEditor;
                float2 node_1614 = (i.uv0+node_7753.g*float2(0.05,0.05));
                float4 _Clouds1_var = tex2D(_Clouds1,TRANSFORM_TEX(node_1614, _Clouds1));
                float2 node_3896 = (i.uv0+node_7753.g*float2(-0.05,-0.05));
                float4 _Clouds2_var = tex2D(_Clouds2,TRANSFORM_TEX(node_3896, _Clouds2));
                clip((node_1908-(_Clouds1_var.r*_Clouds2_var.r)) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * _Color.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Clouds1; uniform float4 _Clouds1_ST;
            uniform float4 _Color;
            uniform sampler2D _Clouds2; uniform float4 _Clouds2_ST;
            uniform float _Speed;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float4 node_4763 = _Time + _TimeEditor;
                float node_1908 = (sin((node_4763.g*_Speed))*0.6+0.6);
                float4 node_8196 = _Time + _TimeEditor;
                float2 node_1614 = (i.uv0+node_8196.g*float2(0.05,0.05));
                float4 _Clouds1_var = tex2D(_Clouds1,TRANSFORM_TEX(node_1614, _Clouds1));
                float2 node_3896 = (i.uv0+node_8196.g*float2(-0.05,-0.05));
                float4 _Clouds2_var = tex2D(_Clouds2,TRANSFORM_TEX(node_3896, _Clouds2));
                clip((node_1908-(_Clouds1_var.r*_Clouds2_var.r)) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuse = directDiffuse * _Color.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Off
            
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
            uniform sampler2D _Clouds1; uniform float4 _Clouds1_ST;
            uniform sampler2D _Clouds2; uniform float4 _Clouds2_ST;
            uniform float _Speed;
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
                float4 node_4763 = _Time + _TimeEditor;
                float node_1908 = (sin((node_4763.g*_Speed))*0.6+0.6);
                float4 node_3362 = _Time + _TimeEditor;
                float2 node_1614 = (i.uv0+node_3362.g*float2(0.05,0.05));
                float4 _Clouds1_var = tex2D(_Clouds1,TRANSFORM_TEX(node_1614, _Clouds1));
                float2 node_3896 = (i.uv0+node_3362.g*float2(-0.05,-0.05));
                float4 _Clouds2_var = tex2D(_Clouds2,TRANSFORM_TEX(node_3896, _Clouds2));
                clip((node_1908-(_Clouds1_var.r*_Clouds2_var.r)) - 0.5);
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
            uniform sampler2D _Clouds1; uniform float4 _Clouds1_ST;
            uniform sampler2D _Clouds2; uniform float4 _Clouds2_ST;
            uniform float _Speed;
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
                float4 node_4763 = _Time + _TimeEditor;
                float node_1908 = (sin((node_4763.g*_Speed))*0.6+0.6);
                float4 node_5941 = _Time + _TimeEditor;
                float2 node_1614 = (i.uv0+node_5941.g*float2(0.05,0.05));
                float4 _Clouds1_var = tex2D(_Clouds1,TRANSFORM_TEX(node_1614, _Clouds1));
                float2 node_3896 = (i.uv0+node_5941.g*float2(-0.05,-0.05));
                float4 _Clouds2_var = tex2D(_Clouds2,TRANSFORM_TEX(node_3896, _Clouds2));
                clip((node_1908-(_Clouds1_var.r*_Clouds2_var.r)) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
