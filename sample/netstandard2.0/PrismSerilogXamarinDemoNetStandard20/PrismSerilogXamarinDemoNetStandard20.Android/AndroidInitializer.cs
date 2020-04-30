using Android.Provider;
using Java.IO;
using Java.Lang;
using Prism;
using Prism.Ioc;
using Serilog;

namespace PrismSerilogXamarinDemoNetStandard20.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations

            var stringBuilder = new System.Text.StringBuilder();
            var messages = new System.IO.StringWriter(stringBuilder);
            Log.Logger =
                new LoggerConfiguration()
                    .WriteTo.AndroidLog()
                    .WriteTo.TextWriter(messages)
                    .MinimumLevel.Debug()
                    .CreateLogger();

            containerRegistry.RegisterInstance(messages);
        }
    }
}
