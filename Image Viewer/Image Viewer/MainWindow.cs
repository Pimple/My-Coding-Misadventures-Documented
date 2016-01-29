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
            displayImage.Source = new BitmapImage(new Uri(images[index]));
            selectedIndex = index;
            BestFit();
        }

        private void BestFit()
        {
            BitmapImage image = new BitmapImage(new Uri(images[selectedIndex]));
            double height = image.Height;
            double width = image.Width;
            
            double maxHeight = scrollViewer.ActualHeight;
            double maxWidth = scrollViewer.ActualWidth;

            imageBackground.Height = maxHeight;
            imageBackground.Width = maxWidth;

            double scale = Math.Min(maxHeight / height,  maxWidth / width);
            height *= scale;
            width *= scale;

            RenderImage(image, Convert.ToInt16(height) - 1, Convert.ToInt16(width) - 1);
        }

        private void RenderImage(BitmapImage image, int height, int width)
        {
            imageBackground.Height = height;
            imageBackground.Width = width;
            displayImage.Height = height;
            displayImage.Width = width;
            displayImage.Source = image;
        }
        
        private void Zoom(int percentage)
        {
            BitmapImage image = new BitmapImage(new Uri(images[selectedIndex]));
            if (image.Width > imageBackground.Width || double.IsNaN(imageBackground.Width))
                imageBackground.Width = image.Width;
            if (image.Height > imageBackground.Height || double.IsNaN(imageBackground.Height))
                imageBackground.Height = image.Height;
            RenderImage(image, Convert.ToInt16(image.Height), Convert.ToInt16(image.Width));
        }
    }
}
