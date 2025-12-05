using System.Drawing;

using Microsoft.Extensions.Logging;

namespace PrintPlayset
{
   public class ImagesManipulator
   {
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
      public static IEnumerable<string> ResizeImages(ILogger logger, IEnumerable<string> itemPaths, int cardWidth, int cardHeight)
      {
         foreach (var file in itemPaths)
         {
            using var stream = new FileStream(file, FileMode.Open);

            Image image;
            try
            {
               image = Image.FromStream(stream);
            }
            catch
            {
               logger.LogWarning(file + " is not readable!");
               continue;
            }

            using var bitmap = new Bitmap(cardWidth, cardHeight);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(image, 0, 0, cardWidth, cardHeight);
            bitmap.SetResolution(100, 100);

            var tempFile = Path.GetTempFileName().Replace(".tmp", ".png");
            bitmap.Save(tempFile);

            logger.LogInformation(file + " resized to " + tempFile);

            yield return tempFile;
         }
      }

   }
}
