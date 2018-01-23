// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3400,x:34174,y:32764,varname:node_3400,prsc:2|emission-874-OUT,alpha-7292-OUT;n:type:ShaderForge.SFN_Color,id:9780,x:33348,y:32701,ptovrint:False,ptlb:Color1,ptin:_Color1,varname:node_9780,prsc:2,glob:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_DepthBlend,id:8691,x:32875,y:33042,varname:node_8691,prsc:2|DIST-6769-OUT;n:type:ShaderForge.SFN_Slider,id:59,x:32426,y:33020,ptovrint:False,ptlb:Softness,ptin:_Softness,varname:node_59,prsc:2,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Multiply,id:6019,x:33822,y:32959,varname:node_6019,prsc:2|A-4760-OUT,B-4792-OUT;n:type:ShaderForge.SFN_Add,id:874,x:33999,y:32868,varname:node_874,prsc:2|A-5257-RGB,B-6019-OUT;n:type:ShaderForge.SFN_Color,id:5257,x:33822,y:32817,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_5257,prsc:2,glob:False,c1:0.5961818,c2:0.3545091,c3:0.6102941,c4:1;n:type:ShaderForge.SFN_Code,id:4792,x:33144,y:33028,varname:node_4792,prsc:2,code:aQBmACgAaQBuAHAAdQB0ACAAPgAgADAALgAwADUAIAAmACYAIABpAG4AcAB1AHQAIAA8ACAAMAAuADUAKQAKAAkAcgBlAHQAdQByAG4AIAAxADsACgBlAGwAcwBlAAoACQByAGUAdAB1AHIAbgAgADAAOwA=,output:0,fname:FakeGaussian,width:480,height:266,input:0,input_1_label:input|A-8691-OUT;n:type:ShaderForge.SFN_Tex2d,id:1812,x:32955,y:32837,ptovrint:False,ptlb:Clouds2,ptin:_Clouds2,varname:node_1812,prsc:2,tex:e65442ae71e67e04399cedb35a3cfa1e,ntxv:0,isnm:False|UVIN-7734-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4760,x:33567,y:32804,varname:node_4760,prsc:2|A-9780-RGB,B-6136-OUT;n:type:ShaderForge.SFN_Multiply,id:6136,x:33348,y:32848,varname:node_6136,prsc:2|A-8119-OUT,B-7886-OUT;n:type:ShaderForge.SFN_Tex2d,id:5081,x:32955,y:32625,ptovrint:False,ptlb:Clouds1,ptin:_Clouds1,varname:node_5081,prsc:2,tex:abdac6d030ac002469f6905fb7287ce6,ntxv:0,isnm:False|UVIN-107-UVOUT;n:type:ShaderForge.SFN_Panner,id:107,x:32794,y:32625,varname:node_107,prsc:2,spu:0,spv:0.1;n:type:ShaderForge.SFN_Panner,id:7734,x:32794,y:32837,varname:node_7734,prsc:2,spu:0,spv:-0.1;n:type:ShaderForge.SFN_RemapRange,id:8119,x:33151,y:32625,varname:node_8119,prsc:2,frmn:0,frmx:1,tomn:0.4,tomx:1|IN-5081-RGB;n:type:ShaderForge.SFN_RemapRange,id:7886,x:33151,y:32802,varname:node_7886,prsc:2,frmn:0,frmx:1,tomn:0.4,tomx:1|IN-1812-RGB;n:type:ShaderForge.SFN_Time,id:8634,x:32353,y:33185,varname:node_8634,prsc:2;n:type:ShaderForge.SFN_Sin,id:3370,x:32524,y:33206,varname:node_3370,prsc:2|IN-8634-T;n:type:ShaderForge.SFN_RemapRange,id:6769,x:32692,y:33206,varname:node_6769,prsc:2,frmn:-1,frmx:1,tomn:0.2,tomx:1|IN-3370-OUT;n:type:ShaderForge.SFN_Slider,id:7292,x:33799,y:33116,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_7292,prsc:2,min:0,cur:0,max:1;proporder:9780-59-5257-1812-5081-7292;pass:END;sub:END;*/

Shader "Shader Forge/WaterSurface" {
    Properties {
        _Color1 ("Color1", Color) = (1,0,0,1)
        _Softness ("Softness", Range(0, 1)) = 0.5
        _Color2 ("Color2", Color) = (0.5961818,0.3545091,0.6102941,1)
        _Clouds2 ("Clouds2", 2D) = "white" {}
        _Clouds1 ("Clouds1", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0
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
            Cull Off
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
            uniform float4 _TimeEditor;
            uniform float4 _Color1;
            uniform float4 _Color2;
            float FakeGaussian( float input ){
            if(input > 0.05 && input < 0.5)
            	return 1;
            else
            	return 0;
            }
            
            uniform sampler2D _Clouds2; uniform float4 _Clouds2_ST;
            uniform sampler2D _Clouds1; uniform float4 _Clouds1_ST;
            uniform float _Opacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
                float4 node_996 = _Time + _TimeEditor;
                float2 node_107 = (i.uv0+node_996.g*float2(0,0.1));
                float4 _Clouds1_var = tex2D(_Clouds1,TRANSFORM_TEX(node_107, _Clouds1));
                float2 node_7734 = (i.uv0+node_996.g*float2(0,-0.1));
                float4 _Clouds2_var = tex2D(_Clouds2,TRANSFORM_TEX(node_7734, _Clouds2));
                float4 node_8634 = _Time + _TimeEditor;
                float3 emissive = (_Color2.rgb+((_Color1.rgb*((_Clouds1_var.rgb*0.6+0.4)*(_Clouds2_var.rgb*0.6+0.4)))*FakeGaussian( saturate((sceneZ-partZ)/(sin(node_8634.g)*0.4+0.6)) )));
                float3 finalColor = emissive;
                return fixed4(finalColor,_Opacity);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
