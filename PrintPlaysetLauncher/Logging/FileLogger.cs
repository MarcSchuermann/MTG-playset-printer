// -----------------------------------------------------------------------
// <copyright file="FileLogger.cs" company="Marc Schuermann" />
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace PrintPlaysetLauncher.Logging
{
   internal sealed class FileLogger : ILogger
   {
      private readonly string path;
      public FileLogger(string path)
      {
         this.path = path;
      }

      public IDisposable BeginScope<TState>(TState state)
      {
         return null;
      }

      public bool IsEnabled(LogLevel logLevel)
      {
         return true;
      }

      public void Log<TState>(LogLevel logLevel, EventId eventId,
          TState state, Exception exception, Func<TState, Exception, string> formatter)
      {
         var message = $"{DateTime.Now}: {logLevel}: {formatter(state, exception)}";
         File.AppendAllText(path, message + Environment.NewLine);

         if (exception != null)
            File.AppendAllText(path, exception.ToString() + Environment.NewLine);
      }

   }
}
