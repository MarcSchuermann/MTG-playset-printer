using Microsoft.Extensions.Configuration;

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

      private string GetSetting(string key)
      {
         return configuration[key];
      }
   }
}
