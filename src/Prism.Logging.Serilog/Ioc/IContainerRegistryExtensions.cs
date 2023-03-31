#region Copyright 2019-2023 C. Augusto Proiete & Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

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
