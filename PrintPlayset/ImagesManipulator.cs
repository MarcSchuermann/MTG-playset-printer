using System.Diagnostics;
using System.Drawing;

namespace PrintPlayset
{
   public class ImagesManipulator
   {
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
      public static IEnumerable<string> ResizeImages(IEnumerable<string> itemPaths)
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
               Debug.WriteLine(file + " is not readable!");
               continue;
            }

            var applicationConfiguration = new ApplicationConfiguration();

            using var bitmap = new Bitmap(applicationConfiguration.CardWidth, applicationConfiguration.CardHeight);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(image, 0, 0, applicationConfiguration.CardWidth, applicationConfiguration.CardHeight);
            bitmap.SetResolution(100, 100);

            var tempFile = Path.GetTempFileName().Replace(".tmp", ".png");
            bitmap.Save(tempFile);

            yield return tempFile;
         }
      }

   }
}
