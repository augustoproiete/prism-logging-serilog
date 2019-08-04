using Serilog.Events;

namespace Prism.Logging.Serilog.Tests.Support
{
    internal static class Extensions
    {
        public static object LiteralValue(this LogEventPropertyValue propertyValue)
        {
            return ((ScalarValue)propertyValue).Value;
        }
    }
}
