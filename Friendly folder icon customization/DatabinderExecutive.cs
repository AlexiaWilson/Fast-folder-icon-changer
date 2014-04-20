using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using System.ComponentModel;

namespace IconCustomizer
{
    class DatabinderExecutive : INotifyPropertyChanged
    {
        public List<IconInfo> FoundIcons
        {
            get { return _foundIcons; }
            set
            {
                _foundIcons = value;
                NotifyPropertyChanged();
            }
        } // Databinding: List of icons in library & storage
        private List<IconInfo> _foundIcons;
        public IconInfo DisplayedIcon
        {
            get { return _displayedIcon; }
            set
            {
                _displayedIcon = value;
                NotifyPropertyChanged();
            }
        }    // Databinding: Icon being used by the directory
        private IconInfo _displayedIcon;

        private IconManager _iconManager;

        public DatabinderExecutive(MainWindow mainWindow)
        {
            _iconManager = new IconManager();
        }

        // Loads FoundIcons & DisplayIcon with data
        public void Start()
        {
            QueryFiles();
            QueryIcon();
        }


        // Loads FoundIcons
        private void QueryFiles()
        {
            FoundIcons = _iconManager.FindIcons();
        }

        // Loads DisplayIcon
        private void QueryIcon()
        {
            DisplayedIcon = _iconManager.FolderIcon();
        }

        // Sets the folders icon
        public void Set(IconInfo icon)
        {
            _iconManager.SetIcon(icon);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
