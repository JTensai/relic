// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1226211,fgcg:0.3970588,fgcb:0.3177967,fgca:1,fgde:0.025,fgrn:10,fgrf:30,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9561,x:33625,y:32657,varname:node_9561,prsc:2|diff-4787-RGB,emission-5494-OUT;n:type:ShaderForge.SFN_Tex2d,id:6500,x:32804,y:32604,ptovrint:False,ptlb:Lines,ptin:_Lines,varname:node_6500,prsc:2,tex:e11d02f3e3b6ef34db79abf49e9a87ed,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9929,x:33042,y:32768,varname:node_9929,prsc:2|A-6500-RGB,B-8643-OUT;n:type:ShaderForge.SFN_Time,id:1435,x:31875,y:33032,varname:node_1435,prsc:2;n:type:ShaderForge.SFN_Sin,id:2305,x:32282,y:33086,varname:node_2305,prsc:2|IN-6851-OUT;n:type:ShaderForge.SFN_Lerp,id:1821,x:32717,y:33028,varname:node_1821,prsc:2|A-1760-OUT,B-3334-OUT,T-7577-OUT;n:type:ShaderForge.SFN_Lerp,id:8643,x:33009,y:33321,varname:node_8643,prsc:2|A-1821-OUT,B-8741-OUT,T-6934-OUT;n:type:ShaderForge.SFN_Vector3,id:1760,x:32496,y:32907,varname:node_1760,prsc:2,v1:1,v2:0,v3:0;n:type:ShaderForge.SFN_Vector3,id:3334,x:32496,y:32989,varname:node_3334,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_Vector3,id:8741,x:32717,y:33176,varname:node_8741,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Cos,id:905,x:32282,y:33237,varname:node_905,prsc:2|IN-6851-OUT;n:type:ShaderForge.SFN_RemapRange,id:7577,x:32496,y:33086,varname:node_7577,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-2305-OUT;n:type:ShaderForge.SFN_RemapRange,id:6934,x:32496,y:33261,varname:node_6934,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-905-OUT;n:type:ShaderForge.SFN_Multiply,id:6851,x:32084,y:33143,varname:node_6851,prsc:2|A-1435-T,B-7688-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7688,x:31839,y:33243,ptovrint:False,ptlb:Multiplier,ptin:_Multiplier,varname:node_7688,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:5494,x:33240,y:32785,varname:node_5494,prsc:2|A-9929-OUT,B-3444-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3444,x:33042,y:32921,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_3444,prsc:2,glob:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:4787,x:33219,y:32514,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_4787,prsc:2,tex:6842eab8ae712bb4d8d4ca8538fb3bc0,ntxv:0,isnm:False;proporder:6500-7688-3444-4787;pass:END;sub:END;*/

Shader "Shader Forge/RainbowHighlight" {
    Properties {
        _Lines ("Lines", 2D) = "white" {}
        _Multiplier ("Multiplier", Float ) = 1
        _Intensity ("Intensity", Float ) = 1
        _MainTex ("MainTex", 2D) = "white" {}
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
            uniform sampler2D _Lines; uniform float4 _Lines_ST;
            uniform float _Multiplier;
            uniform float _Intensity;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * _MainTex_var.rgb;
////// Emissive:
                float4 _Lines_var = tex2D(_Lines,TRANSFORM_TEX(i.uv0, _Lines));
                float4 node_1435 = _Time + _TimeEditor;
                float node_6851 = (node_1435.g*_Multiplier);
                float3 emissive = ((_Lines_var.rgb*lerp(lerp(float3(1,0,0),float3(0,1,0),(sin(node_6851)*0.5+0.5)),float3(0,0,1),(cos(node_6851)*0.5+0.5)))*_Intensity);
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
            uniform sampler2D _Lines; uniform float4 _Lines_ST;
            uniform float _Multiplier;
            uniform float _Intensity;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuse = directDiffuse * _MainTex_var.rgb;
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
