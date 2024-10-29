using FFMediaToolkit;
using FFMediaToolkit.Encoding;
using ImagesToVideoCrafter.Options;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImagesToVideoCrafter
{
    public class Crafter
    {
        public ImagesToVideoCrafterOptions CrafterOptions { get; private set; }

        public Crafter(ImagesToVideoCrafterOptions? crafterOptions = null)
        {
            CrafterOptions = crafterOptions ?? ImagesToVideoCrafterOptions.Default;
        }

        public string Craft(string outputFileNameWithoutExtension = "Video", bool addDateTimeToFilename = true, Action<string>? printInfoAction = null)
        {
            //Correct options
            if (!CrafterOptions.UseFramerate)
            {
                printInfoAction?.Invoke("[INFO] При использовании времени на кадр FrameMilliseconds вместо частоты кадров Framerate (UseFramerate = true)" +
                    " из-за проблемы точности чисел с плавающей запятой может возникать ошибка. " +
                    "\nЭтого возможно избежать повывсив Framerate, но это может сказаться на результате. " +
                    "(Хотя вроде не должно, я прост не шарю в видео кодеках на 100%, а тестирование провожу крайне ограниченное).");
                if (CrafterOptions.Framerate < (1000d / CrafterOptions.FrameMilliseconds))
                {
                    CrafterOptions.Framerate = ((int)Math.Ceiling(1000d / CrafterOptions.FrameMilliseconds)) + 1;
                    printInfoAction?.Invoke("[WARN] Заданная комбинация параметров UseFramerate, Framerate и FrameMilliseconds скорее всего ввызвала бы ошибку." +
                        " Параметр Framerate был скорректирован. \nFramerate = " + CrafterOptions.Framerate);
                }
            }

            //Edit filename
            if (addDateTimeToFilename)
            {
                var now = DateTime.Now;
                outputFileNameWithoutExtension +=
                    $" {now.Year}-{now.Month}-{now.Day} {now.Hour}-{now.Minute}-{now.Second}.";
            }
            if (CrafterOptions.AddVideoInfoToFilename)
            {
                outputFileNameWithoutExtension +=
                    " Options - " + CrafterOptions.Width + "x" + CrafterOptions.Height +
                    ", ~" + CrafterOptions.Framerate + " fps" +
                    ", " + CrafterOptions.Codec + " codec" +
                    ", " + CrafterOptions.EncoderPresetSpeed + " encoder speed (0 - speed, 8 - quality)" +
                    ", " + CrafterOptions.EncoderPresetSpeed + " CRF (constant rate factor supports only H.264 and H.265 codecs).";
            }
            string outputFileName = outputFileNameWithoutExtension + ".mp4";

            //Start crafting
            Directory.CreateDirectory(CrafterOptions.OutputDirectory);
            string FullFileName = Path.Combine(CrafterOptions.OutputDirectory, outputFileName);

            FFmpegLoader.FFmpegPath = CrafterOptions.FFmpegBinaresDirectory;

            var settings = CrafterOptions.GetVideoEncoderSettings();

            using var file = MediaBuilder
                .CreateContainer(FullFileName)
                .WithVideo(settings)
                .Create();

            var imageFiles = Directory.EnumerateFiles(CrafterOptions.InputDirectory);
            imageFiles = CrafterOptions.ReverseInputFilesOrder ? imageFiles.OrderDescending() : imageFiles.Order();

            int framesAdded = 0;
            TimeSpan currentFrameTime = TimeSpan.Zero;
            foreach (var imageFile in imageFiles)
            {
                if (CrafterOptions.UseFramerate)
                {
                    var bitmap = ((Bitmap)Bitmap.FromFile(imageFile));
                    file.Video.AddFrame(bitmap.ToImageData(out BitmapData? bitLock));
                    bitmap.UnlockBits(bitLock!);
                }
                else
                {
                    var bitmap = ((Bitmap)Bitmap.FromFile(imageFile));
                    file.Video.AddFrame(bitmap.ToImageData(out BitmapData? bitLock), currentFrameTime);
                    bitmap.UnlockBits(bitLock!);

                    currentFrameTime += TimeSpan.FromMilliseconds(CrafterOptions.FrameMilliseconds);
                }

                framesAdded++;
                printInfoAction?.Invoke("Added frame " + framesAdded);
            }
            return FullFileName;
        }
    }
}
