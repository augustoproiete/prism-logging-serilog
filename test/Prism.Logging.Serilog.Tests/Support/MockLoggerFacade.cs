using System;
using System.Collections.Generic;

namespace Prism.Logging.Serilog.Tests.Support
{
    internal class MockLoggerFacade : ILoggerFacade
    {
        public List<Tuple<string, Category, Priority>> Messages { get; } =
            new List<Tuple<string, Category, Priority>>();

        public void Log(string message, Category category, Priority priority)
        {
            Messages.Add(new Tuple<string, Category, Priority>(message, category, priority));
        }
    }
}
