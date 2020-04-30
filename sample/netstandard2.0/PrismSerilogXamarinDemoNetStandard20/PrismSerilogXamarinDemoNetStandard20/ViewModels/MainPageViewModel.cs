using System;
using System.IO;
using System.Text;
using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;

namespace PrismSerilogXamarinDemoNetStandard20.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private ILoggerFacade _logger;
        private readonly StringWriter _stringWriter;
        private readonly StringBuilder _stringBuilder;

        public MainPageViewModel(ILoggerFacade logger, StringWriter stringWriter)
        {
            _stringBuilder = stringWriter.GetStringBuilder();

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _stringWriter = stringWriter;

            LogDebugCommand = new DelegateCommand(LogDebug);
            LogInformationCommand = new DelegateCommand(LogInformation);
            LogWarningCommand = new DelegateCommand(LogWarning);
            LogExceptionCommand = new DelegateCommand(LogException);
        }

        public DelegateCommand LogExceptionCommand { get; set; }

        public DelegateCommand LogWarningCommand { get; set; }

        public DelegateCommand LogInformationCommand { get; set; }

        public DelegateCommand LogDebugCommand { get; set; }

        public string Text => _stringBuilder.ToString();

        private void LogDebug()
        {
            _logger.Log("Debug from Serilog!", Category.Debug, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogInformation()
        {
            _logger.Log("Information from Serilog!", Category.Info, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogWarning()
        {
            _logger.Log("Warning from Serilog!", Category.Warn, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogException()
        {
            _logger.Log("Exception from Serilog!", Category.Exception, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }
    }
}
