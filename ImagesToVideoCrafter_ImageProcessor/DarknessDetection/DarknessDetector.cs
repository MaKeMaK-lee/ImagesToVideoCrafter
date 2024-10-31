using System.Drawing;
using System.Drawing.Imaging;

namespace ImagesToVideoCrafter_ImageProcessor.DarknessDetection
{
    public class DarknessDetector
    {
        public double CalculateImageLightness(Bitmap bitmap)
        {
            double lum = 0;
            var tmpBitmap = new Bitmap(bitmap);
            var width = tmpBitmap.Width;
            var height = tmpBitmap.Height;
            var bppModifier = tmpBitmap.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;

            var srcData = tmpBitmap.LockBits(new Rectangle(0, 0, tmpBitmap.Width, tmpBitmap.Height), ImageLockMode.ReadOnly, tmpBitmap.PixelFormat);
            var stride = srcData.Stride;
            var scan0 = srcData.Scan0;

            //Luminance (standard, objective): (0.2126*R) + (0.7152*G) + (0.0722*B)
            //Luminance (perceived option 1): (0.299*R + 0.587*G + 0.114*B)
            //Luminance (perceived option 2, slower to calculate): sqrt( 0.299*R^2 + 0.587*G^2 + 0.114*B^2 )

            unsafe
            {
                byte* p = (byte*)(void*)scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int idx = (y * stride) + x * bppModifier;
                        lum += (0.299 * p[idx + 2] + 0.587 * p[idx + 1] + 0.114 * p[idx]);
                    }
                }
            }

            tmpBitmap.UnlockBits(srcData);
            tmpBitmap.Dispose();
            var avgLum = lum / (width * height);

            return avgLum / 255.0;
        }

    }
}
