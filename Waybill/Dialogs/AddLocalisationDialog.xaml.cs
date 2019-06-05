using System.Windows;


namespace WpfApp2.CustomDialogs
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class AddLocalisationDialog : Window
    {
        public string Street { get { return street.Text; } }
        public string City { get { return city.Text; } }
        public string ZipCode { get { return zipCode.Text; } }
        public bool IsAddClicked { get; set; } = false;

        public AddLocalisationDialog()
        {
            InitializeComponent();
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

        private void AddLocalisation_Click(object sender, RoutedEventArgs e)
        {
            IsAddClicked = true;
            Close();

        }
    }
}
    