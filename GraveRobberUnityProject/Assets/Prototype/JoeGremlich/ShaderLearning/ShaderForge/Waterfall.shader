// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4731,x:33437,y:32726,varname:node_4731,prsc:2|diff-8922-RGB,emission-5007-OUT,clip-5053-OUT;n:type:ShaderForge.SFN_Tex2d,id:8922,x:32728,y:32616,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_8922,prsc:2,tex:76d5e4891499d4b4da7af02b7ae4fee1,ntxv:0,isnm:False|UVIN-4328-UVOUT;n:type:ShaderForge.SFN_Panner,id:4328,x:32486,y:32623,varname:node_4328,prsc:2,spu:0,spv:0.2;n:type:ShaderForge.SFN_Tex2d,id:7321,x:32701,y:32829,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_7321,prsc:2,tex:fa8d84b69a56bc04796da292ce6698ae,ntxv:0,isnm:False|UVIN-2229-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3664,x:32517,y:33077,varname:node_3664,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:5053,x:32982,y:32996,varname:node_5053,prsc:2|A-7321-R,B-6336-OUT;n:type:ShaderForge.SFN_Power,id:6336,x:32760,y:33077,varname:node_6336,prsc:2|VAL-3664-V,EXP-7798-OUT;n:type:ShaderForge.SFN_Panner,id:2229,x:32507,y:32829,varname:node_2229,prsc:2,spu:-0.1,spv:0.7;n:type:ShaderForge.SFN_Multiply,id:7958,x:33065,y:32779,varname:node_7958,prsc:2|A-8922-RGB,B-8695-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8695,x:32893,y:32884,ptovrint:False,ptlb:EmissionIntensity,ptin:_EmissionIntensity,varname:node_8695,prsc:2,glob:False,v1:0.7;n:type:ShaderForge.SFN_Time,id:8525,x:31884,y:33160,varname:node_8525,prsc:2;n:type:ShaderForge.SFN_RemapRange,id:7798,x:32607,y:33222,varname:node_7798,prsc:2,frmn:-1,frmx:1,tomn:1,tomx:1.2|IN-5803-OUT;n:type:ShaderForge.SFN_Multiply,id:3426,x:32066,y:33220,varname:node_3426,prsc:2|A-8525-T,B-6255-OUT;n:type:ShaderForge.SFN_Vector1,id:6255,x:31884,y:33293,varname:node_6255,prsc:2,v1:1;n:type:ShaderForge.SFN_Sin,id:5803,x:32440,y:33222,varname:node_5803,prsc:2|IN-3426-OUT;n:type:ShaderForge.SFN_Tex2d,id:5676,x:32805,y:32434,ptovrint:False,ptlb:Folds,ptin:_Folds,varname:node_5676,prsc:2,tex:9d51ae1f67675554a8a04f64a125800e,ntxv:0,isnm:False|UVIN-6415-UVOUT;n:type:ShaderForge.SFN_Panner,id:6415,x:32601,y:32434,varname:node_6415,prsc:2,spu:0,spv:0.15;n:type:ShaderForge.SFN_Blend,id:5007,x:33124,y:32518,varname:node_5007,prsc:2,blmd:10,clmp:True|SRC-5676-RGB,DST-7958-OUT;proporder:8922-7321-8695-5676;pass:END;sub:END;*/

Shader "Shader Forge/Waterfall" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _EmissionIntensity ("EmissionIntensity", Float ) = 0.7
        _Folds ("Folds", 2D) = "white" {}
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
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float _EmissionIntensity;
            uniform sampler2D _Folds; uniform float4 _Folds_ST;
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
                float4 node_3318 = _Time + _TimeEditor;
                float2 node_2229 = (i.uv0+node_3318.g*float2(-0.1,0.7));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_2229, _Mask));
                float4 node_8525 = _Time + _TimeEditor;
                clip((_Mask_var.r+pow(i.uv0.g,(sin((node_8525.g*1.0))*0.1+1.1))) - 0.5);
////// Lighting:
////// Emissive:
                float2 node_6415 = (i.uv0+node_3318.g*float2(0,0.15));
                float4 _Folds_var = tex2D(_Folds,TRANSFORM_TEX(node_6415, _Folds));
                float2 node_4328 = (i.uv0+node_3318.g*float2(0,0.2));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_4328, _MainTex));
                float3 emissive = saturate(( (_MainTex_var.rgb*_EmissionIntensity) > 0.5 ? (1.0-(1.0-2.0*((_MainTex_var.rgb*_EmissionIntensity)-0.5))*(1.0-_Folds_var.rgb)) : (2.0*(_MainTex_var.rgb*_EmissionIntensity)*_Folds_var.rgb) ));
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
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
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
                float4 node_3358 = _Time + _TimeEditor;
                float2 node_2229 = (i.uv0+node_3358.g*float2(-0.1,0.7));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_2229, _Mask));
                float4 node_8525 = _Time + _TimeEditor;
                clip((_Mask_var.r+pow(i.uv0.g,(sin((node_8525.g*1.0))*0.1+1.1))) - 0.5);
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
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
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
                float4 node_5291 = _Time + _TimeEditor;
                float2 node_2229 = (i.uv0+node_5291.g*float2(-0.1,0.7));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_2229, _Mask));
                float4 node_8525 = _Time + _TimeEditor;
                clip((_Mask_var.r+pow(i.uv0.g,(sin((node_8525.g*1.0))*0.1+1.1))) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
