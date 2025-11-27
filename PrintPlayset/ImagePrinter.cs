
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.Extensions.Logging;

namespace PrintPlayset
{
   public static class ImagePrinter
   {
      public static void PrintImages(ILogger logger, IEnumerable<string> imagesToPrint)
      {
         foreach (var file in imagesToPrint)
         {
            if (!File.Exists(file))
            {
               logger.LogInformation("File " + file + " doesn't exist!");
               continue;
            }

            using (var stream = new FileStream(file, FileMode.Open))
            {
               var printDocument = new PrintDocument();
               printDocument.PrintPage += (_, e) =>
               {
                  try
                  {
                     var i = Image.FromStream(stream);
                     e.Graphics.DrawImage(i, new Point(0, 0));
                     e.Graphics.DrawImage(i, new Point(235, 0));
                     e.Graphics.DrawImage(i, new Point(0, 335));
                     e.Graphics.DrawImage(i, new Point(235, 335));
                  }
                  catch (Exception ex)
                  {
                     logger.LogError(ex, "Error printing image " + file);
                  }
               };

               printDocument.Print();
               logger.LogInformation("File " + file + " printed.");
            }
         }
      }
   }
}
