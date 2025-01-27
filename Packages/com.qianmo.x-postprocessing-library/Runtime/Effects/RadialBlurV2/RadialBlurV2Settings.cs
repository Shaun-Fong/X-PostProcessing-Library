﻿
//----------------------------------------------------------------------------------------------------------
// X-PostProcessing Library
// https://github.com/QianMo/X-PostProcessing-Library
// Copyright (C) 2020 QianMo. All rights reserved.
// Licensed under the MIT License 
// You may not use this file except in compliance with the License.You may obtain a copy of the License at
// http://opensource.org/licenses/MIT
//----------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


namespace XPL.Runtime
{

    public enum RadialBlurQuality
    {
        RadialBlur_4Tap_Fatest = 0,
        RadialBlur_6Tap = 1,
        RadialBlur_8Tap_Balance = 2,
        RadialBlur_10Tap = 3,
        RadialBlur_12Tap = 4,
        RadialBlur_20Tap_Quality = 5,
        RadialBlur_30Tap_Extreme = 6,
    }

    [Serializable]
    public sealed class RadialBlurQualityParameter : VolumeParameter<RadialBlurQuality> { }

    [Serializable]
    [VolumeComponentMenuForRenderPipeline("X-PostProcessing/Blur/RadialBlur/RadialBlurV2", typeof(UniversalRenderPipeline))]
    public class RadialBlurV2Settings : VolumeComponent, IPostProcessComponent
    {
        public RadialBlurQualityParameter QualityLevel = new RadialBlurQualityParameter { value = RadialBlurQuality.RadialBlur_8Tap_Balance };

        public ClampedFloatParameter Intensity = new ClampedFloatParameter(0f, 0.0f, 1.0f);

        public ClampedFloatParameter BlurRadius = new ClampedFloatParameter(0.6f, -1f, 1f);

        public ClampedFloatParameter RadialCenterX = new ClampedFloatParameter(0.5f, 0f, 1f);

        public ClampedFloatParameter RadialCenterY = new ClampedFloatParameter(0.5f, 0f, 1f);

        public bool IsActive() => Intensity.value > 0;

        public bool IsTileCompatible() => true;

    }
}

