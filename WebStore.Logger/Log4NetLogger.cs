using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Extensions.Logging;
using log4net;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _Log;

        public Log4NetLogger(string CategoryName, XmlElement Configuration)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), 
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _Log = LogManager.GetLogger(logger_repository.Name, CategoryName);
            log4net.Config.XmlConfigurator.Configure(logger_repository, Configuration);
        }

        public bool IsEnabled(LogLevel Level)
        {
            switch (Level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), Level, null);

                case LogLevel.Trace: 
                case LogLevel.Debug:
                    return _Log.IsDebugEnabled;

                case LogLevel.Information:
                    return _Log.IsInfoEnabled;

                case LogLevel.Warning:
                    return _Log.IsWarnEnabled;

                case LogLevel.Error:
                    return _Log.IsErrorEnabled;

                case LogLevel.Critical:
                    return _Log.IsFatalEnabled;

                case LogLevel.None:
                    return false;
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public void Log<TState>(LogLevel Level, EventId EventId, TState State, Exception Error, Func<TState, Exception, string> Formatter)
        {
            if(Formatter is null) throw new ArgumentNullException(nameof(Formatter));

            if(!IsEnabled(Level)) return;

            var log_message = Formatter(State, Error);

            if (string.IsNullOrEmpty(log_message) && Error is null) return;

            switch (Level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), Level, null);

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _Log.Debug(log_message);
                    break;

                case LogLevel.Information: 
                    _Log.Info(log_message);
                    break;

                case LogLevel.Warning: 
                    _Log.Warn(log_message);
                    break;

                case LogLevel.Error: 
                    _Log.Error(log_message ?? Error.ToString());
                    break;

                case LogLevel.Critical: 
                    _Log.Fatal(log_message ?? Error.ToString());
                    break;

                case LogLevel.None: 
                    break;
            }

        }
    }
}
