using FFMediaToolkit.Encoding;
using FFMediaToolkit.Graphics;
using ImagesToVideoCrafter.Options;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImagesToVideoCrafter
{
    public static class Utils
    {
        public static VideoEncoderSettings GetVideoEncoderSettings(this ImagesToVideoCrafterOptions crafterOptions) => //TODO this is not a good place for it i think
            new VideoEncoderSettings(
                width: crafterOptions!.Width,
                height: crafterOptions.Height,
                framerate: crafterOptions.Framerate)
            {
                Codec = crafterOptions.Codec switch
                {
                    "H264" => VideoCodec.H264,
                    "H265" => VideoCodec.H265,
                    "MPEG4" => VideoCodec.MPEG4,

                    _ => throw new Exception("Failed to create FFMPEG VideoEncoderSettings (wrong codec option)."),
                },
                EncoderPreset = crafterOptions.EncoderPresetSpeed switch
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
                CRF = crafterOptions.CRF,
            };

        public static ImageData ToImageData(this Bitmap bitmap, out BitmapData? bitLock)
        {
            var rect = new Rectangle(Point.Empty, bitmap.Size);
            bitLock = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var bitmapData = ImageData.FromPointer(bitLock.Scan0, ImagePixelFormat.Bgr24, bitmap.Size);
            return bitmapData;
        }
    }
}
