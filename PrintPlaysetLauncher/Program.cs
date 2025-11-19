using PrintPlayset;

namespace PrintPlaysetLauncher
{
   public class Program
   {
      public static void Main(string[] args)
      {
         Console.WriteLine("Hello, World!");

         foreach (var arg in args)
         {
            Console.WriteLine(arg);
         }

         foreach (var filePath in args)
         {
            if (!File.Exists(filePath))
               continue;

            ImagesManipulator.ResizeImages(new[] { filePath }).ToList();
         }


         Console.WriteLine("End, World!");
      }
   }
}
