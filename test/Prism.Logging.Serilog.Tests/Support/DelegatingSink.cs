using System;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Prism.Logging.Serilog.Tests.Support
{
    internal class DelegatingSink : ILogEventSink
    {
        private readonly Action<LogEvent> _writeAction;

        public DelegatingSink(Action<LogEvent> writeAction)
        {
            _writeAction = writeAction ?? throw new ArgumentNullException(nameof(writeAction));
        }

        public void Emit(LogEvent logEvent)
        {
            _writeAction(logEvent);
        }

        public static ILogger GetLogger(Action<LogEvent> writeAction)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(new DelegatingSink(writeAction))
                .CreateLogger();

            return logger;
        }
    }
}
