// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7307,x:34218,y:32621,varname:node_7307,prsc:2|diff-9929-OUT,spec-3533-OUT,normal-101-OUT,emission-7594-RGB;n:type:ShaderForge.SFN_Panner,id:8270,x:32506,y:32612,varname:node_8270,prsc:2,spu:0.025,spv:0.03;n:type:ShaderForge.SFN_Tex2d,id:4952,x:32834,y:32394,ptovrint:False,ptlb:Diffuse Map,ptin:_DiffuseMap,varname:node_4952,prsc:2,tex:de8863bf27aa9ec41ad715e18fc5cb94,ntxv:0,isnm:False|UVIN-8270-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2514,x:33183,y:32593,ptovrint:False,ptlb:Normal Map 1,ptin:_NormalMap1,varname:node_2514,prsc:2,tex:38bd5c730e17ffa4bb36404a0529547d,ntxv:3,isnm:True|UVIN-8270-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:1126,x:32739,y:31781,ptovrint:False,ptlb:Lightning,ptin:_Lightning,varname:node_1126,prsc:2,tex:91951ea79518672429acf8599338a9b0,ntxv:3,isnm:True|UVIN-5617-OUT;n:type:ShaderForge.SFN_Tex2d,id:7435,x:31953,y:32266,ptovrint:False,ptlb:Lightning Alpha,ptin:_LightningAlpha,varname:node_7435,prsc:2,tex:fb6566c21f717904f83743a5a76dd0b0,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Time,id:7137,x:32220,y:32096,varname:node_7137,prsc:2;n:type:ShaderForge.SFN_Power,id:3291,x:32240,y:32334,varname:node_3291,prsc:2|VAL-7435-R,EXP-5501-OUT;n:type:ShaderForge.SFN_Vector1,id:5501,x:31953,y:32509,varname:node_5501,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:4232,x:32613,y:32241,varname:node_4232,prsc:2|A-601-OUT,B-6591-OUT;n:type:ShaderForge.SFN_Frac,id:1296,x:32778,y:32162,varname:node_1296,prsc:2|IN-4232-OUT;n:type:ShaderForge.SFN_Multiply,id:1407,x:33203,y:32000,varname:node_1407,prsc:2|A-1126-R,B-9250-OUT;n:type:ShaderForge.SFN_Panner,id:5097,x:32371,y:31650,varname:node_5097,prsc:2,spu:0.01,spv:0.01;n:type:ShaderForge.SFN_Multiply,id:5617,x:32563,y:31781,varname:node_5617,prsc:2|A-5097-UVOUT,B-9254-OUT;n:type:ShaderForge.SFN_Vector1,id:9254,x:32371,y:31859,varname:node_9254,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:2876,x:33515,y:32437,varname:node_2876,prsc:2|A-4952-RGB,B-5174-OUT;n:type:ShaderForge.SFN_Multiply,id:6591,x:32442,y:32334,varname:node_6591,prsc:2|A-3291-OUT,B-6419-OUT;n:type:ShaderForge.SFN_Vector1,id:6419,x:32240,y:32549,varname:node_6419,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Multiply,id:5174,x:33337,y:31833,varname:node_5174,prsc:2|A-5065-RGB,B-1407-OUT;n:type:ShaderForge.SFN_Color,id:5065,x:33110,y:31737,ptovrint:False,ptlb:Crust Color,ptin:_CrustColor,varname:node_5065,prsc:2,glob:False,c1:0.3622629,c2:0.7867647,c3:0.01735512,c4:1;n:type:ShaderForge.SFN_Tex2d,id:9253,x:32571,y:33038,ptovrint:False,ptlb:Noise Map,ptin:_NoiseMap,varname:node_9253,prsc:2,tex:91951ea79518672429acf8599338a9b0,ntxv:3,isnm:True|UVIN-2802-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8197,x:33183,y:32779,ptovrint:False,ptlb:Normal Map 2,ptin:_NormalMap2,varname:node_8197,prsc:2,tex:2dd3788f8589b40bf82a92d76ffc5091,ntxv:3,isnm:True|UVIN-8270-UVOUT;n:type:ShaderForge.SFN_Panner,id:2802,x:32285,y:32988,varname:node_2802,prsc:2,spu:0,spv:0;n:type:ShaderForge.SFN_Time,id:6308,x:32374,y:33209,varname:node_6308,prsc:2;n:type:ShaderForge.SFN_Add,id:9081,x:32795,y:33160,varname:node_9081,prsc:2|A-9253-R,B-4265-OUT;n:type:ShaderForge.SFN_Lerp,id:101,x:33824,y:32739,varname:node_101,prsc:2|A-3771-OUT,B-8197-RGB,T-8372-OUT;n:type:ShaderForge.SFN_Frac,id:5211,x:32969,y:33160,varname:node_5211,prsc:2|IN-9081-OUT;n:type:ShaderForge.SFN_Vector1,id:444,x:32368,y:33422,varname:node_444,prsc:2,v1:0.05;n:type:ShaderForge.SFN_Multiply,id:4265,x:32587,y:33261,varname:node_4265,prsc:2|A-6308-T,B-444-OUT;n:type:ShaderForge.SFN_If,id:8260,x:33430,y:33173,varname:node_8260,prsc:2|A-5211-OUT,B-5367-OUT,GT-1218-OUT,EQ-5211-OUT,LT-5211-OUT;n:type:ShaderForge.SFN_Vector1,id:5367,x:33114,y:33093,varname:node_5367,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:2934,x:33402,y:33397,varname:node_2934,prsc:2,v1:2;n:type:ShaderForge.SFN_OneMinus,id:1218,x:33158,y:33255,varname:node_1218,prsc:2|IN-5211-OUT;n:type:ShaderForge.SFN_Multiply,id:8372,x:33654,y:33278,varname:node_8372,prsc:2|A-8260-OUT,B-2934-OUT;n:type:ShaderForge.SFN_Multiply,id:601,x:32442,y:32142,varname:node_601,prsc:2|A-7137-T,B-3751-OUT;n:type:ShaderForge.SFN_Vector1,id:3751,x:32259,y:32257,varname:node_3751,prsc:2,v1:0.05;n:type:ShaderForge.SFN_If,id:9250,x:33106,y:32161,varname:node_9250,prsc:2|A-1296-OUT,B-5081-OUT,GT-9075-OUT,EQ-1296-OUT,LT-1296-OUT;n:type:ShaderForge.SFN_Vector1,id:5081,x:32805,y:32048,varname:node_5081,prsc:2,v1:0.5;n:type:ShaderForge.SFN_OneMinus,id:9075,x:32933,y:32204,varname:node_9075,prsc:2|IN-1296-OUT;n:type:ShaderForge.SFN_Multiply,id:3771,x:33515,y:32627,varname:node_3771,prsc:2|A-2514-RGB,B-8559-OUT;n:type:ShaderForge.SFN_Vector1,id:8559,x:33331,y:32717,varname:node_8559,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Clamp01,id:9929,x:33737,y:32437,varname:node_9929,prsc:2|IN-2876-OUT;n:type:ShaderForge.SFN_Color,id:7594,x:33947,y:32861,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_7594,prsc:2,glob:False,c1:0.15,c2:0.35,c3:0,c4:1;n:type:ShaderForge.SFN_ComponentMask,id:3533,x:33894,y:32572,varname:node_3533,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-101-OUT;proporder:4952-2514-1126-7435-5065-9253-8197-7594;pass:END;sub:END;*/

