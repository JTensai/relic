// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:False,aust:False,igpj:True,qofs:5,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4136,x:33696,y:32711,varname:node_4136,prsc:2|emission-313-OUT,alpha-4166-A;n:type:ShaderForge.SFN_Tex2d,id:3499,x:32859,y:32677,ptovrint:False,ptlb:Neon,ptin:_Neon,varname:node_3499,prsc:2,tex:416d92ae5ebe52549a87fe1ea5a69fcc,ntxv:0,isnm:False|UVIN-9526-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6838,x:32452,y:33038,varname:node_6838,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:4133,x:33110,y:32965,varname:node_4133,prsc:2|A-3499-RGB,B-4511-OUT,C-1229-OUT;n:type:ShaderForge.SFN_OneMinus,id:8863,x:32692,y:32937,varname:node_8863,prsc:2|IN-6838-V;n:type:ShaderForge.SFN_Power,id:4511,x:32886,y:32937,varname:node_4511,prsc:2|VAL-8863-OUT,EXP-6053-OUT;n:type:ShaderForge.SFN_Vector1,id:6053,x:32692,y:33070,varname:node_6053,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:939,x:33264,y:32845,varname:node_939,prsc:2|A-4261-OUT,B-4133-OUT;n:type:ShaderForge.SFN_Vector1,id:4261,x:33062,y:32794,varname:node_4261,prsc:2,v1:2;n:type:ShaderForge.SFN_Panner,id:9526,x:32620,y:32645,varname:node_9526,prsc:2,spu:0,spv:1.5;n:type:ShaderForge.SFN_Power,id:1229,x:32886,y:33133,varname:node_1229,prsc:2|VAL-6838-V,EXP-6053-OUT;n:type:ShaderForge.SFN_VertexColor,id:4166,x:33264,y:32720,varname:node_4166,prsc:2;n:type:ShaderForge.SFN_Multiply,id:313,x:33466,y:32777,varname:node_313,prsc:2|A-4166-RGB,B-939-OUT;proporder:3499;pass:END;sub:END;*/

Shader "Shader Forge/NeonRibbon" {
    Properties {
        _Neon ("Neon", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+5"
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
            uniform float4 _TimeEditor;
            uniform sampler2D _Neon; uniform float4 _Neon_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_6944 = _Time + _TimeEditor;
                float2 node_9526 = (i.uv0+node_6944.g*float2(0,1.5));
                float4 _Neon_var = tex2D(_Neon,TRANSFORM_TEX(node_9526, _Neon));
                float node_6053 = 0.5;
                float3 emissive = (i.vertexColor.rgb*(2.0*(_Neon_var.rgb*pow((1.0 - i.uv0.g),node_6053)*pow(i.uv0.g,node_6053))));
                float3 finalColor = emissive;
                return fixed4(finalColor,i.vertexColor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
