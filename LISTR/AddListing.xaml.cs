using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for AddListing.xaml
    /// </summary>
    public partial class AddListing : Page
    {
        private String defaultpath = "";
        private int numImages = 0;

        public AddListing()
        {
            InitializeComponent();
        }

        private void SelectPhotos(object sender, MouseButtonEventArgs e)
        {
            if (defaultpath.Equals(""))
            {
                defaultpath = img1.Source.ToString();
            }

            Microsoft.Win32.OpenFileDialog dialogue = new Microsoft.Win32.OpenFileDialog();

            // set valid file extensions
            dialogue.DefaultExt = ".png";
            dialogue.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // display the open file dialogue
            Nullable<bool> result = dialogue.ShowDialog();

            if (result == true)
            {
                string filename = dialogue.FileName; ;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filename);
                bitmap.EndInit();
                if (img1.Source.ToString().Contains("default.png"))
                {
                    img1.Source = bitmap;
                    img1_label.Visibility = Visibility.Visible;
                }
                else if (img2.Source.ToString().Contains("default.png"))
                {
                    img2.Source = bitmap;
                    img2_label.Visibility = Visibility.Visible;
                }
                else if (img3.Source.ToString().Contains("default.png"))
                {
                    img3.Source = bitmap;
                    img3_label.Visibility = Visibility.Visible;
                }
                else if (img4.Source.ToString().Contains("default.png"))
                {
                    img4.Source = bitmap;
                    img4_label.Visibility = Visibility.Visible;
                }
                else if (img5.Source.ToString().Contains("default.png"))
                {
                    img5.Source = bitmap;
                    img5_label.Visibility = Visibility.Visible;
                }
                numImages++;
            }
        }

        private void ImgMouseDown(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;
            Image image = FindName(label.Name.Substring(0, label.Name.Length - 6)) as Image;
            image.Source = new BitmapImage(new Uri(@defaultpath));
            label.Visibility = Visibility.Hidden;
            numImages--;
        }

        private void PostListing(object sender, RoutedEventArgs e)
        {
            House house = new House();
            house.Description = Description.Text;
            byte[][] images = new byte[numImages][];
            int count = 0;
            for (int i = 1; i <= 5; i++)
            {
                Image image = FindName("img" + i) as Image;
                if (!image.Source.ToString().Contains("default.png"))
                {
                    images[count++] = ImageToByteArray(image);
                }
            }
            house.Images = images;

            MainWindow.houseCollection.InsertOne(house);
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageIn.Source as BitmapImage));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
    }
}