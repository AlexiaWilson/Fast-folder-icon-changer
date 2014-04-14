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
        public string Storage { get; set; }

        public StorageManager()
        {


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
            ShellAPI.SetIcon(directory, icon);
        }
    }
}
