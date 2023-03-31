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
