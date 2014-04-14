using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Media.Imaging;
using System.Security.Cryptography;

namespace Friendly_folder_icon_customization 
{
    public class Icon
    {
        public string FileLocation { get; set; }
        public BitmapImage Bitmap { get; set; }
        public int Index {get; set; }

        public Icon(string FileLocation)
        {
            this.FileLocation = FileLocation;
            Bitmap = new BitmapImage(new Uri(FileLocation));
            Index = 0;
        }

        public Icon()
        {

        }

        public new string GetHashCode() {
            using(var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(FileLocation))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }
    }

    public class LibraryIcon : Icon {
        public LibraryIcon(string FileLocation) : base(FileLocation) { }
    }
    public class StorageIcon : Icon {
        public StorageIcon(string FileLocation) : base(FileLocation) { }
    }

    class GridDataManager 
    {
        public ObservableCollection<Icon> Items 
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items.Replace(value);
            }
        }
        private ObservableCollection<Icon> _Items = new ObservableCollection<Icon>();

        private string library = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private string storage = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\QuickFolderIconizer";

        public void Scan()
        {
            var Icons = new ObservableCollection<Icon>();
            var FoundIcons = new List<string>();

            // Add all icons in our storage folder to our observer
            foreach (var iconFile in FindIcons(storage))
            {
                var icon = new StorageIcon(iconFile);
                Icons.Add(icon);
            }

            // Add all icons found in our recursive directory search to a list
            foreach (var folder in FindDirectories(library))
            {
                FoundIcons.AddRange(FindIcons(folder));
            }

            // Process that list into Icon classes and add to our observer
            foreach (var iconFile in FoundIcons)
            {
                var comparer = new IconComparer();
                var icon = new LibraryIcon(iconFile);

                // We prefer to display our stored icons over the ones inside our library
                if (Icons.Contains(icon, comparer))
                {
                    continue;
                }
                Icons.Add(icon);
            }

            Items = Icons;
        }

        // Recursively scans a folder for folders inside of it. Used to pass as an argument to Scan() 
        private List<string> FindDirectories(string folderuri)
        {
            var Folders = new List<string>();
            Folders.Add(folderuri);

            foreach (var folder in Directory.GetDirectories(folderuri))
            {
                Folders.AddRange(FindDirectories(folder));
            }

            return Folders;
        }

        // Returns the file names matching a pattern in a folder
        private List<string> FindIcons(string folder)
        {
            var Files = new List<string>();
            foreach (string file in Directory.GetFiles(folder))
            {
                if (!file.Contains(".ico"))
                {
                    continue;
                }

                Files.Add(file);
            }

            return Files;
        }
    }

    class IconComparer : IEqualityComparer<Icon>
    {
        public bool Equals(Icon icon1, Icon icon2)
        {
            return icon1.GetHashCode() == icon2.GetHashCode();
        }

        public int GetHashCode(Icon icon)
        {
            return icon.FileLocation.GetHashCode();
        }
    }
}

