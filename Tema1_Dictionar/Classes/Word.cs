using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Tema1_Dictionar.Classes
{
    public partial class Word
    {
        public string content { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string imagePath { get; set; }


        public Word() 
        {
            content = string.Empty;
            description = string.Empty;
            category = string.Empty;
            imagePath = string.Empty;
        }

        public Word(string content, string category, string description, string imagePath)
        {
            this.content = content;
            this.category = category;
            this.description = description;

            if (imagePath == string.Empty)
            {
                this.imagePath = "default.jpg";
            }
            else
            {
                this.imagePath = imagePath;
            }
        }

        public BitmapImage LoadImage(string imagePath)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(basePath, "Resources", "Images", imagePath);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(path);
            bitmapImage.EndInit();

            return bitmapImage;
        }

    }
}
