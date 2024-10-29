namespace ImagesToVideoCrafter.Options
{
    public class ImagesToVideoCrafterOptions
    {
        /// <summary>
        /// Path to FFMPEG binares (FMPEG shared .dll files: avcodec, avformat, avutil, swresample, swscale). When write this version is 6.x
        /// </summary>
        public required string FFmpegBinaresDirectory { get; set; }
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

        /// <returns>
        /// Новый экземпляр со значениями по умолчанию
        /// </returns>...
        public static ImagesToVideoCrafterOptions Default
        {
            get
            {
                return new ImagesToVideoCrafterOptions()
                {
                    FFmpegBinaresDirectory = "C:\\ffmpeg\\x86_64",
                    OutputDirectory = "C:\\ImagesToVideoCrafter\\Out",
                    AddVideoInfoToFilename = false,
                    ReverseInputFilesOrder = false,
                    InputDirectory = "C:\\ImagesToVideoCrafter\\In",

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
    }
}
