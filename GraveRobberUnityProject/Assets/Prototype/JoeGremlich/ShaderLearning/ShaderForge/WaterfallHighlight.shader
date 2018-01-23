// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1121,x:33051,y:32682,varname:node_1121,prsc:2|diff-1396-OUT,emission-1396-OUT;n:type:ShaderForge.SFN_Tex2d,id:9967,x:32464,y:32618,ptovrint:False,ptlb:node_9967,ptin:_node_9967,varname:node_9967,prsc:2,tex:d441e4af35ea5624082c61643eb1bf05,ntxv:0,isnm:False|UVIN-9368-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5464,x:32329,y:32887,ptovrint:False,ptlb:node_5464,ptin:_node_5464,varname:node_5464,prsc:2,tex:e3db92b5c21cb89448c34c60817ce83a,ntxv:0,isnm:False|UVIN-528-UVOUT;n:type:ShaderForge.SFN_Panner,id:528,x:32052,y:32880,varname:node_528,prsc:2,spu:0.3,spv:0;n:type:ShaderForge.SFN_Multiply,id:3124,x:32614,y:32814,varname:node_3124,prsc:2|A-9967-RGB,B-5464-RGB;n:type:ShaderForge.SFN_Panner,id:9368,x:32197,y:32605,varname:node_9368,prsc:2,spu:-0.1,spv:0;n:type:ShaderForge.SFN_Tex2d,id:9059,x:32459,y:33089,ptovrint:False,ptlb:Falloff,ptin:_Falloff,varname:node_9059,prsc:2,tex:ebef8a10a53920c4c9a8a6ee85923489,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1396,x:32827,y:32849,varname:node_1396,prsc:2|A-3124-OUT,B-9059-RGB;proporder:9967-5464-9059;pass:END;sub:END;*/

Shader "Shader Forge/WaterfallHighlight" {
    Properties {
        _node_9967 ("node_9967", 2D) = "white" {}
        _node_5464 ("node_5464", 2D) = "white" {}
        _Falloff ("Falloff", 2D) = "white" {}
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
            Blend One One
            ZWrite Off
            
            Fog {Mode Off}
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
            uniform sampler2D _node_9967; uniform float4 _node_9967_ST;
            uniform sampler2D _node_5464; uniform float4 _node_5464_ST;
            uniform sampler2D _Falloff; uniform float4 _Falloff_ST;
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
                float4 node_8012 = _Time + _TimeEditor;
                float2 node_9368 = (i.uv0+node_8012.g*float2(-0.1,0));
                float4 _node_9967_var = tex2D(_node_9967,TRANSFORM_TEX(node_9368, _node_9967));
                float2 node_528 = (i.uv0+node_8012.g*float2(0.3,0));
                float4 _node_5464_var = tex2D(_node_5464,TRANSFORM_TEX(node_528, _node_5464));
                float4 _Falloff_var = tex2D(_Falloff,TRANSFORM_TEX(i.uv0, _Falloff));
                float3 node_1396 = ((_node_9967_var.rgb*_node_5464_var.rgb)*_Falloff_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * node_1396;
////// Emissive:
                float3 emissive = node_1396;
/// Final Color:
                float3 finalColor = diffuse + emissive;
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
            uniform sampler2D _node_9967; uniform float4 _node_9967_ST;
            uniform sampler2D _node_5464; uniform float4 _node_5464_ST;
            uniform sampler2D _Falloff; uniform float4 _Falloff_ST;
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
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_4158 = _Time + _TimeEditor;
                float2 node_9368 = (i.uv0+node_4158.g*float2(-0.1,0));
                float4 _node_9967_var = tex2D(_node_9967,TRANSFORM_TEX(node_9368, _node_9967));
                float2 node_528 = (i.uv0+node_4158.g*float2(0.3,0));
                float4 _node_5464_var = tex2D(_node_5464,TRANSFORM_TEX(node_528, _node_5464));
                float4 _Falloff_var = tex2D(_Falloff,TRANSFORM_TEX(i.uv0, _Falloff));
                float3 node_1396 = ((_node_9967_var.rgb*_node_5464_var.rgb)*_Falloff_var.rgb);
                float3 diffuse = directDiffuse * node_1396;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
