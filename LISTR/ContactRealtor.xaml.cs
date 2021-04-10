using System.Web.UI.WebControls;
using System.Windows.Controls.Primitives;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class ContactRealtor : Popup
    {
        private readonly Browsing browsing;

        private readonly myLISTR myLISTR;
        private readonly myLISTRSeen myLISTRSeen;
        private readonly myLISTRDislike myLISTRDislike;



        private const string defaultText = "Hello";

        public ContactRealtor(Browsing browsing)
        {
            InitializeComponent();
            this.browsing = browsing;
        }


        public ContactRealtor(myLISTR myLISTR)
        {
            InitializeComponent();
            this.myLISTR = myLISTR;
        }

        public ContactRealtor(myLISTRSeen myLISTRSeen)
        {
            this.myLISTRSeen = myLISTRSeen;
        }

        public ContactRealtor(myLISTRDislike myLISTRDislike)
        {
            this.myLISTRDislike = myLISTRDislike;
        }





        private void SendClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Username.Text) || string.IsNullOrWhiteSpace(Password1.Password) || string.IsNullOrWhiteSpace(myTextBox.Text))
            {
                RegisterError.Text = "Please fill out at least your name, \r\n e-mail, and provide a message";
                return;
            }
            else
            {
                this.IsOpen = false;
            }

        }

        private void CancelClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsOpen = false;
        }



    }
}