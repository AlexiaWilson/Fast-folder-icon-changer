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
using System.Drawing;

namespace Friendly_folder_icon_customization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DefaultPage defaultView = new DefaultPage();
            ResourcePage resourceView = new ResourcePage();
            GridDataManager gridManager = new GridDataManager();
            StorageManager dataManager = new StorageManager();
            GridDataFolderScanner dataScanner = new GridDataFolderScanner();

            defaultView.DataContext = gridManager;
            resourceView.DataContext = gridManager;

            FrameView.Content = defaultView;

            //// dleete below
            CurrentIcon.Source = new BitmapImage(new Uri("C:\\Users\\Alexia\\Pictures\\Icons\\Martz90-Circle-Timer.ico"));

            gridManager.Items = dataScanner.Scan(dataManager.Library);
        }
    }
}
