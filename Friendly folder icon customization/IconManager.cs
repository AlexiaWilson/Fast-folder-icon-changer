using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Friendly_folder_icon_customization
{
    class IconManager
    {
        private FileDept _fileDept;

        public IconManager()
        {
            _fileDept = new FileDept();   
        }

        // Parses a list of file paths into IconInfo objects
        public List<IconInfo> FindIcons()
        {
            var icons = new List<IconInfo>();
            List<string> iconFilePaths = _fileDept.SearchForIcons();

            foreach (var iconPath in iconFilePaths)
            {
                icons.Add(FormatIcon(iconPath));
            }

            return icons;
        }

        // Loads and returns an IconInfo
        public IconInfo FormatIcon(string fileLocation)
        {
            return new IconInfo(fileLocation);
        }

        // Returns an IconInfo representing the folders current icon
        public IconInfo FolderIcon()
        {
            // If our folder has no icon associated with it, call FormatIcon with a null file path 
            var filePath = _fileDept.GetFolderIcon();

            // Check for an empty/nonexistent file path
            var icon = (filePath.Length > 0) ? filePath : null;

            // Protect against a crash if the icon has been deleted and the desktop.ini wasn't updated
            icon = File.Exists(icon) ? icon : null;

            // Protect against trying to file load a DLL resource
            // [SIC-16]
            icon = icon.EndsWith(".dll") ? null : icon;

            return FormatIcon(icon);
        }

        // Sets (or clears) a folders icon
        public void SetIcon(IconInfo icon)
        {
            if (icon == null)
            {
                _fileDept.ClearFolderIcon();
            }
            else
            {
                _fileDept.SetFolderIcon(icon.FilePath, icon.FileName);
            }
        }
    }

    // Contains information about an icon
    class IconInfo
    {
        public string FilePath = "";
        public string FileName = "";
        public BitmapImage Bitmap { get; set; }
        public int Index = 0;

        public IconInfo(string filePath = null, int index = 0)
        {
            // If the folders icon is being removed, IconInfo will receive a null filePath
            if (filePath == null)
            {
                Bitmap = new BitmapImage();
                return;
            }

            FilePath = filePath;
            FileName = filePath.Substring(filePath.LastIndexOf(@"\"));
            Index = index;
            Bitmap = new BitmapImage();
            Bitmap.BeginInit();
            Bitmap.CacheOption = BitmapCacheOption.OnLoad;
            Bitmap.UriSource = new Uri(filePath);
            Bitmap.EndInit();
        }
    }
}
