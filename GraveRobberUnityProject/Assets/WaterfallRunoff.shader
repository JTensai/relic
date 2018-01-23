// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:8231,x:33612,y:32701,varname:node_8231,prsc:2|emission-6279-OUT,alpha-6100-OUT;n:type:ShaderForge.SFN_Tex2d,id:3096,x:32837,y:32708,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_3096,prsc:2,tex:5f2f611c6a8d05140aae7d77e7de897a,ntxv:0,isnm:False|UVIN-7470-UVOUT;n:type:ShaderForge.SFN_Panner,id:7470,x:32605,y:32728,varname:node_7470,prsc:2,spu:-0.1,spv:0.75;n:type:ShaderForge.SFN_TexCoord,id:808,x:32529,y:32974,varname:node_808,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:8238,x:32692,y:32974,varname:node_8238,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-808-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2755,x:32778,y:32500,ptovrint:False,ptlb:BackTex,ptin:_BackTex,varname:node_2755,prsc:2,tex:76d5e4891499d4b4da7af02b7ae4fee1,ntxv:0,isnm:False|UVIN-3580-UVOUT;n:type:ShaderForge.SFN_Blend,id:6279,x:33062,y:32622,varname:node_6279,prsc:2,blmd:10,clmp:True|SRC-3096-RGB,DST-2755-RGB;n:type:ShaderForge.SFN_Panner,id:3580,x:32565,y:32523,varname:node_3580,prsc:2,spu:0,spv:0.5;n:type:ShaderForge.SFN_Power,id:6100,x:32987,y:33008,varname:node_6100,prsc:2|VAL-8238-OUT,EXP-1192-OUT;n:type:ShaderForge.SFN_Vector1,id:1192,x:32731,y:33154,varname:node_1192,prsc:2,v1:0.5;proporder:3096-2755;pass:END;sub:END;*/

Shader "Shader Forge/WaterfallRunoff" {
    Properties {
        _Tex ("Tex", 2D) = "white" {}
        _BackTex ("BackTex", 2D) = "white" {}
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
            uniform float4 _TimeEditor;
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform sampler2D _BackTex; uniform float4 _BackTex_ST;
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
////// Lighting:
////// Emissive:
                float4 node_2751 = _Time + _TimeEditor;
                float2 node_7470 = (i.uv0+node_2751.g*float2(-0.1,0.75));
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_7470, _Tex));
                float2 node_3580 = (i.uv0+node_2751.g*float2(0,0.5));
                float4 _BackTex_var = tex2D(_BackTex,TRANSFORM_TEX(node_3580, _BackTex));
                float3 emissive = saturate(( _BackTex_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_BackTex_var.rgb-0.5))*(1.0-_Tex_var.rgb)) : (2.0*_BackTex_var.rgb*_Tex_var.rgb) ));
                float3 finalColor = emissive;
                return fixed4(finalColor,pow(i.uv0.g,0.5));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
