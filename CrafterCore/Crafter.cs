using FFMediaToolkit;
using FFMediaToolkit.Encoding;
using ImagesToVideoCrafter_Core.Extensions;
using ImagesToVideoCrafter_ImageProcessor.DarknessDetection;
using ImagesToVideoCrafter_Options;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImagesToVideoCrafter_Core
{
    public class Crafter(ImagesToVideoCrafterOptions? crafterOptions = null)
    {
        private TimeSpan CurrentCraftCurrentFrameTime;
        private readonly DarknessDetector DarknessDetector = new();

        public ImagesToVideoCrafterOptions CrafterOptions { get; private set; } = crafterOptions ?? ImagesToVideoCrafterOptions.Default;

        public void AddFrame(MediaOutput file, Bitmap bitmap)
        {
            if (CrafterOptions.UseFramerate)
            {
                file.Video.AddFrame(bitmap.ToImageData(out BitmapData? bitLock));
                bitmap.UnlockBits(bitLock!);
            }
            else
            {
                file.Video.AddFrame(bitmap.ToImageData(out BitmapData? bitLock), CurrentCraftCurrentFrameTime);
                bitmap.UnlockBits(bitLock!);

                CurrentCraftCurrentFrameTime += TimeSpan.FromMilliseconds(CrafterOptions.FrameMilliseconds);
            }
        }

        public string Craft(string? outputFileNameWithoutExtension = null, bool addDateTimeToFilename = true,
            Action<string>? printInfoAction = null,
            Action<string>? printWarningAction = null,
            Action<string>? printDebugAction = null)
        {
            printDebugAction?.Invoke("Craft started with opTions:\n" + CrafterOptions.GetJson());

            //Correct options
            outputFileNameWithoutExtension ??= CrafterOptions.OutputVideoName;
            if (!CrafterOptions.UseFramerate)
            {
                printInfoAction?.Invoke("При использовании времени на кадр FrameMilliseconds вместо частоты кадров Framerate (UseFramerate = true)" +
                    " из-за проблемы точности чисел с плавающей запятой может возникать ошибка. " +
                    "\nЭтого возможно избежать повывсив Framerate, но это может сказаться на результате. " +
                    "(Хотя вроде не должно, я прост не шарю в видео кодеках на 100%, а тестирование провожу крайне ограниченное).");
                if (CrafterOptions.Framerate < (1000d / CrafterOptions.FrameMilliseconds))
                {
                    CrafterOptions.Framerate = ((int)Math.Ceiling(1000d / CrafterOptions.FrameMilliseconds)) + 1;
                    printWarningAction?.Invoke("Заданная комбинация параметров UseFramerate, Framerate и FrameMilliseconds скорее всего ввызвала бы ошибку." +
                        " Параметр Framerate был скорректирован. \nFramerate = " + CrafterOptions.Framerate);
                }
            }

            //Edit filename
            if (addDateTimeToFilename)
            {
                var now = DateTime.Now;
                outputFileNameWithoutExtension +=
                    $" {now.Year}-{now.Month}-{now.Day} {now.Hour:00}-{now.Minute:00}-{now.Second:00}.";
            }
            if (CrafterOptions.AddVideoInfoToFilename)
            {
                outputFileNameWithoutExtension +=
                    " Options - " + CrafterOptions.Width + "x" + CrafterOptions.Height +
                    ", ~" + CrafterOptions.FrameMilliseconds + " frT" +
                    ", ~" + CrafterOptions.Framerate + " fps " + (CrafterOptions.UseFramerate ? "(used)" : "(unused)") +
                    ", " + CrafterOptions.Codec + " codec" +
                    ", " + CrafterOptions.EncoderPresetSpeed + " encoder speed (0 - speed, 8 - size)" +
                    ", " + CrafterOptions.CRF + " CRF (0 - quality, 51 - size)" +
                    ", " + CrafterOptions.LightnessThreshold + " lightd.";
            }
            string outputFileName = outputFileNameWithoutExtension + "mp4";

            //Start crafting
            Directory.CreateDirectory(CrafterOptions.OutputDirectory);
            Directory.CreateDirectory(CrafterOptions.InputDirectory);
            string FullFileName = Path.Combine(CrafterOptions.OutputDirectory, outputFileName);

            FFmpegLoader.FFmpegPath = CrafterOptions.FFmpegBinaresDirectory;

            var settings = CrafterOptions.GetVideoEncoderSettings();

            using var file = MediaBuilder
                .CreateContainer(Path.GetFullPath(FullFileName))
                .WithVideo(settings)
                .Create();

            var imageFiles = Directory.EnumerateFiles(CrafterOptions.InputDirectory);
            var imageFilesCount = imageFiles.Count();

            imageFiles = CrafterOptions.ReverseInputFilesOrder ? imageFiles.OrderDescending() : imageFiles.Order();

            int frameNumber = 1;
            CurrentCraftCurrentFrameTime = TimeSpan.Zero;
            foreach (var imageFile in imageFiles)
            {
                printDebugAction?.Invoke("Start processing frame of file " + imageFile);

                var bitmap = ((Bitmap)Bitmap.FromFile(imageFile));//TODO? uncatched crush with empty file

                var imageLightness = DarknessDetector.CalculateImageLightness(bitmap);
                printDebugAction?.Invoke("Frame brightness = " + imageLightness);

                bool isValideLightness = !(imageLightness < CrafterOptions.LightnessThreshold);

                if (!isValideLightness)
                {
                    printDebugAction?.Invoke("Frame brightness is defined as DARK");
                }
                else
                {
                    printDebugAction?.Invoke("Frame brightness is defined as LIGHT");
                    AddFrame(file, bitmap);
                }


                printInfoAction?.Invoke("Кадр " + frameNumber + " / " + imageFilesCount + (isValideLightness ? " добавлен" : " пропущен"));

                printDebugAction?.Invoke("End processing frame.");
                frameNumber++;
            }
            return FullFileName;
        }
    }
}
