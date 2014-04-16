using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Friendly_folder_icon_customization
{
    class UserCallDispatcher
    {
        private IconInfo _selectedIcon;
        private DatabinderExecutive dataExecutive;

        public UserCallDispatcher(DatabinderExecutive dataExecutive)
        {
            this.dataExecutive = dataExecutive;
            _selectedIcon = null;
        }

        public void SelectionHandler(object sender, SelectionChangedEventArgs e)
        {

            IconInfo item = (IconInfo) e.AddedItems[0];
            _selectedIcon = item;
        }

        public void ClearHandler(object sender, RoutedEventArgs e)
        {
            _selectedIcon = null;
            dataExecutive.Set(_selectedIcon);
            Environment.Exit(0);
        }

        public void SaveHandler(object sender, RoutedEventArgs e)
        {
            dataExecutive.Set(_selectedIcon);
            Environment.Exit(0);
        }

        public void ExitHandler(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
