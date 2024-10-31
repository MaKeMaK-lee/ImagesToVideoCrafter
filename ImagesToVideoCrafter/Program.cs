using ImagesToVideoCrafter_Core;
using ImagesToVideoCrafter_Options;

#if DEBUG
using System.Security.Principal;
#endif

namespace ImagesToVideoCrafter
{
    public class Program
    {
        private static ImagesToVideoCrafterOptions SetOptions(ImagesToVideoCrafterOptions options, string[] args)
        {
            try
            {
                var argsEnumerator = args.GetEnumerator();
                while (argsEnumerator.MoveNext())
                {
                    switch (argsEnumerator.Current)
                    {
                        case "-FFmpegBinaresDirectory":
                            argsEnumerator.MoveNext();
                            options.FFmpegBinaresDirectory = (string)argsEnumerator.Current;
                            break;
                        case "-OutputDirectory":
                            argsEnumerator.MoveNext();
                            options.OutputDirectory = (string)argsEnumerator.Current;
                            break;
                        case "-OutputVideoName":
                            argsEnumerator.MoveNext();
                            options.OutputVideoName = (string)argsEnumerator.Current;
                            break;
                        case "-ReverseInputFilesOrder":
                            argsEnumerator.MoveNext();
                            options.ReverseInputFilesOrder = bool.Parse((string)argsEnumerator.Current);
                            break;
                        case "-AddVideoInfoToFilename":
                            argsEnumerator.MoveNext();
                            options.AddVideoInfoToFilename = bool.Parse((string)argsEnumerator.Current);
                            break;
                        case "-InputDirectory":
                            argsEnumerator.MoveNext();
                            options.InputDirectory = (string)argsEnumerator.Current;
                            break;
                        case "-FrameMilliseconds":
                            argsEnumerator.MoveNext();
                            options.FrameMilliseconds = double.Parse(((string)argsEnumerator.Current).Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                            break;
                        case "-Framerate":
                            argsEnumerator.MoveNext();
                            options.Framerate = int.Parse((string)argsEnumerator.Current);
                            break;
                        case "-UseFramerate":
                            argsEnumerator.MoveNext();
                            options.UseFramerate = bool.Parse((string)argsEnumerator.Current);
                            break;
                        case "-CRF":
                            argsEnumerator.MoveNext();
                            options.CRF = int.Parse((string)argsEnumerator.Current);
                            break;
                        case "-Width":
                            argsEnumerator.MoveNext();
                            options.Width = int.Parse((string)argsEnumerator.Current);
                            break;
                        case "-Height":
                            argsEnumerator.MoveNext();
                            options.Height = int.Parse((string)argsEnumerator.Current);
                            break;
                        case "-EncoderPresetSpeed":
                            argsEnumerator.MoveNext();
                            options.EncoderPresetSpeed = short.Parse((string)argsEnumerator.Current);
                            break;
                        case "-Codec":
                            argsEnumerator.MoveNext();
                            options.Codec = (string)argsEnumerator.Current;
                            break;
                        case "-DebugMode":
                            argsEnumerator.MoveNext();
                            options.DebugMode = bool.Parse((string)argsEnumerator.Current);
                            break;
                        default:

                            break;
                    }
                }
            }
            catch (Exception inner)
            {
                throw new Exception("Ошибка при чтении входных параметров", inner);
            }
            return options;
        }

        public static void Main(string[] args)
        {
#if DEBUG
            string username = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            args = [
                "-FFmpegBinaresDirectory","C:\\Users\\"+username+"\\Desktop\\ffmpeg\\x86_64" ,
                "-OutputVideoName","test video" ,
                "-OutputDirectory","C:\\Users\\"+username+"\\Desktop\\resultVideos" ,
                "-ReverseInputFilesOrder","false" ,
                "-InputDirectory","C:\\Users\\"+username+"\\Desktop\\testImages" ,
                "-FrameMilliseconds","41.666666666666666666666666666667" ,
                "-Framerate","24" ,
                "-UseFramerate false" ,
                "-CRF","17" ,
                "-Width","1920" ,
                "-Height","1080" ,
                "-EncoderPresetSpeed","4" ,
                "-Codec","H264" ,
                "-AddVideoInfoToFilename","false" ,
                "-DebugMode","true" ,
                ];
#endif
            var crafterOptions = SetOptions(ImagesToVideoCrafterOptions.Default, args);

            DateTime startTime = DateTime.Now;

            var crafter = new Crafter(crafterOptions);
            string FullFileName = crafter.Craft(
                printInfoAction: (s) => Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] [INFO] " + s),
                printWarningAction: (s) => Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] [WARN] " + s),
                printDebugAction: (s) => { if (crafterOptions.DebugMode) Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] [DEBUG] " + s); }
                );

            var time = (DateTime.Now - startTime);
            Console.WriteLine("\n\n\nDone " +
                (time.Hours == 0 ? "" : (time.Hours + "h ")) +
                (time.Minutes == 0 ? "" : (time.Minutes + "m ")) +
                time.Seconds + "." + time.Milliseconds + "s" +
                "\n" +
                "Video saved: " + FullFileName + "\n");
            Console.ReadKey(true);
        }
    }
}
