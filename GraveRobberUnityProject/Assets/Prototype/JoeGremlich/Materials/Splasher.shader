// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:5171,x:33509,y:32714,varname:node_5171,prsc:2|diff-6577-RGB;n:type:ShaderForge.SFN_Tex2d,id:6577,x:32827,y:32591,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6577,prsc:2,tex:c2c4a30251642304da04e04aeaa20d7c,ntxv:0,isnm:False|UVIN-3505-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:732,x:32075,y:32581,varname:node_732,prsc:2,uv:0;n:type:ShaderForge.SFN_Power,id:8206,x:32468,y:32663,varname:node_8206,prsc:2|VAL-732-UVOUT,EXP-2586-OUT;n:type:ShaderForge.SFN_Vector1,id:7725,x:31861,y:33019,varname:node_7725,prsc:2,v1:1;n:type:ShaderForge.SFN_Time,id:8767,x:31889,y:32876,varname:node_8767,prsc:2;n:type:ShaderForge.SFN_Frac,id:2736,x:32229,y:33060,varname:node_2736,prsc:2|IN-8607-OUT;n:type:ShaderForge.SFN_Divide,id:8607,x:32070,y:32933,varname:node_8607,prsc:2|A-8767-T,B-7725-OUT;n:type:ShaderForge.SFN_RemapRange,id:2586,x:32472,y:32897,varname:node_2586,prsc:2,frmn:0,frmx:1,tomn:0,tomx:8|IN-2736-OUT;n:type:ShaderForge.SFN_Tex2d,id:5669,x:32731,y:32895,ptovrint:False,ptlb:clouds,ptin:_clouds,varname:node_5669,prsc:2,tex:5c1bdcc4915f1014f9ea9fd5e76be435,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3080,x:33063,y:32929,varname:node_3080,prsc:2|A-6577-A,B-2307-OUT;n:type:ShaderForge.SFN_Power,id:2307,x:32942,y:33084,varname:node_2307,prsc:2|VAL-5669-A,EXP-2423-OUT;n:type:ShaderForge.SFN_RemapRange,id:2423,x:32526,y:33073,varname:node_2423,prsc:2,frmn:0,frmx:1,tomn:0,tomx:2|IN-2736-OUT;n:type:ShaderForge.SFN_Vector1,id:4,x:32199,y:32767,varname:node_4,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Panner,id:3505,x:32447,y:32394,varname:node_3505,prsc:2,spu:0,spv:-1;proporder:6577-5669;pass:END;sub:END;*/

Shader "Shader Forge/Splasher" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _clouds ("clouds", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
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
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_3491 = _Time + _TimeEditor;
                float2 node_3505 = (i.uv0+node_3491.g*float2(0,-1));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_3505, _Texture));
                float3 diffuse = (directDiffuse + indirectDiffuse) * _Texture_var.rgb;
/// Final Color:
                float3 finalColor = diffuse;
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
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
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
                float4 node_173 = _Time + _TimeEditor;
                float2 node_3505 = (i.uv0+node_173.g*float2(0,-1));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_3505, _Texture));
                float3 diffuse = directDiffuse * _Texture_var.rgb;
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
