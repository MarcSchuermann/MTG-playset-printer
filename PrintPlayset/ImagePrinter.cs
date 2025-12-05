
using System.Drawing;
using System.Drawing.Printing;

using Microsoft.Extensions.Logging;

using System.Windows.Forms;

namespace PrintPlayset
{
   public static class ImagePrinter
   {
      public static void PrintImages(ILogger logger, IEnumerable<string> imagesToPrint, bool openFilePreviewDialog)
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

               if (openFilePreviewDialog)
               {
                  logger.LogInformation("Open print dialog for file " + file);

                  var previewDialog = new PrintPreviewDialog
                  {
                     Document = printDocument
                  };

                  try
                  {
                     previewDialog.WindowState = FormWindowState.Maximized;
                     previewDialog.ShowDialog();
                  }
                  catch (Exception ex)
                  {
                     logger.LogError(ex, "Error showing print preview dialog for image " + file);
                  }
               }
               else
               {
                  logger.LogInformation("Printing file " + file);

                  try
                  {
                     printDocument.Print();
                  }
                  catch (Exception ex)
                  {
                     logger.LogError(ex, "Error printing image " + file);
                  }
               }
               logger.LogInformation("File " + file + " printed.");
            }
         }
      }
   }
}
