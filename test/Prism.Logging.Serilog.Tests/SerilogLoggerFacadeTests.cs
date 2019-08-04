using System;
using Serilog;
using Serilog.Events;
using Xunit;
using Prism.Logging.Serilog.Tests.Support;

namespace Prism.Logging.Serilog.Tests
{
    public class SerilogLoggerFacadeTests
    {
        [Fact]
        public void Log_messages_of_Category_Debug_are_forwarded_to_Serilog_logger_as_Debug()
        {
            TestCategoryToLogEventLevelMap(Category.Debug, LogEventLevel.Debug);
        }

        [Fact]
        public void Log_messages_of_Category_Info_are_forwarded_to_Serilog_logger_as_Information()
        {
            TestCategoryToLogEventLevelMap(Category.Info, LogEventLevel.Information);
        }

        [Fact]
        public void Log_messages_of_Category_Warn_are_forwarded_to_Serilog_logger_as_Information()
        {
            TestCategoryToLogEventLevelMap(Category.Warn, LogEventLevel.Warning);
        }

        [Fact]
        public void Log_messages_of_Category_Exception_are_forwarded_to_Serilog_logger_as_Error()
        {
            TestCategoryToLogEventLevelMap(Category.Exception, LogEventLevel.Error);
        }

        [Fact]
        public void Log_messages_with_unknown_Category_throws_ArgumentOutOfRangeException()
        {
            LogEvent logEvent = null;
            var logger = DelegatingSink.GetLogger(le => logEvent = le);

            const string message = "This is an Exception message";

            var target = new SerilogLoggerFacade(logger);

            Assert.Throws<ArgumentOutOfRangeException>(() => target.Log(message, (Category) (-1), Priority.None));
            Assert.Null(logEvent);
        }

        [Fact]
        public void Log_messages_have_SourceContext_set_to_SerilogLoggerFacade_FullName()
        {
            LogEvent logEvent = null;
            var logger = DelegatingSink.GetLogger(le => logEvent = le);

            var target = new SerilogLoggerFacade(logger);
            target.Log(string.Empty, Category.Exception, Priority.None);

            Assert.True(logEvent.Properties.ContainsKey("SourceContext"));

            var sourceContext = logEvent.Properties["SourceContext"].LiteralValue();
            Assert.Equal(typeof(SerilogLoggerFacade).FullName, sourceContext);
        }

        [Fact]
        public void Log_messages_with_Priority_None_are_forwarded_to_Serilog_logger_with_LogContext_property_Priority_with_value_None()
        {
            TestPriorityToPropertyMap(Priority.None, nameof(Priority), nameof(Priority.None));
        }

        [Fact]
        public void Log_messages_with_Priority_High_are_forwarded_to_Serilog_logger_with_LogContext_property_Priority_with_value_High()
        {
            TestPriorityToPropertyMap(Priority.High, nameof(Priority),nameof(Priority.High));
        }

        [Fact]
        public void Log_messages_with_Priority_Medium_are_forwarded_to_Serilog_logger_with_LogContext_property_Priority_with_value_Medium()
        {
            TestPriorityToPropertyMap(Priority.Medium, nameof(Priority), nameof(Priority.Medium));
        }

        [Fact]
        public void Log_messages_with_Priority_Low_are_forwarded_to_Serilog_logger_with_LogContext_property_Priority_with_value_Low()
        {
            TestPriorityToPropertyMap(Priority.Low, nameof(Priority), nameof(Priority.Low));
        }

        [Fact]
        public void Log_messages_are_forwarded_to_another_Prism_ILoggerFacade_if_one_is_provided()
        {
            var logger = new LoggerConfiguration().CreateLogger();
            var loggerFacade = new MockLoggerFacade();

            const string message = "This is an Exception message of High priority";
            const Category category = Category.Exception;
            const Priority priority = Priority.High;

            var target = new SerilogLoggerFacade(logger, loggerFacade);
            target.Log(message, category, priority);

            Assert.Equal(1, loggerFacade.Messages.Count);
            Assert.Equal(message, loggerFacade.Messages[0].Item1);
            Assert.Equal(category, loggerFacade.Messages[0].Item2);
            Assert.Equal(priority, loggerFacade.Messages[0].Item3);
        }

        [Fact]
        public void Serilog_Log_Logger_is_used_by_default_if_no_ILogger_injected_via_constructor()
        {
            LogEvent logEvent = null;
            Log.Logger = DelegatingSink.GetLogger(le => logEvent = le);

            const string message = "This is a Debug message";

            var target = new SerilogLoggerFacade();
            target.Log(message, Category.Debug, Priority.None);

            Assert.Equal(LogEventLevel.Debug, logEvent.Level);
        }

        private static void TestCategoryToLogEventLevelMap(Category category, LogEventLevel expectedLogEventLevel)
        {
            LogEvent logEvent = null;
            var logger = DelegatingSink.GetLogger(le => logEvent = le);

            var message = $"This is a {category} message";

            var target = new SerilogLoggerFacade(logger);
            target.Log(message, category, Priority.None);

            Assert.Equal(expectedLogEventLevel, logEvent.Level);
            Assert.Equal(message, logEvent.MessageTemplate.Text);
        }

        private static void TestPriorityToPropertyMap(Priority priority, string propertyName, string expectedValue)
        {
            LogEvent logEvent = null;
            var logger = DelegatingSink.GetLogger(le => logEvent = le);

            var target = new SerilogLoggerFacade(logger);
            target.Log(string.Empty, Category.Exception, priority);

            Assert.True(logEvent.Properties.ContainsKey(propertyName));

            var priorityValue = logEvent.Properties[propertyName].LiteralValue();
            Assert.Equal(expectedValue, priorityValue);
        }
    }
}
