using FFMediaToolkit.Encoding;
using ImagesToVideoCrafter_Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ImagesToVideoCrafter_Core.Extensions
{
    internal static class ImagesToVideoCrafterOptionsExtensions
    {
        public static VideoEncoderSettings GetVideoEncoderSettings(this ImagesToVideoCrafterOptions options) =>
            new VideoEncoderSettings(
                width: options!.Width,
                height: options.Height,
                framerate: options.Framerate)
            {
                Codec = options.Codec switch
                {
                    "H264" => VideoCodec.H264,
                    "H265" => VideoCodec.H265,
                    "MPEG4" => VideoCodec.MPEG4,

                    _ => throw new Exception("Failed to create FFMPEG VideoEncoderSettings (wrong codec option)."),
                },
                EncoderPreset = options.EncoderPresetSpeed switch
                {
                    0 => EncoderPreset.UltraFast,
                    1 => EncoderPreset.SuperFast,
                    2 => EncoderPreset.VeryFast,
                    3 => EncoderPreset.Faster,
                    4 => EncoderPreset.Fast,
                    5 => EncoderPreset.Medium,
                    6 => EncoderPreset.Slow,
                    7 => EncoderPreset.Slower,
                    8 => EncoderPreset.VerySlow,

                    _ => throw new Exception("Failed to create FFMPEG VideoEncoderSettings (wrong encoder preset option)."),
                },
                CRF = options.CRF,
            };
    }
}
