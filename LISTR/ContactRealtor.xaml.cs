using System.Windows.Controls.Primitives;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class ContactRealtor : Popup
    {
        private readonly Browsecontrol browsing;
        private const string defaultText = "Hello";

        public ContactRealtor(Browsecontrol browsing)
        {
            InitializeComponent();
            this.browsing = browsing;
        }

        public ContactRealtor()
        {
            InitializeComponent();
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