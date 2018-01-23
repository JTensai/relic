// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:2,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:8345,x:33399,y:32693,varname:node_8345,prsc:2|emission-1762-OUT,clip-4704-OUT;n:type:ShaderForge.SFN_Color,id:5018,x:32697,y:32724,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_5018,prsc:2,glob:False,c1:1,c2:0.5586207,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9433,x:32564,y:32931,ptovrint:False,ptlb:Haga,ptin:_Haga,varname:node_9433,prsc:2,tex:ef512abeec1a92f4ab5af1f84efede87,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1762,x:33003,y:32794,varname:node_1762,prsc:2|A-5018-RGB,B-853-OUT;n:type:ShaderForge.SFN_Add,id:3508,x:32857,y:33051,varname:node_3508,prsc:2|A-9433-RGB,B-1497-OUT;n:type:ShaderForge.SFN_Clamp01,id:853,x:32857,y:32864,varname:node_853,prsc:2|IN-3508-OUT;n:type:ShaderForge.SFN_Time,id:639,x:32456,y:33165,varname:node_639,prsc:2;n:type:ShaderForge.SFN_Sin,id:1497,x:32683,y:33167,varname:node_1497,prsc:2|IN-639-TTR;n:type:ShaderForge.SFN_ComponentMask,id:4704,x:33187,y:32924,varname:node_4704,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-1762-OUT;proporder:5018-9433;pass:END;sub:END;*/

Shader "Shader Forge/HagaSphere" {
    Properties {
        _MainColor ("MainColor", Color) = (1,0.5586207,0,1)
        _Haga ("Haga", 2D) = "white" {}
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _MainColor;
            uniform sampler2D _Haga; uniform float4 _Haga_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 _Haga_var = tex2D(_Haga,TRANSFORM_TEX(i.uv0, _Haga));
                float4 node_639 = _Time + _TimeEditor;
                float3 node_853 = saturate((_Haga_var.rgb+sin(node_639.a)));
                float3 node_1762 = (_MainColor.rgb*node_853);
                clip(node_1762.r - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = node_1762;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            uniform float4 _MainColor;
            uniform sampler2D _Haga; uniform float4 _Haga_ST;
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
                float4 _Haga_var = tex2D(_Haga,TRANSFORM_TEX(i.uv0, _Haga));
                float4 node_639 = _Time + _TimeEditor;
                float3 node_853 = saturate((_Haga_var.rgb+sin(node_639.a)));
                float3 node_1762 = (_MainColor.rgb*node_853);
                clip(node_1762.r - 0.5);
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
            uniform float4 _MainColor;
            uniform sampler2D _Haga; uniform float4 _Haga_ST;
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
                float4 _Haga_var = tex2D(_Haga,TRANSFORM_TEX(i.uv0, _Haga));
                float4 node_639 = _Time + _TimeEditor;
                float3 node_853 = saturate((_Haga_var.rgb+sin(node_639.a)));
                float3 node_1762 = (_MainColor.rgb*node_853);
                clip(node_1762.r - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
