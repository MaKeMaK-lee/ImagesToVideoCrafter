using FFMediaToolkit.Encoding;
using System.Text.Json;

namespace CrafterCore.Options
{
    public class ImagesToVideoCrafterOptions
    {
        /// <summary>
        /// Path to FFMPEG binares (FMPEG shared .dll files: avcodec, avformat, avutil, swresample, swscale). When write this version is 6.x
        /// </summary>
        public required string FFmpegBinaresDirectory { get; set; }
        public required string OutputVideoName { get; set; }
        public required string OutputDirectory { get; set; }
        public required bool AddVideoInfoToFilename { get; set; }
        public required bool ReverseInputFilesOrder { get; set; }
        public required string InputDirectory { get; set; }

        public required double FrameMilliseconds { get; set; }
        public required int Framerate { get; set; }
        /// <summary>
        /// Set true to use Framerate or false to use FrameMilliseconds
        /// </summary>
        public required bool UseFramerate { get; set; }
        public required int Width { get; set; }
        public required int Height { get; set; }
        /// <summary>
        /// Constant Rate Factor.
        /// Balance recommended: 17
        /// </summary>
        public required int CRF { get; set; }
        /// <summary>
        /// 0 - ultra fast, 8 - very slow.
        /// Balance recommended: 4 - fast
        /// </summary>
        public required short EncoderPresetSpeed { get; set; }
        public required string Codec { get; set; }

        public required bool DebugMode { get; set; }

        /// <returns>
        /// Новый экземпляр со значениями по умолчанию
        /// </returns>...
        public static ImagesToVideoCrafterOptions Default
        {
            get
            {
                return new ImagesToVideoCrafterOptions()
                {
                    FFmpegBinaresDirectory = "ffmpeg\\x86_64",
                    OutputVideoName = "Video",
                    OutputDirectory = "VideoResults",
                    AddVideoInfoToFilename = false,
                    ReverseInputFilesOrder = false,
                    InputDirectory = "FramesSource",
                    DebugMode = false,

                    FrameMilliseconds = 1000 / 24,
                    Framerate = 24,
                    UseFramerate = false,
                    Width = 1920,
                    Height = 1080,
                    CRF = 17,
                    EncoderPresetSpeed = 4,
                    Codec = "H264",

                };
            }
        }

        public VideoEncoderSettings GetVideoEncoderSettings() =>
            new VideoEncoderSettings(
                width: this!.Width,
                height: this.Height,
                framerate: this.Framerate)
            {
                Codec = this.Codec switch
                {
                    "H264" => VideoCodec.H264,
                    "H265" => VideoCodec.H265,
                    "MPEG4" => VideoCodec.MPEG4,

                    _ => throw new Exception("Failed to create FFMPEG VideoEncoderSettings (wrong codec option)."),
                },
                EncoderPreset = this.EncoderPresetSpeed switch
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
                CRF = this.CRF,
            };

        public string GetJson()
        {
            return JsonSerializer.Serialize(this, options: new()
            {
                IgnoreReadOnlyProperties = true,
                WriteIndented = true
            });
        }
    }
}
