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
            RenderImage(Convert.ToInt16(canvasHeight), Convert.ToInt16(canvasWidth));
        }

        private void RenderImage(int height, int width)
        {
            displayImage.Height = height;
            displayImage.Width = width;
        }
    }
}
