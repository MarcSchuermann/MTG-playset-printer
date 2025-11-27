using Microsoft.Extensions.Logging;

namespace PrintPlaysetLauncher.Logging
{
   internal class FileLogger : ILogger
   {
      private readonly string _path;
      public FileLogger(string path) => _path = path;

      public IDisposable BeginScope<TState>(TState state) => null;
      public bool IsEnabled(LogLevel logLevel) => true;

      public void Log<TState>(LogLevel logLevel, EventId eventId,
          TState state, Exception exception, Func<TState, Exception, string> formatter)
      {
         var message = $"{DateTime.Now}: {formatter(state, exception)}";
         File.AppendAllText(_path, message + Environment.NewLine);
      }

   }
}
