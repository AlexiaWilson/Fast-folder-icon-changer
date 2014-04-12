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
        public List<string> Library { get; set; }
        public string Storage { get; set; }

        public StorageManager()
        {
            Library = new List<string>();

            //FIXME:
            Library.Add("C:\\Users\\Alexia\\Pictures\\Icons");
        }

        public void Save(Icon icon, string directory)
        {
            /* 
             * if file is in appdata storage:
             *  seticon
             * otherwise:
             *  copy icon to appdata storage
             *  seticon
             *  
             */
            IconImport.SetIcon(directory, icon);
        }
    }
}
