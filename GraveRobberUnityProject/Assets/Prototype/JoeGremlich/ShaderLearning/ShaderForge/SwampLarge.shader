// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:False,igpj:True,qofs:549,qpre:2,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:900,x:33951,y:33156,varname:node_900,prsc:2|diff-3673-OUT,emission-2345-OUT,alpha-7283-OUT;n:type:ShaderForge.SFN_Tex2d,id:9393,x:32514,y:32789,ptovrint:False,ptlb:Highlight1,ptin:_Highlight1,varname:node_9393,prsc:2,tex:e65442ae71e67e04399cedb35a3cfa1e,ntxv:0,isnm:False|UVIN-1031-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3490,x:32514,y:32971,ptovrint:False,ptlb:Highlight2,ptin:_Highlight2,varname:node_3490,prsc:2,tex:abdac6d030ac002469f6905fb7287ce6,ntxv:0,isnm:False|UVIN-6939-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2598,x:32742,y:32895,varname:node_2598,prsc:2|A-9393-RGB,B-3490-RGB;n:type:ShaderForge.SFN_Panner,id:1031,x:32354,y:32813,varname:node_1031,prsc:2,spu:-0.0003125,spv:-0.000625;n:type:ShaderForge.SFN_Panner,id:6939,x:32354,y:32953,varname:node_6939,prsc:2,spu:0.003125,spv:0.000625;n:type:ShaderForge.SFN_Tex2d,id:4204,x:32790,y:33286,ptovrint:False,ptlb:Swamp1,ptin:_Swamp1,varname:node_4204,prsc:2,tex:d0a517bdda13a874c94c8c3e71f1c6fe,ntxv:0,isnm:False|UVIN-815-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8362,x:32790,y:33470,ptovrint:False,ptlb:Swamp2,ptin:_Swamp2,varname:node_8362,prsc:2,tex:56ed36f1673ce9b4d9128d97bc6eb8d3,ntxv:0,isnm:False|UVIN-1683-UVOUT;n:type:ShaderForge.SFN_Panner,id:815,x:32478,y:33271,varname:node_815,prsc:2,spu:0.0015625,spv:0.0015625;n:type:ShaderForge.SFN_Panner,id:1683,x:32478,y:33452,varname:node_1683,prsc:2,spu:-0.015625,spv:-0.0015625;n:type:ShaderForge.SFN_Multiply,id:3673,x:33048,y:33372,varname:node_3673,prsc:2|A-4204-RGB,B-8362-RGB;n:type:ShaderForge.SFN_Multiply,id:9997,x:32916,y:32895,varname:node_9997,prsc:2|A-2598-OUT,B-1770-OUT;n:type:ShaderForge.SFN_Multiply,id:4366,x:33234,y:33396,varname:node_4366,prsc:2|A-3673-OUT,B-4299-OUT;n:type:ShaderForge.SFN_Add,id:9286,x:33421,y:33353,varname:node_9286,prsc:2|A-3230-OUT,B-4366-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4299,x:33048,y:33517,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_4299,prsc:2,glob:False,v1:0.6;n:type:ShaderForge.SFN_DepthBlend,id:2343,x:32880,y:33730,varname:node_2343,prsc:2|DIST-2372-OUT;n:type:ShaderForge.SFN_Time,id:7577,x:32168,y:33720,varname:node_7577,prsc:2;n:type:ShaderForge.SFN_Sin,id:2258,x:32548,y:33730,varname:node_2258,prsc:2|IN-9301-OUT;n:type:ShaderForge.SFN_RemapRange,id:2372,x:32722,y:33730,varname:node_2372,prsc:2,frmn:-1,frmx:1,tomn:0.2,tomx:0.5|IN-2258-OUT;n:type:ShaderForge.SFN_Add,id:2345,x:33661,y:33434,varname:node_2345,prsc:2|A-9286-OUT,B-7303-OUT;n:type:ShaderForge.SFN_OneMinus,id:3955,x:33032,y:33730,varname:node_3955,prsc:2|IN-2343-OUT;n:type:ShaderForge.SFN_Multiply,id:7303,x:33201,y:33730,varname:node_7303,prsc:2|A-3955-OUT,B-3640-RGB;n:type:ShaderForge.SFN_Color,id:3640,x:33032,y:33891,ptovrint:False,ptlb:WaveColor,ptin:_WaveColor,varname:node_3640,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Slider,id:7283,x:33570,y:33695,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_7283,prsc:2,min:0,cur:0.8,max:1;n:type:ShaderForge.SFN_Multiply,id:3230,x:33156,y:33065,varname:node_3230,prsc:2|A-9997-OUT,B-3164-RGB;n:type:ShaderForge.SFN_Color,id:3164,x:32897,y:33106,ptovrint:False,ptlb:Highlight Color,ptin:_HighlightColor,varname:node_3164,prsc:2,glob:False,c1:0.4068966,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:1770,x:32726,y:33051,ptovrint:False,ptlb:Highlight Strength,ptin:_HighlightStrength,varname:node_1770,prsc:2,glob:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:9301,x:32383,y:33805,varname:node_9301,prsc:2|A-7577-T,B-8387-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8387,x:32168,y:33880,ptovrint:False,ptlb:Wave Speed,ptin:_WaveSpeed,varname:node_8387,prsc:2,glob:False,v1:1;proporder:9393-3490-4204-8362-4299-3640-7283-3164-1770-8387;pass:END;sub:END;*/

