using System;
using Prism.Logging;
using Prism.Logging.Serilog;
using Serilog;

namespace Prism.Ioc
{
    /// <summary>
    /// Extends <see cref="T:Prism.Ioc.IContainerRegistry" /> with methods to register
    /// <see cref="T:Prism.Logging.Serilog.SerilogLoggerFacade" /> with Prism.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IContainerRegistryExtensions
    {
        /// <summary>
        /// Register a <see cref="T:Prism.Logging.Serilog.SerilogLoggerFacade" /> instance
        /// with Prism, which will forward log messages from Prism to Serilog
        /// </summary>
        /// <param name="containerRegistry">Prism container registry</param>
        public static void RegisterSerilog(this IContainerRegistry containerRegistry)
        {
            RegisterSerilog(containerRegistry, Log.Logger);
        }

        /// <summary>
        /// Register a <see cref="T:Prism.Logging.Serilog.SerilogLoggerFacade" /> instance
        /// with Prism, which will forward log messages from Prism to Serilog
        /// </summary>
        /// <param name="containerRegistry">Prism container registry.</param>
        /// <param name="logger">Serilog logger instance to forward the message to.</param>
        /// <param name="next">(Optional) Prism logger façade instance to forward the message to.</param>
        public static void RegisterSerilog(this IContainerRegistry containerRegistry, ILogger logger, ILoggerFacade next = null)
        {
            if (containerRegistry == null) throw new ArgumentNullException(nameof(containerRegistry));

            containerRegistry.RegisterInstance<ILoggerFacade>(new SerilogLoggerFacade(logger, next));
        }
    }
}
