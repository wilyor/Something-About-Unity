// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "MyShader/Diffuse With LightProbes" 
{
	Properties{ 
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {} 
	}
	SubShader{
		Pass {
			Tags {
				"LightMode" = "ForwardBase"
			}
			CGPROGRAM
			#pragma vertex v
			#pragma fragment f
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 vlight : TEXCOORD1;
			};
			v2f v(appdata_base vertex_data) {
				v2f o;
				o.vertex = UnityObjectToClipPos(vertex_data.vertex);
				o.uv = vertex_data.texcoord;
				float3 worldNormal = mul((float3x3)UNITY_MATRIX_IT_MV, vertex_data.normal);
				o.vlight = ShadeSH9(float4(worldNormal, 1.0));
				return o;
			}
			fixed4 f(v2f input_fragment) : SV_Target {
				fixed4 col = tex2D(_MainTex, input_fragment.uv);
				col.rgb *= input_fragment.vlight;
				return col;
			}
			ENDCG
		}
	}
}