Shader "Shader Forge/SwampLarge" {
    Properties {
        _Highlight1 ("Highlight1", 2D) = "white" {}
        _Highlight2 ("Highlight2", 2D) = "white" {}
        _Swamp1 ("Swamp1", 2D) = "white" {}
        _Swamp2 ("Swamp2", 2D) = "white" {}
        _Intensity ("Intensity", Float ) = 0.6
        _WaveColor ("WaveColor", Color) = (0.5,0.5,0.5,1)
        _Opacity ("Opacity", Range(0, 1)) = 0.8
        _HighlightColor ("Highlight Color", Color) = (0.4068966,1,0,1)
        _HighlightStrength ("Highlight Strength", Float ) = 0.5
        _WaveSpeed ("Wave Speed", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="AlphaTest+549"
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
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _Highlight1; uniform float4 _Highlight1_ST;
            uniform sampler2D _Highlight2; uniform float4 _Highlight2_ST;
            uniform sampler2D _Swamp1; uniform float4 _Swamp1_ST;
            uniform sampler2D _Swamp2; uniform float4 _Swamp2_ST;
            uniform float _Intensity;
            uniform float4 _WaveColor;
            uniform float _Opacity;
            uniform float4 _HighlightColor;
            uniform float _HighlightStrength;
            uniform float _WaveSpeed;
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
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
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
                float4 node_6420 = _Time + _TimeEditor;
                float2 node_815 = (i.uv0+node_6420.g*float2(0.0015625,0.0015625));
                float4 _Swamp1_var = tex2D(_Swamp1,TRANSFORM_TEX(node_815, _Swamp1));
                float2 node_1683 = (i.uv0+node_6420.g*float2(-0.015625,-0.0015625));
                float4 _Swamp2_var = tex2D(_Swamp2,TRANSFORM_TEX(node_1683, _Swamp2));
                float3 node_3673 = (_Swamp1_var.rgb*_Swamp2_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * node_3673;
////// Emissive:
                float2 node_1031 = (i.uv0+node_6420.g*float2(-0.0003125,-0.000625));
                float4 _Highlight1_var = tex2D(_Highlight1,TRANSFORM_TEX(node_1031, _Highlight1));
                float2 node_6939 = (i.uv0+node_6420.g*float2(0.003125,0.000625));
                float4 _Highlight2_var = tex2D(_Highlight2,TRANSFORM_TEX(node_6939, _Highlight2));
                float4 node_7577 = _Time + _TimeEditor;
                float3 emissive = (((((_Highlight1_var.rgb*_Highlight2_var.rgb)*_HighlightStrength)*_HighlightColor.rgb)+(node_3673*_Intensity))+((1.0 - saturate((sceneZ-partZ)/(sin((node_7577.g*_WaveSpeed))*0.15+0.35)))*_WaveColor.rgb));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(finalColor,_Opacity);
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
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _Highlight1; uniform float4 _Highlight1_ST;
            uniform sampler2D _Highlight2; uniform float4 _Highlight2_ST;
            uniform sampler2D _Swamp1; uniform float4 _Swamp1_ST;
            uniform sampler2D _Swamp2; uniform float4 _Swamp2_ST;
            uniform float _Intensity;
            uniform float4 _WaveColor;
            uniform float _Opacity;
            uniform float4 _HighlightColor;
            uniform float _HighlightStrength;
            uniform float _WaveSpeed;
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
                float4 projPos : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
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
                float4 node_7981 = _Time + _TimeEditor;
                float2 node_815 = (i.uv0+node_7981.g*float2(0.0015625,0.0015625));
                float4 _Swamp1_var = tex2D(_Swamp1,TRANSFORM_TEX(node_815, _Swamp1));
                float2 node_1683 = (i.uv0+node_7981.g*float2(-0.015625,-0.0015625));
                float4 _Swamp2_var = tex2D(_Swamp2,TRANSFORM_TEX(node_1683, _Swamp2));
                float3 node_3673 = (_Swamp1_var.rgb*_Swamp2_var.rgb);
                float3 diffuse = directDiffuse * node_3673;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * _Opacity,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
