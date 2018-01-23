// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:3,bsrc:0,bdst:6,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4426,x:33487,y:32721,varname:node_4426,prsc:2|emission-8490-OUT;n:type:ShaderForge.SFN_Color,id:3661,x:32604,y:32751,ptovrint:False,ptlb:Water,ptin:_Water,varname:node_3661,prsc:2,glob:False,c1:0.6849589,c2:0.7573529,c3:0.7423748,c4:1;n:type:ShaderForge.SFN_DepthBlend,id:734,x:32707,y:33035,varname:node_734,prsc:2|DIST-7152-OUT;n:type:ShaderForge.SFN_Slider,id:7152,x:32380,y:33035,ptovrint:False,ptlb:Softness,ptin:_Softness,varname:node_7152,prsc:2,min:0,cur:0.4,max:1;n:type:ShaderForge.SFN_Multiply,id:8490,x:33032,y:32836,varname:node_8490,prsc:2|A-3661-RGB,B-734-OUT;proporder:3661-7152;pass:END;sub:END;*/

Shader "Shader Forge/WaterDepth" {
    Properties {
        _Water ("Water", Color) = (0.6849589,0.7573529,0.7423748,1)
        _Softness ("Softness", Range(0, 1)) = 0.4
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
            Blend One OneMinusSrcColor
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _Water;
            uniform float _Softness;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 projPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
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
                float3 emissive = (_Water.rgb*saturate((sceneZ-partZ)/_Softness));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
