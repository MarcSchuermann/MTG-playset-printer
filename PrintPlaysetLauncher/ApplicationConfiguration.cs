using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PrintPlayset
{
   internal class ApplicationConfiguration
   {
      private readonly IConfigurationRoot configuration;
      public ApplicationConfiguration()
      {
         var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);

         configuration = builder.Build();
      }

      public int CardWidth => int.TryParse(GetSetting("CardWidth"), out var parsedWidth) ? parsedWidth : 235;

      public int CardHeight => int.TryParse(GetSetting("CardHeight"), out var parsedHeigth) ? parsedHeigth : 335;

      public LogLevel LogLevel => Enum.TryParse(GetSetting("LogLevel"), out LogLevel parsedLogLevel) ? parsedLogLevel : LogLevel.Information;

      public bool OpenFilePreviewDialog => bool.TryParse(GetSetting("OpenFilePreviewDialog"), out var parsedOpenFilePreviewDialog) && parsedOpenFilePreviewDialog;

      private string GetSetting(string key)
      {
         return configuration[$"AppSettings:{key}"];
      }
   }
}
