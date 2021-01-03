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
