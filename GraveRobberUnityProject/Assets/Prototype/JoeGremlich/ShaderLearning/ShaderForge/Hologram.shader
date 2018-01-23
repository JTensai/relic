// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1784,x:33817,y:32764,varname:node_1784,prsc:2|emission-2421-OUT;n:type:ShaderForge.SFN_Color,id:3525,x:32588,y:32694,ptovrint:False,ptlb:BaseColor,ptin:_BaseColor,varname:node_3525,prsc:2,glob:False,c1:0,c2:0.7103448,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:5313,x:32588,y:32865,ptovrint:False,ptlb:Line1,ptin:_Line1,varname:node_5313,prsc:2,tex:401a5c4508287bf4ca31462b77bc61c9,ntxv:0,isnm:False|UVIN-4090-UVOUT;n:type:ShaderForge.SFN_Add,id:8522,x:33050,y:32819,varname:node_8522,prsc:2|A-3525-RGB,B-2275-OUT,C-2879-OUT;n:type:ShaderForge.SFN_Panner,id:4090,x:32418,y:32865,varname:node_4090,prsc:2,spu:0,spv:0.5;n:type:ShaderForge.SFN_Tex2d,id:4254,x:32588,y:33043,ptovrint:False,ptlb:Line2,ptin:_Line2,varname:node_4254,prsc:2,tex:401a5c4508287bf4ca31462b77bc61c9,ntxv:0,isnm:False|UVIN-8902-UVOUT;n:type:ShaderForge.SFN_Panner,id:8902,x:32418,y:33012,varname:node_8902,prsc:2,spu:0,spv:-0.8;n:type:ShaderForge.SFN_Fresnel,id:2430,x:32976,y:33204,varname:node_2430,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4350,x:33249,y:32870,varname:node_4350,prsc:2|A-8522-OUT,B-2430-OUT;n:type:ShaderForge.SFN_Multiply,id:2421,x:33460,y:32870,varname:node_2421,prsc:2|A-4350-OUT,B-8554-OUT,C-8351-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8554,x:33249,y:33026,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_8554,prsc:2,glob:False,v1:5;n:type:ShaderForge.SFN_TexCoord,id:7930,x:33050,y:32623,varname:node_7930,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:8351,x:33232,y:32623,varname:node_8351,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-7930-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2879,x:32924,y:33032,varname:node_2879,prsc:2|A-4254-RGB,B-6295-OUT;n:type:ShaderForge.SFN_Multiply,id:6295,x:32667,y:33288,varname:node_6295,prsc:2|A-3525-RGB,B-2926-OUT;n:type:ShaderForge.SFN_Vector1,id:2926,x:32443,y:33379,varname:node_2926,prsc:2,v1:1.9;n:type:ShaderForge.SFN_Multiply,id:2275,x:32842,y:32882,varname:node_2275,prsc:2|A-5313-RGB,B-6295-OUT;proporder:3525-5313-4254-8554;pass:END;sub:END;*/

Shader "Shader Forge/Hologram" {
    Properties {
        _BaseColor ("BaseColor", Color) = (0,0.7103448,1,1)
        _Line1 ("Line1", 2D) = "white" {}
        _Line2 ("Line2", 2D) = "white" {}
        _Intensity ("Intensity", Float ) = 5
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
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _BaseColor;
            uniform sampler2D _Line1; uniform float4 _Line1_ST;
            uniform sampler2D _Line2; uniform float4 _Line2_ST;
            uniform float _Intensity;
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_782 = _Time + _TimeEditor;
                float2 node_4090 = (i.uv0+node_782.g*float2(0,0.5));
                float4 _Line1_var = tex2D(_Line1,TRANSFORM_TEX(node_4090, _Line1));
                float3 node_6295 = (_BaseColor.rgb*1.9);
                float2 node_8902 = (i.uv0+node_782.g*float2(0,-0.8));
                float4 _Line2_var = tex2D(_Line2,TRANSFORM_TEX(node_8902, _Line2));
                float3 emissive = (((_BaseColor.rgb+(_Line1_var.rgb*node_6295)+(_Line2_var.rgb*node_6295))*(1.0-max(0,dot(normalDirection, viewDirection))))*_Intensity*i.uv0.g);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
