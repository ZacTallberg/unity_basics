Shader "Hexasphere/HexaTileTextured" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _TileAlpha ("Tile Alpha", Float) = 1
        _MainTex ("Texture", 2D) = "white"
		[HideInInspector] _SrcBlend ("__src", Float) = 1.0
		[HideInInspector] _DstBlend ("__dst", Float) = 0.0
		[HideInInspector] _ZWrite ("__zw", Float) = 1.0
    }
    SubShader {
    	Tags { "Queue" = "Geometry-2" "RenderPipeline" = "" }
        Pass {
       		Tags { "LightMode" = "ForwardBase" }
	      	Blend [_SrcBlend] [_DstBlend]
	      	ZWrite [_ZWrite]
            Offset 1, 1

                CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma multi_compile_fwdbase nolightmap nodynlightmap novertexlight nodirlightmap
				#include "UnityCG.cginc"
				#include "AutoLight.cginc"

				fixed4 _Color;
				sampler2D _MainTex;
				fixed _TileAlpha;

                struct appdata {
    				float4 vertex   : POSITION;
    				float2 texcoord : TEXCOORD0;
    			};

				struct v2f {
	    			float4 pos      : SV_POSITION;
	    			float2 uv       : TEXCOORD0;
	    			SHADOW_COORDS(1)
				};

				v2f vert(appdata v) {
    				v2f o;
	                o.pos = UnityObjectToClipPos(v.vertex);
	                o.uv = v.texcoord;
	                TRANSFER_SHADOW(o);
    				return o;
    			}
    		
    			fixed4 frag (v2f i) : SV_Target {
    				fixed4 color = tex2D(_MainTex, i.uv);
    				fixed atten = SHADOW_ATTENUATION(i);
    				color *= _Color;
    				color.rgb *= atten;
    				color.a *= _TileAlpha;
    				return color;
                }
                ENDCG

        }
    }

    SubShader {
        Tags { "Queue" = "Geometry-2" "RenderPipeline" = "LightweightPipeline" }
        Pass {
           // Tags { "LightMode" = "LightweightForward" }
           	Blend [_SrcBlend] [_DstBlend]
			ZWrite [_ZWrite]
            Offset 1, 1

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #pragma multi_compile_fwdbase nolightmap nodynlightmap novertexlight nodirlightmap
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"

                fixed4 _Color;
                sampler2D _MainTex;
                fixed _TileAlpha;

                struct appdata {
                    float4 vertex   : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f {
                    float4 pos      : SV_POSITION;
                    float2 uv       : TEXCOORD0;
                    SHADOW_COORDS(1)
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    TRANSFER_SHADOW(o);
                    return o;
                }
            
                fixed4 frag (v2f i) : SV_Target {
                    fixed4 color = tex2D(_MainTex, i.uv);
                    fixed atten = SHADOW_ATTENUATION(i);
    				color *= _Color;
    				color.rgb *= atten;
    				color.a *= _TileAlpha;
    				return color;
                }
                ENDCG

        }
    }

    Fallback "Diffuse"
}