Shader "Shader Forge/ToxicWaterSurface" {
    Properties {
        _DiffuseMap ("Diffuse Map", 2D) = "white" {}
        _NormalMap1 ("Normal Map 1", 2D) = "bump" {}
        _Lightning ("Lightning", 2D) = "bump" {}
        _LightningAlpha ("Lightning Alpha", 2D) = "bump" {}
        _CrustColor ("Crust Color", Color) = (0.3622629,0.7867647,0.01735512,1)
        _NoiseMap ("Noise Map", 2D) = "bump" {}
        _NormalMap2 ("Normal Map 2", 2D) = "bump" {}
        _Emission ("Emission", Color) = (0.15,0.35,0,1)
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
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform sampler2D _NormalMap1; uniform float4 _NormalMap1_ST;
            uniform sampler2D _Lightning; uniform float4 _Lightning_ST;
            uniform sampler2D _LightningAlpha; uniform float4 _LightningAlpha_ST;
            uniform float4 _CrustColor;
            uniform sampler2D _NoiseMap; uniform float4 _NoiseMap_ST;
            uniform sampler2D _NormalMap2; uniform float4 _NormalMap2_ST;
            uniform float4 _Emission;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_5452 = _Time + _TimeEditor;
                float2 node_8270 = (i.uv0+node_5452.g*float2(0.025,0.03));
                float3 _NormalMap1_var = UnpackNormal(tex2D(_NormalMap1,TRANSFORM_TEX(node_8270, _NormalMap1)));
                float3 _NormalMap2_var = UnpackNormal(tex2D(_NormalMap2,TRANSFORM_TEX(node_8270, _NormalMap2)));
                float2 node_2802 = (i.uv0+node_5452.g*float2(0,0));
                float3 _NoiseMap_var = UnpackNormal(tex2D(_NoiseMap,TRANSFORM_TEX(node_2802, _NoiseMap)));
                float4 node_6308 = _Time + _TimeEditor;
                float node_5211 = frac((_NoiseMap_var.r+(node_6308.g*0.05)));
                float node_8260_if_leA = step(node_5211,0.5);
                float node_8260_if_leB = step(0.5,node_5211);
                float3 node_101 = lerp((_NormalMap1_var.rgb*0.5),_NormalMap2_var.rgb,(lerp((node_8260_if_leA*node_5211)+(node_8260_if_leB*(1.0 - node_5211)),node_5211,node_8260_if_leA*node_8260_if_leB)*2.0));
                float3 normalLocal = node_101;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_3533 = node_101.r;
                float3 specularColor = float3(node_3533,node_3533,node_3533);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 indirectDiffuse = float3(0,0,0);
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _DiffuseMap_var = tex2D(_DiffuseMap,TRANSFORM_TEX(node_8270, _DiffuseMap));
                float2 node_5617 = ((i.uv0+node_5452.g*float2(0.01,0.01))*1.0);
                float3 _Lightning_var = UnpackNormal(tex2D(_Lightning,TRANSFORM_TEX(node_5617, _Lightning)));
                float4 node_7137 = _Time + _TimeEditor;
                float3 _LightningAlpha_var = UnpackNormal(tex2D(_LightningAlpha,TRANSFORM_TEX(i.uv0, _LightningAlpha)));
                float node_1296 = frac(((node_7137.g*0.05)+(pow(_LightningAlpha_var.r,0.5)*0.25)));
                float node_9250_if_leA = step(node_1296,0.5);
                float node_9250_if_leB = step(0.5,node_1296);
                float3 diffuse = (directDiffuse + indirectDiffuse) * saturate((_DiffuseMap_var.rgb+(_CrustColor.rgb*(_Lightning_var.r*lerp((node_9250_if_leA*node_1296)+(node_9250_if_leB*(1.0 - node_1296)),node_1296,node_9250_if_leA*node_9250_if_leB)))));
////// Emissive:
                float3 emissive = _Emission.rgb;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
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
            uniform sampler2D _DiffuseMap; uniform float4 _DiffuseMap_ST;
            uniform sampler2D _NormalMap1; uniform float4 _NormalMap1_ST;
            uniform sampler2D _Lightning; uniform float4 _Lightning_ST;
            uniform sampler2D _LightningAlpha; uniform float4 _LightningAlpha_ST;
            uniform float4 _CrustColor;
            uniform sampler2D _NoiseMap; uniform float4 _NoiseMap_ST;
            uniform sampler2D _NormalMap2; uniform float4 _NormalMap2_ST;
            uniform float4 _Emission;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_349 = _Time + _TimeEditor;
                float2 node_8270 = (i.uv0+node_349.g*float2(0.025,0.03));
                float3 _NormalMap1_var = UnpackNormal(tex2D(_NormalMap1,TRANSFORM_TEX(node_8270, _NormalMap1)));
                float3 _NormalMap2_var = UnpackNormal(tex2D(_NormalMap2,TRANSFORM_TEX(node_8270, _NormalMap2)));
                float2 node_2802 = (i.uv0+node_349.g*float2(0,0));
                float3 _NoiseMap_var = UnpackNormal(tex2D(_NoiseMap,TRANSFORM_TEX(node_2802, _NoiseMap)));
                float4 node_6308 = _Time + _TimeEditor;
                float node_5211 = frac((_NoiseMap_var.r+(node_6308.g*0.05)));
                float node_8260_if_leA = step(node_5211,0.5);
                float node_8260_if_leB = step(0.5,node_5211);
                float3 node_101 = lerp((_NormalMap1_var.rgb*0.5),_NormalMap2_var.rgb,(lerp((node_8260_if_leA*node_5211)+(node_8260_if_leB*(1.0 - node_5211)),node_5211,node_8260_if_leA*node_8260_if_leB)*2.0));
                float3 normalLocal = node_101;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_3533 = node_101.r;
                float3 specularColor = float3(node_3533,node_3533,node_3533);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _DiffuseMap_var = tex2D(_DiffuseMap,TRANSFORM_TEX(node_8270, _DiffuseMap));
                float2 node_5617 = ((i.uv0+node_349.g*float2(0.01,0.01))*1.0);
                float3 _Lightning_var = UnpackNormal(tex2D(_Lightning,TRANSFORM_TEX(node_5617, _Lightning)));
                float4 node_7137 = _Time + _TimeEditor;
                float3 _LightningAlpha_var = UnpackNormal(tex2D(_LightningAlpha,TRANSFORM_TEX(i.uv0, _LightningAlpha)));
                float node_1296 = frac(((node_7137.g*0.05)+(pow(_LightningAlpha_var.r,0.5)*0.25)));
                float node_9250_if_leA = step(node_1296,0.5);
                float node_9250_if_leB = step(0.5,node_1296);
                float3 diffuse = directDiffuse * saturate((_DiffuseMap_var.rgb+(_CrustColor.rgb*(_Lightning_var.r*lerp((node_9250_if_leA*node_1296)+(node_9250_if_leB*(1.0 - node_1296)),node_1296,node_9250_if_leA*node_9250_if_leB)))));
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
