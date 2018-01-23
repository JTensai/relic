// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:False,aust:False,igpj:True,qofs:905,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:7386,x:33393,y:32691,varname:node_7386,prsc:2|emission-9659-OUT;n:type:ShaderForge.SFN_VertexColor,id:8124,x:32703,y:32889,varname:node_8124,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:7097,x:32703,y:32680,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_7097,prsc:2,tex:bb5af181f83f782459a1ecd6d061f18f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7338,x:32946,y:32752,varname:node_7338,prsc:2|A-7097-RGB,B-8124-RGB,C-8124-A;n:type:ShaderForge.SFN_DepthBlend,id:6760,x:33003,y:33021,varname:node_6760,prsc:2|DIST-8959-OUT;n:type:ShaderForge.SFN_Slider,id:8959,x:32670,y:33055,ptovrint:False,ptlb:Softnes,ptin:_Softnes,varname:node_8959,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:9659,x:33143,y:32874,varname:node_9659,prsc:2|A-7338-OUT,B-6760-OUT;proporder:7097-8959;pass:END;sub:END;*/

Shader "Shader Forge/Smoke" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _Softnes ("Softnes", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+905"
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
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Softnes;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 emissive = ((_MainTexture_var.rgb*i.vertexColor.rgb*i.vertexColor.a)*saturate((sceneZ-partZ)/_Softnes));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
