using System.Text.Json;

namespace ImagesToVideoCrafter_Options
{
    public class ImagesToVideoCrafterOptions
    {
        /// <summary>
        /// Path to FFMPEG binares
        /// </summary>
        public required string FFmpegBinaresDirectory { get; set; }
        public required string OutputVideoName { get; set; }
        public required string OutputDirectory { get; set; }
        public required bool AddVideoInfoToFilename { get; set; }
        public required bool ReverseInputFilesOrder { get; set; }
        public required string InputDirectory { get; set; }
        public required bool DebugMode { get; set; }

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

        public required double LightnessThreshold { get; set; }


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

                    LightnessThreshold = 0.2,

                };
            }
        }

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
