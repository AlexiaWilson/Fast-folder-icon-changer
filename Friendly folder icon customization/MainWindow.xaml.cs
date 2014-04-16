using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Friendly_folder_icon_customization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private DefaultPage _libraryView = new DefaultPage();
        private ResourcePage _resourceView = new ResourcePage();
        private Page _viewInUse;

        private DatabinderExecutive dataExecutive;
        private UserCallDispatcher userEventHandlers;
        public MainWindow()
        {
            InitializeComponent();
            dataExecutive = new DatabinderExecutive(this);
            userEventHandlers = new UserCallDispatcher(dataExecutive);

            _libraryView.DataContext = dataExecutive;
            _resourceView.DataContext = dataExecutive;
            CurrentIcon.DataContext = dataExecutive;

            _viewInUse = _libraryView;
            FrameView.Content = _viewInUse;

            _libraryView.IconList.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(userEventHandlers.SelectionHandler));
            _resourceView.IconList.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(userEventHandlers.SelectionHandler));

            ClearButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(userEventHandlers.ClearHandler));
            SaveButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(userEventHandlers.SaveHandler));
            CloseButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(userEventHandlers.ExitHandler));

            dataExecutive.Start();
        }
    }
}
