using CrafterCore.Options;
using FFMediaToolkit.Encoding;
using FFMediaToolkit.Graphics;
using System.Drawing;
using System.Drawing.Imaging;

namespace CrafterCore
{
    public static class Utils
    {
        

        public static ImageData ToImageData(this Bitmap bitmap, out BitmapData? bitLock)
        {
            var rect = new Rectangle(Point.Empty, bitmap.Size);
            bitLock = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var bitmapData = ImageData.FromPointer(bitLock.Scan0, ImagePixelFormat.Bgr24, bitmap.Size);
            return bitmapData;
        }
    }
}
