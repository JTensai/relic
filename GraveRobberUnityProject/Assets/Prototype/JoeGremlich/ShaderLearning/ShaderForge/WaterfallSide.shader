// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3014,x:33464,y:32702,varname:node_3014,prsc:2|diff-5628-RGB,alpha-4738-OUT;n:type:ShaderForge.SFN_Tex2d,id:5628,x:32789,y:32612,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_5628,prsc:2,tex:bac43fee42c41054b971cca701329ed0,ntxv:0,isnm:False|UVIN-1609-UVOUT;n:type:ShaderForge.SFN_Panner,id:1609,x:32469,y:32617,varname:node_1609,prsc:2,spu:0,spv:2;n:type:ShaderForge.SFN_Multiply,id:2487,x:33039,y:32794,varname:node_2487,prsc:2|A-5628-A,B-9463-OUT;n:type:ShaderForge.SFN_Slider,id:9463,x:32725,y:32949,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_9463,prsc:2,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_TexCoord,id:8898,x:32725,y:33078,varname:node_8898,prsc:2,uv:0;n:type:ShaderForge.SFN_ComponentMask,id:3612,x:32908,y:33078,varname:node_3612,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-8898-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4738,x:33199,y:32995,varname:node_4738,prsc:2|A-2487-OUT,B-1053-OUT;n:type:ShaderForge.SFN_Power,id:1053,x:33122,y:33167,varname:node_1053,prsc:2|VAL-3612-OUT,EXP-7983-OUT;n:type:ShaderForge.SFN_Vector1,id:7983,x:32908,y:33275,varname:node_7983,prsc:2,v1:1.5;proporder:5628-9463;pass:END;sub:END;*/

Shader "Shader Forge/WaterfallSide" {
    Properties {
        _Tex ("Tex", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0.2
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
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform float _Opacity;
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
                float4 node_5743 = _Time + _TimeEditor;
                float2 node_1609 = (i.uv0+node_5743.g*float2(0,2));
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_1609, _Tex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * _Tex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor,((_Tex_var.a*_Opacity)*pow(i.uv0.g,1.5)));
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
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            uniform float _Opacity;
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
                float4 node_295 = _Time + _TimeEditor;
                float2 node_1609 = (i.uv0+node_295.g*float2(0,2));
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_1609, _Tex));
                float3 diffuse = directDiffuse * _Tex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * ((_Tex_var.a*_Opacity)*pow(i.uv0.g,1.5)),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
