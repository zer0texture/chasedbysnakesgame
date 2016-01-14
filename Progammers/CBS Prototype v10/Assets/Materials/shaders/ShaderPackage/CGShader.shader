Shader "Custom/CGShader" {
Properties {
	  _MainTex("Main Tex", 2D) = "white" {}
      _BumpMap ("Normal Map", 2D) = "bump" {}
      _Color ("Diffuse Material Color", Color) = (1,1,1,1) 
      _SpecColor ("Specular Material Color", Color) = (1,1,1,1) 
      _Shininess ("Shininess", Float) = 10
      _Intensity ("Intensity", Float) = 1
	  _OutlineColor("Outline Color", Color) = (0,0,0,1)
   }
   SubShader {  
	 Pass {      
         Tags { "LightMode" = "ForwardBase" } 
            // pass for ambient light and first light source
 
		 Name "WHITE BASE"
		 //Blend OneMinusSrcColor OneMinusSrcColor

         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag  
 
         #include "UnityCG.cginc"
		 
		 struct vertexInput 
		 {
            float4 vertex : POSITION;
		};
		struct vertexOutput 
		{
            float4 vertex : SV_POSITION;
		};
		 vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
			//output.vertex = input.vertex;
			output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
			return output;
		 }
		 
		 float4 frag(vertexOutput input) : COLOR
         {
			return float4(1.0, 1.0, 1.0, 1.0);
		 }
		 ENDCG
		 }
		  
      
      Pass {      
         Tags { "LightMode" = "ForwardAdd" } 
		 Name "INVERSE"
            // pass for additional light sources
         //Blend One One // additive blending 
		 Blend OneMinusSrcColor OneMinusSrcColor

		  //Blend SrcColor One
 
		

         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag  
 
         #include "UnityCG.cginc"
         uniform float4 _LightColor0; 
            // color of light source (from "Lighting.cginc")
 
         // User-specified properties
		 uniform sampler2D _MainTex;	
         uniform float4 _MainTex_ST;
         uniform sampler2D _BumpMap;	
         uniform float4 _BumpMap_ST;
         uniform float4 _Color; 
         uniform float4 _SpecColor; 
         uniform float _Shininess;
         uniform float _Intensity;

		 uniform float4x4 _LightMatrix0; // transformation 
										 // from world to light space (from Autolight.cginc)
		 uniform sampler2D _LightTexture0;
		 // cookie alpha texture map (from Autolight.cginc)
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
            float3 normal : NORMAL;
            float4 tangent : TANGENT;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posWorld : TEXCOORD0;
               // position of the vertex (and fragment) in world space 
			float4 posLight : TEXCOORD1;
				// position of the vertex (and fragment) in light space
            float4 tex : TEXCOORD2;
            float3 tangentWorld : TEXCOORD3;  
            float3 normalWorld : TEXCOORD4;
            float3 binormalWorld : TEXCOORD5;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            float4x4 modelMatrix = _Object2World;
            float4x4 modelMatrixInverse = _World2Object; 
 
            output.tangentWorld = normalize(
               mul(modelMatrix, float4(input.tangent.xyz, 0.0)).xyz);
            output.normalWorld = normalize(
               mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
            output.binormalWorld = normalize(
               cross(output.normalWorld, output.tangentWorld) 
               * input.tangent.w); // tangent.w is specific to Unity
 
            output.posWorld = mul(modelMatrix, input.vertex);
            output.tex = input.texcoord;
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
			output.posLight = mul(_LightMatrix0, output.posWorld);
            return output;
         }
          
         float4 frag(vertexOutput input) : COLOR
         {
            // in principle we have to normalize tangentWorld,
            // binormalWorld, and normalWorld again; however, the  
            // potential problems are small since we use this 
            // matrix only to compute "normalDirection", 
            // which we normalize anyways
 
            float4 encodedNormal = tex2D(_BumpMap, 
               _BumpMap_ST.xy * input.tex.xy + _BumpMap_ST.zw);
            float3 localCoords = float3(2.0 * encodedNormal.a - 1.0, 
                2.0 * encodedNormal.g - 1.0, 0.0);
            localCoords.z = sqrt(1.0 - dot(localCoords, localCoords));
               // approximation without sqrt:  localCoords.z = 
               // 1.0 - 0.5 * dot(localCoords, localCoords);
 
            float3x3 local2WorldTranspose = float3x3(
               input.tangentWorld,
               input.binormalWorld, 
               input.normalWorld);
            float3 normalDirection = 
               normalize(mul(localCoords, local2WorldTranspose));
 
            float3 viewDirection = normalize(
               _WorldSpaceCameraPos - input.posWorld.xyz);
            float3 lightDirection;
            float attenuation;
 
            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1.0; // no attenuation
               lightDirection = normalize(_WorldSpaceLightPos0.xyz);
            } 
            else // point or spot light
            {
               float3 vertexToLightSource = 
                  _WorldSpaceLightPos0.xyz - input.posWorld.xyz;
               float distance = length(vertexToLightSource);
               attenuation = 1.0 / distance; // linear attenuation 
               lightDirection = normalize(vertexToLightSource);
            }
 
            float3 diffuseReflection = 
               attenuation * _LightColor0.rgb * (1 - _Color.rgb)
               * max(0.0, dot(normalDirection, lightDirection));
               
            diffuseReflection *= _Intensity;
 
            float3 specularReflection;
            if (dot(normalDirection, lightDirection) < 0.0) 
               // light source on the wrong side?
            {
               specularReflection = float3(0.0, 0.0, 0.0); 
                  // no specular reflection
            }
            else // light source on the right side
            {
               specularReflection = attenuation * _LightColor0.rgb 
                  * ( 1 - _SpecColor.rgb) * pow(max(0.0, dot(
                  reflect(-lightDirection, normalDirection), 
                  viewDirection)), _Shininess);
            }

			// compute diffuseReflection and specularReflection

			float cookieAttenuation = 1.0;
			if (0.0 == _WorldSpaceLightPos0.w) // directional light?
			{
				cookieAttenuation =
					tex2D(_LightTexture0, input.posWorld.xy).a;
			}
			else if (1.0 != _LightMatrix0[3][3])
				// spotlight (i.e. not a point light)?
			{
				cookieAttenuation = tex2D(_LightTexture0,
					input.posLight.xy / input.posLight.w
					+ float2(0.5, 0.5)).a;
			}


			float4 tex = tex2D(_MainTex, 
               _MainTex_ST.xy * input.tex.xy + _MainTex_ST.zw);
			   tex  = 1 - tex ;

			   if (tex.a == 0)
				   tex = float4(0.5, 0.5, 0.5, 1);
			
            return float4(tex * diffuseReflection * cookieAttenuation + specularReflection, 1.0);
         }
         ENDCG
      }

	  UsePass "Custom/RandomColor/COLOR"
   }
}