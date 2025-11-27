
using Microsoft.Extensions.Logging;

using PrintPlayset;

using PrintPlaysetLauncher.Logging;

namespace PrintPlaysetLauncher
{
   public class Program
   {
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates", Justification = "<Pending>")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "<Pending>")]
      public static void Main(string[] args)
      {
         var appConfig = new ApplicationConfiguration();

         var logger = CreateLogger(appConfig.LogLevel);
         logger.LogInformation($"App startet with args {string.Join(", ", args)}");

         logger.LogInformation($"Card width from config {appConfig.CardWidth}");
         logger.LogInformation($"Card height from config {appConfig.CardHeight}");

         foreach (var filePath in args)
         {
            if (!File.Exists(filePath))
               continue;

            var resizedImages = ImagesManipulator.ResizeImages(logger, new[] { filePath }, appConfig.CardWidth, appConfig.CardHeight).ToList();
         }

         logger.LogInformation($"App finished");
      }

      private static ILogger CreateLogger(LogLevel logLevel)
      {
         var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PrintPlayset");
         if (!Directory.Exists(appDataPath))
            Directory.CreateDirectory(appDataPath);

         var logFile = Path.Combine(appDataPath, "printPlayset.log");
         if (!File.Exists(logFile))
            File.Create(logFile);

         using var loggerFactory = LoggerFactory.Create(builder => builder.AddFilter("Default", logLevel).AddProvider(new FileLoggerProvider(logFile)).AddConsole());

         var logger = loggerFactory.CreateLogger<Program>();
         return logger;
      }
   }
}
