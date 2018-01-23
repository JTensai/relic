// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:True,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:3,bsrc:0,bdst:6,culm:0,dpts:2,wrdp:False,dith:0,ufog:False,aust:False,igpj:True,qofs:902,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:953,x:33133,y:32685,varname:node_953,prsc:2|emission-3427-OUT,alpha-6807-OUT;n:type:ShaderForge.SFN_VertexColor,id:5915,x:32574,y:32877,varname:node_5915,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:6956,x:32602,y:32658,ptovrint:False,ptlb:node_6956,ptin:_node_6956,varname:node_6956,prsc:2,tex:567da5b814d0235449e6c5432f169209,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3427,x:32840,y:32887,varname:node_3427,prsc:2|A-6956-RGB,B-5915-RGB,C-5915-A;n:type:ShaderForge.SFN_Vector1,id:6807,x:32868,y:33077,varname:node_6807,prsc:2,v1:1;proporder:6956;pass:END;sub:END;*/

Shader "Shader Forge/Ripples" {
    Properties {
        _node_6956 ("node_6956", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+902"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcColor
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
            uniform sampler2D _node_6956; uniform float4 _node_6956_ST;
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
                float4 _node_6956_var = tex2D(_node_6956,TRANSFORM_TEX(i.uv0, _node_6956));
                float3 emissive = (_node_6956_var.rgb*i.vertexColor.rgb*i.vertexColor.a);
                float3 finalColor = emissive;
                return fixed4(finalColor,1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
