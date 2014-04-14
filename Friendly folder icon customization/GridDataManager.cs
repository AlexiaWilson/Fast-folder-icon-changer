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

        // TODO: Constructor for DLL Resource icons
        public Icon()
        {

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
                ParsedImages.Add(new Icon(File));
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
                ParsedImages.Add(new Icon(ImageFile));
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

