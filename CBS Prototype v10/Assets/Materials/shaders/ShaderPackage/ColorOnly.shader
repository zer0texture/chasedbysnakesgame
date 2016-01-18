Shader "Custom/ColorOnly" {
	Properties{
		//_VolTex("Texture", 3D) = "" {}
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
		SubShader{
		Pass
	{

		//Cull Back
		//ColorMask A
		Blend SrcAlpha OneMinusSrcAlpha
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
		
	};

	uniform float _SampleDistance;
	uniform float4 _MainTex_TexelSize;

	ps_input vert(appdata_img v)
	{
		ps_input o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		float2 uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
		o.uv[0] = uv;
		o.uv[1] = uv + float2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y) * _SampleDistance;
		o.uv[2] = uv + float2(+_MainTex_TexelSize.x, -_MainTex_TexelSize.y) * _SampleDistance;


		return o;
	}

	sampler2D _MainTex;
	uniform float _Threshold;
	uniform float _EdgesOnly;


	float4 frag(ps_input i) : COLOR
	{
		fixed4 original = tex2D(_MainTex, i.uv[0]);

	half3 p1 = original.rgb;
	half3 p2 = tex2D(_MainTex, i.uv[1]).rgb;
	half3 p3 = tex2D(_MainTex, i.uv[2]).rgb;


	float4 finalCol = float4(p1, 1);


	if (p1.r == 1 &&
		p1.g == 1 &&
		p1.b == 1)
	{
		finalCol = float4(1, 0, 1, 0);
	}

	return finalCol;
	}

		ENDCG
	}
	}
		FallBack "Diffuse"
}
