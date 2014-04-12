using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Friendly_folder_icon_customization
{
    /// <summary>
    /// Interaction logic for ResourcePage.xaml
    /// </summary>
    public partial class ResourcePage : Page
    {
        /*public static readonly RoutedEvent IconSelectionEvent = EventManager.RegisterRoutedEvent("IconSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ResourcePage));
        public event RoutedEventHandler IconChanged
        {
            add { AddHandler(IconSelectionEvent, value); }
            remove { RemoveHandler(IconSelectionEvent, value); }
        }*/

        public ResourcePage()
        {
            InitializeComponent();
        }

       /* private void IconList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newEventArgs = new RoutedEventArgs(IconSelectionEvent, e);
            RaiseEvent(newEventArgs);
        }*/
    }
}
