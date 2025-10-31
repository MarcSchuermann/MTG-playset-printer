using SHDocVw;

using Shell32;

namespace PrintPlayset
{
   public class FileCollector
   {
      public IList<FolderItems> GetSelectedFolderItems()
      {
         var selectedItems = new List<FolderItems>();

         foreach (InternetExplorer window in new ShellWindows())
         {
            var filename = Path.GetFileNameWithoutExtension(window.FullName).ToLowerInvariant();
            if (filename == "explorer")
            {
               if (window.Document is IShellFolderViewDual2 folderView)
               {
                  var items = folderView.SelectedItems();
                  foreach (FolderItems item in items)
                  {
                     selectedItems.Add(item);
                  }
               }
            }
         }

         return selectedItems;
      }
   }
}
