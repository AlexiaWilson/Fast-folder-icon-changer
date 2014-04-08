using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Friendly_folder_icon_customization
{
    static class ObservableCollectionExtensionMethods
    {
        public static void Replace<T>(this ObservableCollection<T> old, ObservableCollection<T> @new)
        {
            old.Clear();
            foreach (var item in @new)
            {
                old.Add(item);
            }
        }
    }
}
