﻿
//----------------------------------------------------------------------------------------------------------
// X-PostProcessing Library
// https://github.com/QianMo/X-PostProcessing-Library
// Copyright (C) 2020 QianMo. All rights reserved.
// Licensed under the MIT License 
// You may not use this file except in compliance with the License.You may obtain a copy of the License at
// http://opensource.org/licenses/MIT
//----------------------------------------------------------------------------------------------------------

Shader "Hidden/X-PostProcessing/RadialBlurV1"
{
	
	HLSLINCLUDE

	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
	#include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

	TEXTURE2D_X(_MainTex);
	SAMPLER(sampler_MainTex);

	uniform half4 _Params;

	#define _BlurRadius _Params.x
	#define _Iteration _Params.y
	#define _RadialCenter _Params.zw

	half4 RadialBlur(Varyings i)
	{
		float2 blurVector = (_RadialCenter - i.texcoord.xy) * _BlurRadius;

		half4 acumulateColor = half4(0, 0, 0, 0);

		[unroll(30)]
		for (int j = 0; j < _Iteration; j++)
		{
			acumulateColor += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
			i.texcoord.xy += blurVector;
		}

		return acumulateColor / _Iteration;
	}

	half4 Frag(Varyings i) : SV_Target
	{
		return RadialBlur(i);
	}
	
	ENDHLSL
	
	SubShader
	{
		Cull Off ZWrite Off ZTest Always
		
		Pass
		{
			HLSLPROGRAM
			
			#pragma vertex Vert
			#pragma fragment Frag
			
			ENDHLSL
			
		}
	}
}

