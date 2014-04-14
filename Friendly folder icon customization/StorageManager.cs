using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Friendly_folder_icon_customization
{
    class StorageManager
    {
        private string library = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private string storage = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\QuickFolderIconizer";

        public string Storage { get; set; }

        public StorageManager()
        {


        }

        public void Save(Icon icon, string directory)
        {
            if (icon.GetType() == typeof (LibraryIcon))
            {
                var storedIcon = Store(icon.FileLocation);
                ShellAPI.SetIcon(directory, storedIcon);
            }
            else
            {
                ShellAPI.SetIcon(directory, icon);
            }
        }

        private Icon Store(string FileLocation)
        {
            var fileName = FileLocation.Substring(FileLocation.LastIndexOf(@"\"));
            var storageFileName = storage + @"\" + fileName;
            File.Copy(FileLocation, storageFileName);

            return new StorageIcon(storageFileName);
        }
    }
}
