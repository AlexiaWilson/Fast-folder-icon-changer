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
using System.Windows.Controls.Primitives;
using System.Resources;
using System.Diagnostics;

namespace Friendly_folder_icon_customization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void SelectionChanged(object sender, SelectionChangedEventArgs args);
        public string ActiveDirectory { get; private set; }

        private Page current_view;
        private Icon active_icon;

        // Operation controllers
        private GridDataManager gridManager;
        private StorageManager dataManager;
        private GridDataFolderScanner dataScanner;

        public MainWindow()
        {
            InitializeComponent();
            SelectionChanged selectionHandle = IconList_SelectionChanged;
            gridManager = new GridDataManager();
            dataManager = new StorageManager();
            dataScanner = new GridDataFolderScanner();
            DefaultPage defaultView = new DefaultPage();
            ResourcePage resourceView = new ResourcePage();

          /*  try 
            {
                ActiveDirectory = Environment.GetCommandLineArgs()[1];
            } 
            catch (IndexOutOfRangeException e) 
            {
                MessageBox.Show("Please run this program from the right click context menu");
                Environment.Exit(0);
            }*/
            ActiveDirectory = @"C:\Users\Alexia\Desktop\Completed Projects\Project Careerguide15 Python";



            defaultView.DataContext = gridManager;
            defaultView.IconList.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(selectionHandle));

            resourceView.DataContext = gridManager;
            resourceView.IconList.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(selectionHandle));


            current_view = defaultView;
            FrameView.Content = current_view;

            //// dleete below
            var blah = new Icon("C:\\users\\alexia\\pictures\\icons\\martz90-circle-timer.ico");
            SetViewIcon(blah);


            gridManager.Items = dataScanner.Scan(dataManager.Library);


        }

        private void SetViewIcon(Icon icon)
        {
            active_icon = icon;
            CurrentIcon.Source = icon.Bitmap;
        }

        private void SetViewIcon()
        {
            // Read icon in desktop.ini and use it, or use default 'unknown' icon
        }

        private void IconList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Icon Item = (Icon) e.AddedItems[0];
            SetViewIcon(Item);

        }

        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Save_click(object sender, RoutedEventArgs e)
        {
            dataManager.Save(active_icon, ActiveDirectory);
        }

        private void Clear_click(object sender, RoutedEventArgs e)
        {
            var icon = new Icon();
            icon.FileLocation = "";
            icon.Bitmap = new System.Windows.Media.Imaging.BitmapImage();
            icon.Index = 0;

            dataManager.Save(icon, ActiveDirectory);

            active_icon = icon;
            CurrentIcon.Source = icon.Bitmap;
        }
    }
}
