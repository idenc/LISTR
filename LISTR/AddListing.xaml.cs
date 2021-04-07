﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text

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

        private bool MandatoryFieldsFilled()
        {
            return !(String.IsNullOrWhiteSpace(Owners.Text) || String.IsNullOrWhiteSpace(Price.Text) || String.IsNullOrWhiteSpace(Address.Text));
        }

        private void PostListing(object sender, RoutedEventArgs e)
        {
            if (!MandatoryFieldsFilled())
            {
                PostPopup.IsOpen = true;
                return;
            }
            House house = new House();
            house.Description = Description.Text;
            house.NumRooms = Bedrooms.Text;
            house.NumBaths = Bathrooms.Text;
            house.IsRental = ListingType.Text == "Rental";
            house.Owners = Owners.Text;
            house.Price = Double.Parse(Price.Text);
            house.Address = Address.Text;
            house.City = City.Text;
            house.Province = Province.Text;
            house.Area = Double.Parse(Area.Text);
            house.Tags = Tags.Text.Split(',').Select(s => s.Trim()).ToArray();
            if (Application.Current.Properties.Contains("Username"))
            {
                house.Realtor = Application.Current.Properties["Username"] as string;
            }

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
            MainWindow.houses.Add(house);
            GoToListings();
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(imageIn.Source as BitmapImage));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        private void CheckNumbers(object sender, TextCompositionEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e.Text))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = _regex.IsMatch(e.Text);
            }
        }

        private void GoToListings()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Main.Navigate(new RealtorListings());
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            GoToListings();
        }
    }
}