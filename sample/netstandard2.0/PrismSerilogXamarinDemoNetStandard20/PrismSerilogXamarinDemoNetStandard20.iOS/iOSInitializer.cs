using Prism;
using Prism.Ioc;
using Serilog;
using Serilog.Core;

namespace PrismSerilogXamarinDemoNetStandard20.iOS
{
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations

            var stringBuilder = new System.Text.StringBuilder();
            var messages = new System.IO.StringWriter(stringBuilder);
            Log.Logger =
                new LoggerConfiguration()
                    .WriteTo.NSLog()
                    .WriteTo.TextWriter(messages)
                    .MinimumLevel.Debug()
                    .CreateLogger();
            containerRegistry.RegisterInstance(messages);
        }
    }
}
