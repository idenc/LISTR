using System.Windows.Controls.Primitives;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Popup
    {
        private readonly HomePage homePage;

        public Register(HomePage homePage)
        {
            InitializeComponent();
            this.homePage = homePage;
        }

        private void RegisterClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Username.Text) || string.IsNullOrWhiteSpace(Password1.Password) || string.IsNullOrWhiteSpace(Password2.Password))
            {
                RegisterError.Text = "Please fill out all fields!";
                return;
            }

            if (Password1.Password != Password2.Password)
            {
                RegisterError.Text = "Passwords do not match!";
                return;
            }

            var filter = Builders<BsonDocument>.Filter.Eq("username", Username.Text);
            var search = MainWindow.accounts.Find(filter).FirstOrDefault();
            if (search != null)
            {
                RegisterError.Text = "Username is taken. Please enter a different username.";
                return;
            }

            var document = new BsonDocument {
                { "username", Username.Text},
                { "password", Password1.Password},
                { "is_realtor", false} // Realtors must be manually marked as such
            };

            MainWindow.accounts.InsertOne(document);
            this.homePage.DoLogin(false);
            this.IsOpen = false;
        }
    }
}