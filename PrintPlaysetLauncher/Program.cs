// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Marc Schuermann" />
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

using PrintPlayset;

using PrintPlaysetLauncher.Logging;

namespace PrintPlaysetLauncher
{
   public class Program
   {
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
            {
               logger.LogWarning($"File {filePath} does not exist. Skipping.");
               continue;
            }

            var resizedImages = ImagesManipulator.ResizeImages(logger, [filePath], appConfig.CardWidth, appConfig.CardHeight).ToList();

            ImagePrinter.PrintImages(logger, resizedImages, appConfig.OpenFilePreviewDialog);
         }

         logger.LogInformation("App finished");
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
