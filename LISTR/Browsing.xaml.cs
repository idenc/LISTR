using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Browsing.xaml
    /// </summary>
    public partial class Browsing : Page
    {
        private MainWindow mainWindow;
        public static House SampleHouse = MainWindow.houses.AsQueryable().ToList()[0];

        public Browsing()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
            this.DataContext = SampleHouse;
        }
    }
}
