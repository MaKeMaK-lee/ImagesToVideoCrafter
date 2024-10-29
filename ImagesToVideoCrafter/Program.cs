using ImagesToVideoCrafter.Options;

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
            var crafterOptions = SetOptions(ImagesToVideoCrafterOptions.Default, args);

            var Crafter = new Crafter(crafterOptions);
            string FullFileName = Crafter.Craft("Test Video", printInfoAction: Console.WriteLine);

            Console.WriteLine("Done. \nVideo saved: " + FullFileName);
            Console.ReadKey(true);
        }
    }
}
