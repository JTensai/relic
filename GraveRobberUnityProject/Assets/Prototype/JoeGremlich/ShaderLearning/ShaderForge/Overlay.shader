// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1226211,fgcg:0.3970588,fgcb:0.3177967,fgca:1,fgde:0.025,fgrn:10,fgrf:30,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9945,x:33134,y:32729,varname:node_9945,prsc:2|diff-5680-OUT;n:type:ShaderForge.SFN_Tex2d,id:844,x:32453,y:33189,ptovrint:False,ptlb:Top,ptin:_Top,varname:node_844,prsc:2,tex:9d51ae1f67675554a8a04f64a125800e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1012,x:32403,y:32785,ptovrint:False,ptlb:Bottom,ptin:_Bottom,varname:node_1012,prsc:2,tex:76d5e4891499d4b4da7af02b7ae4fee1,ntxv:0,isnm:False;n:type:ShaderForge.SFN_If,id:5680,x:32850,y:32855,varname:node_5680,prsc:2|A-1012-RGB,B-8284-OUT,GT-7929-OUT,EQ-7929-OUT,LT-437-OUT;n:type:ShaderForge.SFN_Vector1,id:8284,x:32403,y:32940,varname:node_8284,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:437,x:32666,y:33052,varname:node_437,prsc:2|A-1012-RGB,B-844-RGB,C-9997-OUT;n:type:ShaderForge.SFN_Vector1,id:9997,x:32453,y:33111,varname:node_9997,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:7929,x:32734,y:33368,varname:node_7929,prsc:2|A-9786-OUT,B-3407-OUT,C-3056-OUT;n:type:ShaderForge.SFN_Vector1,id:9786,x:32453,y:33391,varname:node_9786,prsc:2,v1:2;n:type:ShaderForge.SFN_Subtract,id:3407,x:32480,y:33534,varname:node_3407,prsc:2|A-9820-OUT,B-844-RGB;n:type:ShaderForge.SFN_Vector1,id:9820,x:32188,y:33524,varname:node_9820,prsc:2,v1:1;n:type:ShaderForge.SFN_Subtract,id:3056,x:32493,y:33716,varname:node_3056,prsc:2|A-9820-OUT,B-1012-RGB;proporder:844-1012;pass:END;sub:END;*/

Shader "Shader Forge/Overlay" {
    Properties {
        _Top ("Top", 2D) = "white" {}
        _Bottom ("Bottom", 2D) = "white" {}
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
            uniform sampler2D _Top; uniform float4 _Top_ST;
            uniform sampler2D _Bottom; uniform float4 _Bottom_ST;
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
                float4 _Bottom_var = tex2D(_Bottom,TRANSFORM_TEX(i.uv0, _Bottom));
                float node_5680_if_leA = step(_Bottom_var.rgb,0.5);
                float node_5680_if_leB = step(0.5,_Bottom_var.rgb);
                float4 _Top_var = tex2D(_Top,TRANSFORM_TEX(i.uv0, _Top));
                float node_9820 = 1.0;
                float3 node_7929 = (2.0*(node_9820-_Top_var.rgb)*(node_9820-_Bottom_var.rgb));
                float3 diffuse = (directDiffuse + indirectDiffuse) * lerp((node_5680_if_leA*(_Bottom_var.rgb*_Top_var.rgb*2.0))+(node_5680_if_leB*node_7929),node_7929,node_5680_if_leA*node_5680_if_leB);
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
            uniform sampler2D _Top; uniform float4 _Top_ST;
            uniform sampler2D _Bottom; uniform float4 _Bottom_ST;
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
                float4 _Bottom_var = tex2D(_Bottom,TRANSFORM_TEX(i.uv0, _Bottom));
                float node_5680_if_leA = step(_Bottom_var.rgb,0.5);
                float node_5680_if_leB = step(0.5,_Bottom_var.rgb);
                float4 _Top_var = tex2D(_Top,TRANSFORM_TEX(i.uv0, _Top));
                float node_9820 = 1.0;
                float3 node_7929 = (2.0*(node_9820-_Top_var.rgb)*(node_9820-_Bottom_var.rgb));
                float3 diffuse = directDiffuse * lerp((node_5680_if_leA*(_Bottom_var.rgb*_Top_var.rgb*2.0))+(node_5680_if_leB*node_7929),node_7929,node_5680_if_leA*node_5680_if_leB);
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
