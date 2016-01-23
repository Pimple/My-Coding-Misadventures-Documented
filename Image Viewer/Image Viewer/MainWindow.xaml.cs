﻿using System;
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
        private int zoomValue;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            images = new List<string>();
            zoomValue = 100;
            selectedIndex = -1;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BestFit();
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            dialog.Multiselect = true;
            dialog.DefaultExt = "Image files (jpg, gif, png)";
            dialog.Filter = "Image files|*.jpg; *.jpeg; *.gif; *.png;";

            Nullable<bool> result = dialog.ShowDialog();

            if (result.HasValue && result.Value == true)
            {
                List<string> newImages = new List<string>();

                if (dialog.FileNames.Count() > 0)
                    foreach (string imagePath in dialog.FileNames)
                        newImages.Add(imagePath);

                this.images = newImages;
                OpenImage(0);
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if (images.Count() <= 0)
                return;
            if (selectedIndex >= images.Count() - 1)
                OpenImage(0);
            else
                OpenImage(selectedIndex + 1);
        }

        private void last_Click(object sender, RoutedEventArgs e)
        {
            if (images.Count() <= 0)
                return;
            if (selectedIndex == 0)
                OpenImage(images.Count() - 1);
            else
                OpenImage(selectedIndex - 1);
        }

        private void bestFit_Click(object sender, RoutedEventArgs e)
        {
            BestFit();
        }

        private void zoom_Click(object sender, RoutedEventArgs e)
        {
            Zoom(100);
        }
    }
}
