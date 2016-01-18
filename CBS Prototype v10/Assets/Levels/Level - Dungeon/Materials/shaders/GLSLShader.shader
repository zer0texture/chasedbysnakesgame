Shader "GLSLShader" {
	Properties{
		_MainTex("Main Tex", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
		_Color("Diffuse Material Color", Color) = (1,1,1,1)
		_SpecColor("Specular Material Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Float) = 10
		_Intensity ("Intensity", Float) = 1
	}
		SubShader{
		Pass{
		Tags{ "LightMode" = "ForwardBase" }
		// pass for ambient light and first light source

		GLSLPROGRAM

		// User-specified properties
		uniform sampler2D _MainTex;
	uniform vec4 _MainTex_ST;
	uniform sampler2D _BumpMap;
	uniform vec4 _BumpMap_ST;
	uniform vec4 _Color;
	uniform vec4 _SpecColor;
	uniform float _Shininess;
	uniform float _Intensity;

	// The following built-in uniforms (except _LightColor0) 
	// are also defined in "UnityCG.glslinc", 
	// i.e. one could #include "UnityCG.glslinc" 
	uniform vec3 _WorldSpaceCameraPos;
	// camera position in world space
	uniform mat4 _Object2World; // model matrix
	uniform mat4 _World2Object; // inverse model matrix
	uniform vec4 _WorldSpaceLightPos0;
	// direction to or position of light source
	uniform vec4 _LightColor0;
	// color of light source (from "Lighting.cginc")



#ifdef VERTEX

	attribute vec4 Tangent;

	void main()
	{
		gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	}

#endif

#ifdef FRAGMENT


	void main()
	{
		gl_FragColor = vec4(1, 1, 1, 1);
		//gl_FragColor = vec4(0,0,0,0);
	}

#endif

	ENDGLSL
	}

		Pass{
		Tags{ "LightMode" = "ForwardAdd" }
		// pass for light sources
		//Blend One One
		Blend OneMinusSrcColor OneMinusSrcColor
		

		GLSLPROGRAM

		// User-specified properties
		uniform sampler2D _MainTex;
	uniform vec4 _MainTex_ST;
	uniform sampler2D _BumpMap;
	uniform vec4 _BumpMap_ST;
	uniform vec4 _Color;
	uniform vec4 _SpecColor;
	uniform float _Shininess;
	uniform float _Intensity;

	// The following built-in uniforms (except _LightColor0) 
	// are also defined in "UnityCG.glslinc", 
	// i.e. one could #include "UnityCG.glslinc" 
	uniform vec3 _WorldSpaceCameraPos;
	// camera position in world space
	uniform mat4 _Object2World; // model matrix
	uniform mat4 _World2Object; // inverse model matrix
	uniform vec4 _WorldSpaceLightPos0;
	// direction to or position of light source
	uniform vec4 _LightColor0;
	// color of light source (from "Lighting.cginc")

	uniform mat4 _LightMatrix0; // transformation 
								// from world to light space (from Autolight.cginc)
	uniform sampler2D _LightTexture0;
	// cookie alpha texture map (from Autolight.cginc)

#ifdef VERTEX

	attribute vec4 Tangent;

	out vec4 position;
	// position of the vertex (and fragment) in world space 
	out vec4 textureCoordinates;
	out mat3 localSurface2World; // mapping from 
								 // local surface coordinates to world coordinates

	out vec3 varyingNormalDirection;
	out vec4 positionInLightSpace;

	void main()
	{
		mat4 modelMatrix = _Object2World;
		mat4 modelMatrixInverse = _World2Object; // unity_Scale.w 
												 // is unnecessary because we normalize vectors

		localSurface2World[0] = normalize(vec3(
			modelMatrix * vec4(vec3(Tangent), 0.0)));
		localSurface2World[2] = normalize(vec3(
			vec4(gl_Normal, 0.0) * modelMatrixInverse));
		localSurface2World[1] = normalize(
			cross(localSurface2World[2], localSurface2World[0])
			* Tangent.w); // factor Tangent.w is specific to Unity

		varyingNormalDirection = normalize(vec3(
			vec4(gl_Normal, 0.0) * modelMatrixInverse));

		position = modelMatrix * gl_Vertex;
		textureCoordinates = gl_MultiTexCoord0;
		positionInLightSpace = _LightMatrix0 * position;
		gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	}

#endif

#ifdef FRAGMENT

	in vec4 position;
	// position of the vertex (and fragment) in world space 
	in vec4 textureCoordinates;
	in mat3 localSurface2World; // mapping from 
								// local surface coordinates to world coordinates

	in vec3 varyingNormalDirection;
	in vec4 positionInLightSpace;

	void main()
	{
		// in principle we have to normalize the columns of 
		// "localSurface2World" again; however, the potential 
		// problems are small since we use this matrix only to
		// compute "normalDirection", which we normalize anyways
		vec4 encodedNormal = texture2D(_BumpMap,
			_BumpMap_ST.xy * textureCoordinates.xy
			+ _BumpMap_ST.zw);


		vec3 localCoords = 2.0 * encodedNormal.rgb - vec3(1.0);

		// approximation without sqrt: localCoords.z = 
		// 1.0 - 0.5 * dot(localCoords, localCoords);
		vec3 normalDirection =
			normalize(localSurface2World * localCoords);

		vec3 viewDirection =
			normalize(_WorldSpaceCameraPos - vec3(position));
		vec3 lightDirection;
		float attenuation;
		float cookieAttenuation = 1.0;
		if (0.0 == _WorldSpaceLightPos0.w) // directional light?
		{
			attenuation = 1.0; // no attenuation
			lightDirection = normalize(vec3(_WorldSpaceLightPos0));
		}
		else // point or spot light
		{

			vec3 vertexToLightSource =
				vec3(_WorldSpaceLightPos0 - position);
			float distance = length(vertexToLightSource);
			attenuation = 1.0 / distance; // linear attenuation 
			lightDirection = normalize(vertexToLightSource);
		}

		if (0.0 == _WorldSpaceLightPos0.w) // directional light?
		{
			cookieAttenuation = texture2D(_LightTexture0, vec2(positionInLightSpace)).a;
		}
		else if (1.0 != _LightMatrix0[3][3])
		{
			cookieAttenuation = texture2D(_LightTexture0,
				vec2(positionInLightSpace) / positionInLightSpace.w
				+ vec2(0.5)).a;
		}

		vec3 diffuseReflection =
			attenuation * vec3(_LightColor0 * 2.0) * vec3(vec4(1,1,1,1) - _Color)
			* max(0.0, dot(normalDirection, lightDirection));
		diffuseReflection *= _Intensity;

		vec3 specularReflection;
		if (dot(normalDirection, lightDirection) < 0.0)
			// light source on the wrong side?
		{
			specularReflection = vec3(0.0, 0.0, 0.0);
			// no specular reflection
		}
		else // light source on the right side
		{
			specularReflection = attenuation * vec3(_LightColor0)
				* vec3(vec4(1, 1, 1, 1) - _SpecColor) * pow(max(0.0, dot(
					reflect(-lightDirection, normalDirection),
					viewDirection)), _Shininess);
		}


		gl_FragColor = texture2D(_MainTex,
			_MainTex_ST.xy * textureCoordinates.xy
			+ _MainTex_ST.zw);

		gl_FragColor.rgb = vec3(1, 1, 1) - gl_FragColor.rgb;

		gl_FragColor.rgb *= diffuseReflection;
		gl_FragColor.rgb += specularReflection;
		gl_FragColor.rgb *= cookieAttenuation;
	}

#endif

	ENDGLSL
	}
	}
		// The definition of a fallback shader should be commented out 
		// during development:
		// Fallback "mobile/diffuse "

		//Fallback "CGShader"
		Fallback "CGShader"
}