using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Image_Viewer
{
    partial class MainWindow
    {
        private void OpenImage(int index)
        {
            selectedIndex = index;
            displayImage.Source = new BitmapImage(new Uri(images[selectedIndex]));
            BestFit();
        }

        private void BestFit()
        {
            double canvasHeight = imageBackground.ActualHeight;
            double canvasWidth = imageBackground.ActualWidth;
            if (displayImage.ActualHeight > 0 && displayImage.ActualHeight < canvasHeight)
                canvasHeight = displayImage.ActualHeight;
            if (displayImage.ActualWidth > 0 && displayImage.ActualWidth < canvasWidth)
                canvasWidth = displayImage.ActualWidth;
            RenderImage(Convert.ToInt16(canvasHeight), Convert.ToInt16(canvasWidth));
        }

        private void RenderImage(int height, int width)
        {
            if (height > displayImage.Height)
                imageBackground.Height = height;
            if (width > displayImage.Width)
                imageBackground.Width = width;
            displayImage.Height = height;
            displayImage.Width = width;
        }
        
        private void Zoom(int percentage)
        {
            BitmapImage image = new BitmapImage(new Uri(images[selectedIndex]));
            if (image.Width > imageBackground.Width || double.IsNaN(imageBackground.Width))
                imageBackground.Width = image.Width;
            if (image.Height > imageBackground.Height || double.IsNaN(imageBackground.Height))
                imageBackground.Height = image.Height;
            displayImage.Source = new BitmapImage(new Uri(images[selectedIndex]));
            displayImage.Height = image.Height;
            displayImage.Width = image.Width;
        }
    }
}
