using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaPlayer.Others
{
    public static class ImageSourcer
    {
        //the folder inside the application namespace where images are located
        private static string IMAGES = "pack://application:,,,/Images/";
        //combine the image path with it's name and termonation
        private static string BuildImagePath(string name, string state)
        {
            return IMAGES + name + state + ".png";
        }
        //return the image with sufix _normal
        public static ImageSource GetNormal(string name)
        {
            return new BitmapImage(new Uri(BuildImagePath(name, "_normal")));
        }
        //return the image with sufix _hover
        public static ImageSource GetHover(string name)
        {
            return new BitmapImage(new Uri(BuildImagePath(name, "_hover")));
        }
        //get sound button
        public static ImageSource GetSound(bool state)
        {
            if (state)
            {
                return new BitmapImage(new Uri(BuildImagePath("sound", "_off")));
            }
            else
            {
                return new BitmapImage(new Uri(BuildImagePath("sound", "_on")));
            }
        }
    }
}
