// -----------------------------------------------------------------------
// <copyright file="ImagesManipulator.cs" company="Marc Schuermann" />
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace PrintPlayset
{
   public class ImagesManipulator
   {
      public static IEnumerable<string> ResizeImages(ILogger logger, IEnumerable<string> itemPaths, int cardWidth, int cardHeight)
      {
         foreach (var filePath in itemPaths)
         {
            using var stream = new FileStream(filePath, FileMode.Open);

            Image image;
            try
            {
               image = Image.FromStream(stream);
            }
            catch
            {
               logger.LogWarning($"{filePath} is not readable!");
               continue;
            }

            using var bitmap = new Bitmap(cardWidth, cardHeight);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(image, 0, 0, cardWidth, cardHeight);
            bitmap.SetResolution(100, 100);

            var tempFile = Path.GetTempFileName().Replace(".tmp", ".png");
            bitmap.Save(tempFile);

            logger.LogInformation($"{filePath} resized to {tempFile}");

            yield return tempFile;
         }
      }

   }
}
