// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1750,x:33966,y:32658,varname:node_1750,prsc:2|diff-2651-OUT,emission-2651-OUT;n:type:ShaderForge.SFN_Tex2d,id:8781,x:33259,y:32624,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_8781,prsc:2,tex:3682c865336f38740b94c27a8d8c8c1e,ntxv:0,isnm:False|UVIN-9776-OUT;n:type:ShaderForge.SFN_TexCoord,id:4973,x:32567,y:32614,varname:node_4973,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:2145,x:32854,y:32686,varname:node_2145,prsc:2|A-4973-UVOUT,B-538-OUT;n:type:ShaderForge.SFN_Add,id:5653,x:32854,y:32527,varname:node_5653,prsc:2|A-2060-OUT,B-4973-UVOUT;n:type:ShaderForge.SFN_Add,id:9776,x:33057,y:32624,varname:node_9776,prsc:2|A-5653-OUT,B-2145-OUT;n:type:ShaderForge.SFN_Negate,id:538,x:32567,y:32755,varname:node_538,prsc:2|IN-3674-OUT;n:type:ShaderForge.SFN_Divide,id:2060,x:32567,y:32468,varname:node_2060,prsc:2|A-3674-OUT,B-7980-OUT;n:type:ShaderForge.SFN_Vector1,id:7980,x:32361,y:32468,varname:node_7980,prsc:2,v1:2;n:type:ShaderForge.SFN_VertexColor,id:212,x:33259,y:32778,varname:node_212,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2651,x:33446,y:32747,varname:node_2651,prsc:2|A-8781-RGB,B-212-RGB,C-212-A;n:type:ShaderForge.SFN_Tex2d,id:3265,x:33441,y:32918,ptovrint:False,ptlb:Highlights,ptin:_Highlights,varname:node_3265,prsc:2,tex:e65442ae71e67e04399cedb35a3cfa1e,ntxv:0,isnm:False|UVIN-741-UVOUT;n:type:ShaderForge.SFN_Multiply,id:548,x:33658,y:32828,varname:node_548,prsc:2|A-2651-OUT,B-3265-RGB;n:type:ShaderForge.SFN_Panner,id:741,x:33245,y:32954,varname:node_741,prsc:2,spu:0.05,spv:0.05;n:type:ShaderForge.SFN_Slider,id:5989,x:31918,y:32641,ptovrint:False,ptlb:Amount,ptin:_Amount,varname:node_5989,prsc:2,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:3674,x:32283,y:32635,varname:node_3674,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1|IN-5989-OUT;proporder:8781-3265-5989;pass:END;sub:END;*/

Shader "Shader Forge/Ripple" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Highlights ("Highlights", 2D) = "white" {}
        _Amount ("Amount", Range(0, 1)) = 1
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
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Amount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float node_3674 = (_Amount*1.0+0.0);
                float2 node_9776 = (((node_3674/2.0)+i.uv0)+(i.uv0*(-1*node_3674)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_9776, _MainTex));
                float3 node_2651 = (_MainTex_var.rgb*i.vertexColor.rgb*i.vertexColor.a);
                float3 diffuse = (directDiffuse + indirectDiffuse) * node_2651;
////// Emissive:
                float3 emissive = node_2651;
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Amount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float node_3674 = (_Amount*1.0+0.0);
                float2 node_9776 = (((node_3674/2.0)+i.uv0)+(i.uv0*(-1*node_3674)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_9776, _MainTex));
                float3 node_2651 = (_MainTex_var.rgb*i.vertexColor.rgb*i.vertexColor.a);
                float3 diffuse = directDiffuse * node_2651;
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
