using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Media.Imaging;

namespace Friendly_folder_icon_customization 
{
    class Icon
    {
        public string FileLocation { get; set; }
        public BitmapImage Bitmap { get; set; }
        
        public Icon() { }
        public Icon(string FileLocation)
        {
            this.FileLocation = FileLocation;
        }
        public Icon(string FileLocation, BitmapImage Bitmap)
        {
            this.FileLocation = FileLocation;
            this.Bitmap = Bitmap;
        }
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

        private ObservableCollection<Icon> _Items;

        public GridDataManager()
        {
            _Items = new ObservableCollection<Icon>();
        }
    }

    class GridDataFolderScanner
    {
        public GridDataFolderScanner()
        {

        }

        public ObservableCollection<Icon> Scan(string Folder)
        {
            var Files = ScanFolder(Folder);
            var ParsedImages = new ObservableCollection<Icon>();

            foreach (var File in Files)
            {
                var Image = new BitmapImage(new Uri(File));

                ParsedImages.Add(new Icon(File, Image));
            }

            return ParsedImages;
        }

        public ObservableCollection<Icon> Scan(List<string> Folders )
        {
            var FoundImages = new List<string>();
            var ParsedImages = new ObservableCollection<Icon>();

            foreach (var Folder in Folders)
            {
                FoundImages.AddRange(ScanFolder(Folder));
            }

            foreach(var ImageFile in FoundImages)
            {
                var Image = new BitmapImage(new Uri(ImageFile));

                ParsedImages.Add(new Icon(ImageFile, Image));
            }

            return ParsedImages;
        }

        // Returns the file names matching a pattern in a folder
        private List<string> ScanFolder(string folder)
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
}
