// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:8890,x:33611,y:32431,varname:node_8890,prsc:2|diff-7620-RGB,alpha-4945-OUT;n:type:ShaderForge.SFN_Tex2d,id:7620,x:33262,y:32651,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_7620,prsc:2,tex:5c1bdcc4915f1014f9ea9fd5e76be435,ntxv:2,isnm:False|UVIN-6977-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3947,x:33230,y:32941,ptovrint:False,ptlb:node_3947,ptin:_node_3947,varname:node_3947,prsc:2,tex:0c95a329b7376cc43a6a7b8a37a77ecd,ntxv:0,isnm:False|UVIN-4988-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:159,x:32466,y:32386,varname:node_159,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3157,x:32849,y:32448,varname:node_3157,prsc:2|A-159-UVOUT,B-4564-OUT;n:type:ShaderForge.SFN_Add,id:6900,x:33045,y:32463,varname:node_6900,prsc:2|A-3157-OUT,B-5179-OUT;n:type:ShaderForge.SFN_Append,id:4564,x:32543,y:32678,varname:node_4564,prsc:2|A-8601-OUT,B-8601-OUT;n:type:ShaderForge.SFN_Divide,id:5179,x:32920,y:32657,varname:node_5179,prsc:2|A-902-OUT,B-6576-OUT;n:type:ShaderForge.SFN_Vector1,id:6576,x:32731,y:32872,varname:node_6576,prsc:2,v1:2;n:type:ShaderForge.SFN_OneMinus,id:902,x:32731,y:32704,varname:node_902,prsc:2|IN-4564-OUT;n:type:ShaderForge.SFN_Time,id:6939,x:31755,y:32604,varname:node_6939,prsc:2;n:type:ShaderForge.SFN_Frac,id:5611,x:32192,y:32688,varname:node_5611,prsc:2|IN-3350-OUT;n:type:ShaderForge.SFN_OneMinus,id:8601,x:32370,y:32688,varname:node_8601,prsc:2|IN-5611-OUT;n:type:ShaderForge.SFN_Multiply,id:3350,x:32033,y:32688,varname:node_3350,prsc:2|A-6939-T,B-1553-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1553,x:31834,y:32830,ptovrint:False,ptlb:AlphaZoomSpeed,ptin:_AlphaZoomSpeed,varname:node_1553,prsc:2,glob:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:4945,x:33425,y:32805,varname:node_4945,prsc:2|A-7620-A,B-3947-A;n:type:ShaderForge.SFN_Rotator,id:6977,x:33224,y:32432,varname:node_6977,prsc:2|UVIN-6900-OUT,SPD-4727-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4727,x:32933,y:32828,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_4727,prsc:2,glob:False,v1:0.5;n:type:ShaderForge.SFN_TexCoord,id:7768,x:32465,y:33096,varname:node_7768,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:2499,x:32465,y:33099,varname:node_2499,prsc:2,uv:0;n:type:ShaderForge.SFN_Rotator,id:4988,x:33034,y:32902,varname:node_4988,prsc:2|UVIN-8815-UVOUT,SPD-8583-OUT;n:type:ShaderForge.SFN_TexCoord,id:8815,x:32378,y:32218,varname:node_8815,prsc:2,uv:0;n:type:ShaderForge.SFN_Negate,id:8583,x:33022,y:33039,varname:node_8583,prsc:2|IN-4727-OUT;proporder:7620-3947-1553-4727;pass:END;sub:END;*/

Shader "Shader Forge/AlphaStuff" {
    Properties {
        _MainTex ("MainTex", 2D) = "black" {}
        _node_3947 ("node_3947", 2D) = "white" {}
        _AlphaZoomSpeed ("AlphaZoomSpeed", Float ) = 0.5
        _speed ("speed", Float ) = 0.5
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _node_3947; uniform float4 _node_3947_ST;
            uniform float _AlphaZoomSpeed;
            uniform float _speed;
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
                float4 node_8112 = _Time + _TimeEditor;
                float node_6977_ang = node_8112.g;
                float node_6977_spd = _speed;
                float node_6977_cos = cos(node_6977_spd*node_6977_ang);
                float node_6977_sin = sin(node_6977_spd*node_6977_ang);
                float2 node_6977_piv = float2(0.5,0.5);
                float4 node_6939 = _Time + _TimeEditor;
                float node_8601 = (1.0 - frac((node_6939.g*_AlphaZoomSpeed)));
                float2 node_4564 = float2(node_8601,node_8601);
                float2 node_6977 = (mul(((i.uv0*node_4564)+((1.0 - node_4564)/2.0))-node_6977_piv,float2x2( node_6977_cos, -node_6977_sin, node_6977_sin, node_6977_cos))+node_6977_piv);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_6977, _MainTex));
                float3 diffuse = (directDiffuse + indirectDiffuse) * _MainTex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                float node_4988_ang = node_8112.g;
                float node_4988_spd = (-1*_speed);
                float node_4988_cos = cos(node_4988_spd*node_4988_ang);
                float node_4988_sin = sin(node_4988_spd*node_4988_ang);
                float2 node_4988_piv = float2(0.5,0.5);
                float2 node_4988 = (mul(i.uv0-node_4988_piv,float2x2( node_4988_cos, -node_4988_sin, node_4988_sin, node_4988_cos))+node_4988_piv);
                float4 _node_3947_var = tex2D(_node_3947,TRANSFORM_TEX(node_4988, _node_3947));
                return fixed4(finalColor,(_MainTex_var.a*_node_3947_var.a));
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _node_3947; uniform float4 _node_3947_ST;
            uniform float _AlphaZoomSpeed;
            uniform float _speed;
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
                float4 node_9990 = _Time + _TimeEditor;
                float node_6977_ang = node_9990.g;
                float node_6977_spd = _speed;
                float node_6977_cos = cos(node_6977_spd*node_6977_ang);
                float node_6977_sin = sin(node_6977_spd*node_6977_ang);
                float2 node_6977_piv = float2(0.5,0.5);
                float4 node_6939 = _Time + _TimeEditor;
                float node_8601 = (1.0 - frac((node_6939.g*_AlphaZoomSpeed)));
                float2 node_4564 = float2(node_8601,node_8601);
                float2 node_6977 = (mul(((i.uv0*node_4564)+((1.0 - node_4564)/2.0))-node_6977_piv,float2x2( node_6977_cos, -node_6977_sin, node_6977_sin, node_6977_cos))+node_6977_piv);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_6977, _MainTex));
                float3 diffuse = directDiffuse * _MainTex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse;
                float node_4988_ang = node_9990.g;
                float node_4988_spd = (-1*_speed);
                float node_4988_cos = cos(node_4988_spd*node_4988_ang);
                float node_4988_sin = sin(node_4988_spd*node_4988_ang);
                float2 node_4988_piv = float2(0.5,0.5);
                float2 node_4988 = (mul(i.uv0-node_4988_piv,float2x2( node_4988_cos, -node_4988_sin, node_4988_sin, node_4988_cos))+node_4988_piv);
                float4 _node_3947_var = tex2D(_node_3947,TRANSFORM_TEX(node_4988, _node_3947));
                return fixed4(finalColor * (_MainTex_var.a*_node_3947_var.a),0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
