using System;
using System.Collections.Generic;
using System.IO;
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

namespace Image_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> images;
        private int selectedIndex;
        private double zoomValue;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            images = new List<string>();
            zoomValue = 1;
            selectedIndex = -1;

            window.MouseWheel += OnMouseWheel;
            displayImage.MouseWheel += OnMouseWheel;
            imageBackground.MouseWheel += OnMouseWheel;
            scrollViewer.MouseWheel += OnMouseWheel;
            scrollViewer.PreviewMouseWheel += PreventScrollViewerFromIrritatingMe;
            uberGrid.MouseWheel += OnMouseWheel;

            // Navigation methods
            zoom.Click += OnZoom;
            bestFit.Click += OnBestFit;
            previous.Click += OnPrevious;
            browse.Click += OnBrowse;
            next.Click += OnNext;
            exit.Click += OnExit;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (images.Count() > 0)
                BestFit();
        }

        private void OnBrowse(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialog.DefaultExt = "Image files (jpg, gif, png)";
            dialog.Filter = "Image files|*.jpg; *.jpeg; *.gif; *.png;";

            Nullable<bool> result = dialog.ShowDialog();

            if (result.Value == true)
            {
                string directory = System.IO.Path.GetDirectoryName(dialog.FileName);
                images = Directory.EnumerateFiles(directory).Where
                    (
                        filename => filename.EndsWith(".jpg")
                        || filename.EndsWith(".jpeg")
                        || filename.EndsWith(".gif")
                        || filename.EndsWith(".png")
                    )
                    .ToList<string>();

                OpenImage(images.IndexOf(dialog.FileName));
            }
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            if (images.Count() <= 0)
                return;
            if (selectedIndex >= images.Count() - 1)
                OpenImage(0);
            else
                OpenImage(selectedIndex + 1);
        }

        private void OnPrevious(object sender, RoutedEventArgs e)
        {
            if (images.Count() <= 0)
                return;
            if (selectedIndex == 0)
                OpenImage(images.Count() - 1);
            else
                OpenImage(selectedIndex - 1);
        }

        private void OnBestFit(object sender, RoutedEventArgs e)
        {
            BestFit();
        }

        private void OnZoom(object sender, RoutedEventArgs e)
        {
            zoomValue = 1;
            Zoom(zoomValue);
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnMouseWheelIfScrollable(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || !IsImageScrollable())
                OnMouseWheel(sender, e);
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                zoomValue *= 1.1;
            else if (e.Delta < 0)
                zoomValue *= 0.9;
            Zoom(zoomValue);
        }

        private void PreventScrollViewerFromIrritatingMe(object sender, MouseWheelEventArgs e)
        {
            if (displayImage.Source != null && sender is ScrollViewer && !e.Handled)
            {
                e.Handled = true;
                MouseWheelEventArgs eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                UIElement parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
