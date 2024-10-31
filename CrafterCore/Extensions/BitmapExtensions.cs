using FFMediaToolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesToVideoCrafter_Core.Extensions
{
    internal static class BitmapExtensions
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
