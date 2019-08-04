using System;
using Serilog;

namespace Prism.Logging.Serilog
{
    /// <summary>
    /// Forwards logs from Prism to Serilog
    /// </summary>
    public class SerilogLoggerFacade : ILoggerFacade
    {
        private readonly ILogger _loggerPriorityNone;
        private readonly ILogger _loggerPriorityHigh;
        private readonly ILogger _loggerPriorityMedium;
        private readonly ILogger _loggerPriorityLow;

        private readonly ILoggerFacade _next;

        /// <summary>
        /// Construct a <see cref="T:Prism.Logging.Serilog.SerilogLoggerFacade" /> that
        /// forwards logs to <see cref="T:Serilog.Log.Logger" />.
        /// </summary>
        public SerilogLoggerFacade()
            : this(global::Serilog.Log.Logger)
        {
        }

        /// <summary>
        /// Construct a <see cref="T:Prism.Logging.Serilog.SerilogLoggerFacade" /> that
        /// that forwards logs to the <see cref="T:Serilog.ILogger" /> injected.
        /// </summary>
        /// <param name="logger">Serilog logger instance to forward the message to.</param>
        /// <param name="next">(Optional) Prism logger façade instance to forward the message to.</param>
        public SerilogLoggerFacade(ILogger logger, ILoggerFacade next = null)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            var contextLogger = logger.ForContext<SerilogLoggerFacade>();
            _loggerPriorityNone = contextLogger.ForContext(nameof(Priority), nameof(Priority.None));
            _loggerPriorityHigh = contextLogger.ForContext(nameof(Priority), nameof(Priority.High));
            _loggerPriorityMedium = contextLogger.ForContext(nameof(Priority), nameof(Priority.Medium));
            _loggerPriorityLow = contextLogger.ForContext(nameof(Priority), nameof(Priority.Low));

            _next = next;
        }

        /// <inheritdoc />
        public void Log(string message, Category category, Priority priority)
        {
            ILogger logger;

            switch (priority)
            {
                case Priority.None:
                    logger = _loggerPriorityNone;
                    break;
                case Priority.High:
                    logger = _loggerPriorityHigh;
                    break;
                case Priority.Medium:
                    logger = _loggerPriorityMedium;
                    break;
                case Priority.Low:
                    logger = _loggerPriorityLow;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(priority), priority, $"Unknown {nameof(Priority)}: `{priority}`");
            }

            switch (category)
            {
                case Category.Debug:
                    logger.Debug(message);
                    break;
                case Category.Info:
                    logger.Information(message);
                    break;
                case Category.Warn:
                    logger.Warning(message);
                    break;
                case Category.Exception:
                    logger.Error(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, $"Unknown {nameof(Category)}: `{category}`");
            }

            _next?.Log(message, category, priority);
        }
    }
}
