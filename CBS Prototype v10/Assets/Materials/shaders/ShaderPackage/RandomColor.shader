Shader "Custom/RandomColor" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

			Pass{
			Tags{ "LightMode" = "ForwardBase" }
			// pass for ambient light and first light source

			Name "COLOR"
			//Blend SrcColor Zero

			Blend OneMinusSrcColor One
			CGPROGRAM

			

#pragma vertex vert  
#pragma fragment frag  

#include "UnityCG.cginc"

		struct vertexInput
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};
		struct vertexOutput
		{
			float4 vertex : SV_POSITION;
			float4 vertexLocal : TEXCOORD0;
			float3 normalWorld : TEXCOORD1;
			float4 extraColor : TEXCOORD2;
		};

		float rand(float3 co)
		{
			return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
		}


		uniform float4 _Color;

		vertexOutput vert(vertexInput input)
		{
			float4x4 modelMatrixInverse = _World2Object;

			vertexOutput output;
			//output.vertex = input.vertex;
			output.vertexLocal = input.vertex;
			output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
			output.normalWorld = normalize(
				mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);


			float3 baseWorldPos = mul(_Object2World, float4(0, 0, 0, 1)).xyz;

			float iExtraColorID = rand(baseWorldPos);
			_Color = float4(iExtraColorID, iExtraColorID, iExtraColorID, 1);

			if (
				(
					_Color.r == 1 &&
					_Color.g == 1 &&
					_Color.b == 1 &&
					_Color.a == 1
					)
				)
			{
				/*
				float iExtraColorID = rand(input.vertex);
				float4 extraRandomColor = float4(0.5, 0.5, 0.5, 1);

				if (iExtraColorID <= 0)
					extraRandomColor = float4(1, 0, 0, 1);
				if (iExtraColorID > 0 && iExtraColorID < 0.2)
					extraRandomColor = float4(1, 1, 0, 1);
				if (iExtraColorID > 0.2 && iExtraColorID < 0.4)
					extraRandomColor = float4(1, 0, 1, 1);
				if (iExtraColorID > 0.4 && iExtraColorID < 0.6)
					extraRandomColor = float4(0, 1, 0, 1);
				if (iExtraColorID > 0.6 && iExtraColorID < 0.8)
					extraRandomColor = float4(0, 0, 1, 1);
				if (iExtraColorID > 0.8)
					extraRandomColor = float4(0, 1, 1, 1);

				_Color = extraRandomColor;
				*/
			}


			//output.extraColor = _Color;


			output.extraColor = float4(0, 0, 0, 1);


			float4 diff = (1 - mul(UNITY_MATRIX_MVP, _WorldSpaceCameraPos));

			float4 temp = diff;
			//diff.r = temp.g;
			//diff.g = temp.b;
			//diff.b = temp.r;

			//output.extraColor.rgb += (diff - baseWorldPos) / 100;
			//output.extraColor.rgb += (diff - input.vertex) / 100;

			//output.extraColor.rgb += length(diff - baseWorldPos) / 100;
			output.extraColor.rgb += length(diff - input.vertex) / 500;

			return output;
		}


		float4 frag(vertexOutput input) : COLOR
		{
			float4 poschange = input.vertex / 1000;
			
			


			float4 color = float4(input.normalWorld + input.extraColor + float4(0.5, 0.5, 0.5, 1), 1);

			float3 tempcol = color - poschange;
			bool tempcolPlus = true;
			bool tempcolMinus = false;
			int poschangeItterations = 0;

			//while (poschangeItterations < 8)
			/*
			{
				if ((tempcol.r < 0 || tempcol.g < 0 || tempcol.b < 0) && tempcolPlus)
				{
					tempcol += poschange;
					tempcolPlus = false;
					tempcolMinus = true;
					poschangeItterations++;
				}
				else if ((tempcol.r > 1 || tempcol.g > 1 || tempcol.b > 1) && tempcolMinus)
				{
					tempcol -= poschange;
					poschangeItterations++;
					tempcolMinus = false;
					tempcolPlus = true;
				}
			}
			*/

			//color = float4(tempcol, 1);

			return color;
		}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
