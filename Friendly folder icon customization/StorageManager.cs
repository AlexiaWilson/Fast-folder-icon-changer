using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
