// -----------------------------------------------------------------------
// <copyright file="FileLoggerProvider.cs" company="Marc Schuermann" />
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace PrintPlaysetLauncher.Logging
{
   internal sealed class FileLoggerProvider : ILoggerProvider
   {
      private readonly string path;
      public FileLoggerProvider(string path)
      {
         this.path = path;
      }

      public ILogger CreateLogger(string categoryName)
      {
         return new FileLogger(path);
      }

      public void Dispose() { }

   }
}
