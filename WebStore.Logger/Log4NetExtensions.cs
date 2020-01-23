using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4NetExtensions 
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory Factory, string ConfigurationFile = "log4net.config")
        {
            if (!Path.IsPathRooted(ConfigurationFile))
            {
                var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Не удалось определить сборку с точкой входа в приложение");
                var dir = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Не удалось определить каталог исполнительного файла");
                ConfigurationFile = Path.Combine(dir, ConfigurationFile);
            }

            Factory.AddProvider(new Log4NetProvider(ConfigurationFile));

            return Factory;
        }
    }
}