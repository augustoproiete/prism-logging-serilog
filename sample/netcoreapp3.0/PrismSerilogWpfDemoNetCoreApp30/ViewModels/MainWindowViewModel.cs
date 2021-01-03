#region Copyright 2019-2021 C. Augusto Proiete & Contributors
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
using System.IO;
using System.Text;
using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;

namespace PrismSerilogWpfDemoNetCoreApp30.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ILoggerFacade _logger;
        private string _title = "Prism Serilog WPF Demo (.NET Core 3.0)";

        public MainWindowViewModel(ILoggerFacade logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            LogDebugCommand = new DelegateCommand(LogDebug);
            LogInformationCommand = new DelegateCommand(LogInformation);
            LogWarningCommand = new DelegateCommand(LogWarning);
            LogExceptionCommand = new DelegateCommand(LogException);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Text
        {
            get
            {
                const string logFileName = "DemoLog.txt";

                if (!File.Exists(logFileName))
                {
                    return null;
                }

                // FileShare.ReadWrite required for Serilog to continue writing - File.ReadAllText doesn't allow that
                using (var stream = new FileStream(logFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public DelegateCommand LogDebugCommand { get; }
        public DelegateCommand LogInformationCommand { get; }
        public DelegateCommand LogWarningCommand { get; }
        public DelegateCommand LogExceptionCommand { get; }

        private void LogDebug()
        {
            _logger.Log("This is a Debug message!", Category.Debug, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogInformation()
        {
            _logger.Log("This is an Information message!", Category.Info, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogWarning()
        {
            _logger.Log("This is an Warning message!", Category.Warn, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }

        private void LogException()
        {
            _logger.Log("This is an Exception message!", Category.Exception, Priority.High);

            RaisePropertyChanged(nameof(Text));
        }
    }
}
