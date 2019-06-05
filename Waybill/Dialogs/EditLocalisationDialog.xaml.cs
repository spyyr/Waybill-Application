using System.Windows;


namespace WpfApp2.Dialogs
{
    /// <summary>
    /// Interaction logic for EditLocalisationDialog.xaml
    /// </summary>
    public partial class EditLocalisationDialog : Window
    {
      
        public string Street { get { return street.Text; } }
        public string City { get { return city.Text; } }
        public string ZipCode { get { return zipCode.Text; } }

        public EditLocalisationDialog(string street, string city, string zipCode)
        {
            InitializeComponent();
            this.street.Text = street;
            this.city.Text = city;
            this.zipCode.Text = zipCode;
            CenterWindowOnScreen();
        }
        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void EditLocalisation_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
