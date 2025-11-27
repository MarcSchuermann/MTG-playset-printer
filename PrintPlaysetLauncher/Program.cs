
using Microsoft.Extensions.Logging;

using PrintPlayset;

namespace PrintPlaysetLauncher
{
   public class Program
   {
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates", Justification = "<Pending>")]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "<Pending>")]
      public static void Main(string[] args)
      {
         var logger = CreateLogger();
         logger.LogInformation($"App startet with args {string.Join(", ", args)}");

         var appConfig = new ApplicationConfiguration();
         logger.LogInformation($"Card width from config {appConfig.CardWidth}");
         logger.LogInformation($"Card height from config {appConfig.CardHeight}");

         foreach (var filePath in args)
         {
            if (!File.Exists(filePath))
               continue;

            ImagesManipulator.ResizeImages(new[] { filePath }, appConfig.CardWidth, appConfig.CardHeight).ToList();
         }


         logger.LogInformation($"App finished");
      }

      private static ILogger CreateLogger()
      {
         using var loggerFactory = LoggerFactory.Create(builder => builder.AddFilter("Default", LogLevel.Information).AddConsole());

         var logger = loggerFactory.CreateLogger<Program>();
         return logger;
      }
   }
}
