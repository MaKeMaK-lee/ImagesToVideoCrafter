
using ImagesToVideoCrafter_DesktopGUI.Options;
using ImagesToVideoCrafter_Options;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.Model
{
    public class GuiInstance : IGuiInstance
    {
        private IAdapter _adapter;
        private TextWriter logFileStream;

        private event EventHandler<string>? LogActions;

        public enum LogMode
        {
            DEBUG,
            INFO,
            WARN,
            ERROR,
        }

        public ImagesToVideoCrafterOptionsObservable CrafterOptions { get; }

        public bool Crafting { get; set; }

        public void Craft()
        {
            if (Crafting)
                return;

            Crafting = true;
            var startTime = DateTime.Now;

            var craftTask = _adapter
                .LaunchCraft(CrafterOptions,
                printInfoAction: (s) => LogAs(s, LogMode.INFO),
                printWarningAction: (s) => LogAs(s, LogMode.WARN),
                printDebugAction: (s) => { if (CrafterOptions.DebugMode) LogAs(s, LogMode.DEBUG); })
                .ContinueWith(t =>
                {
                    Crafting = false;

                    var time = (DateTime.Now - startTime);
                    LogAs("Завершено " +
                        (time.Hours == 0 ? "" : (time.Hours + "h ")) +
                        (time.Minutes == 0 ? "" : (time.Minutes + "m ")) +
                        time.Seconds + "." + time.Milliseconds + "s", LogMode.INFO);
                    LogAs("Видео сохранено: " + Path.GetFullPath(t.Result) + "\n", LogMode.INFO);
                });
        }

        public void LogAs(string message, LogMode? logMode = null)
        {
            var str =
                $"[{DateTime.Now.ToLongTimeString()}] " + //TODO 00:00:00
                $"[{(logMode.HasValue ? logMode.Value : "")}] " +
                message;

            LogActions?.Invoke(null, str);
        }

        public void AddLogAction(EventHandler<string> action)
        {
            LogActions += action;
        }

        public void Dispose()
        {
            logFileStream?.Dispose();
        }

        public GuiInstance(IAdapter adapter)
        {
            _adapter = adapter;

            logFileStream = TextWriter.Synchronized(File.CreateText("latest.log"));
            CrafterOptions = ImagesToVideoCrafterOptions.Default.ToOptionsObservable();

            LogActions += (o, message) =>
            {
                logFileStream?.WriteLine(message);
                logFileStream?.Flush();
            };

#if DEBUG
            string username = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            CrafterOptions.FFmpegBinaresDirectory = "C:\\Users\\" + username + "\\Desktop\\ffmpeg\\x86_64";

            LogActions += (o, message) => Debug.WriteLine(message);
#endif
        }
    }
}
