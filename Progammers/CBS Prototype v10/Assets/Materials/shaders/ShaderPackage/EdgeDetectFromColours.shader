Shader "Custom/EdgeDetecFromColor" {
	Properties{
		//_VolTex("Texture", 3D) = "" {}
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Threshold("Threshold for finding edges", Float) = 1.0
		_SampleDistance("Affects outline width", Float) = 1.0
		_EdgesOnly("Render in colours or just white", Float) = 1.0
	}
		SubShader{
		Pass
	{
		Cull Back

		CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

	struct vs_input
	{
		float4 vertex : POSITION;
		float2 uv[5] : TEXCOORD0;
	};

	struct ps_input
	{
		float4 pos : SV_POSITION;
		float2 uv[3] : TEXCOORD0;
		//float4 screenCoord : TEXCOORD1;
		//float4 screenCoordX : TEXCOORD2;
		//float4 screenCoordY : TEXCOORD3;
	};

	uniform float _SampleDistance;
	uniform float4 _MainTex_TexelSize;

	ps_input vert(appdata_img v)
	{
		ps_input o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		/*
		//o.uv = v.vertex.xyz * 0.5 + 0.5;
		o.screenCoord = ComputeScreenPos(o.pos);
		o.screenCoord.y = 1 - o.screenCoord.y;

		float4 newPos = o.pos + float4(_SampleDistance, 0, 0, 0);
		o.screenCoordX = ComputeScreenPos(newPos);
		o.screenCoordX.y = 1 - o.screenCoordX.y;
		newPos = o.pos + float4(0, _SampleDistance, 0, 0);
		o.screenCoordY = ComputeScreenPos(newPos);
		o.screenCoordY.y = 1 - o.screenCoordY.y;
		*/

		float2 uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
		o.uv[0] = uv;
		o.uv[1] = uv + float2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y) * _SampleDistance;
		o.uv[2] = uv + float2(+_MainTex_TexelSize.x, -_MainTex_TexelSize.y) * _SampleDistance;


		return o;
	}

	//sampler3D _VolTex;
	sampler2D _MainTex;
	uniform float _Threshold;
	uniform float _EdgesOnly;
	

	float4 frag(ps_input i) : COLOR
	{
		fixed4 original = tex2D(_MainTex, i.uv[0] );

		/*
		float4 xCol = tex2D(_MainTex, i.screenCoordX.xy);
		float4 yCol = tex2D(_MainTex, i.screenCoordY.xy);
		*/

		half3 p1 = original.rgb;
		half3 p2 = tex2D(_MainTex, i.uv[1]).rgb;
		half3 p3 = tex2D(_MainTex, i.uv[2]).rgb;


		float3 finalCol = float3(1, 1, 1);

		//if (dot(p1, p2) > _Threshold || dot(p1, p2) > _Threshold)
		if(
			(
				abs(p1.r - p2.r) > _Threshold ||
				abs(p1.g - p2.g) > _Threshold ||
				abs(p1.b - p2.b) > _Threshold
				)
			||
			(
				abs(p1.r - p3.r) > _Threshold ||
				abs(p1.g - p3.g) > _Threshold ||
				abs(p1.b - p3.b) > _Threshold
				)
			
			)
		{
			//currCol = float4(0, 0, 0, 1);
			//currCol = yCol;
			//currCol -= xCol/2;
			//currCol -= yCol;
			
			
			
			finalCol = float3(0, 0, 0);
			
		}
		else
		{
			finalCol = original.rgb;
			if(_EdgesOnly > 0)
				finalCol = float3(1, 1, 1);
		}

		return float4(finalCol, 1);
	}

		ENDCG
	}
	}
		FallBack "Diffuse"
}
