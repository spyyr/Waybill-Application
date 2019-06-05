using System.Windows;


namespace WpfApp2.Dialogs
{
    /// <summary>
    /// Interaction logic for AddComputerDialog.xaml
    /// </summary>
    public partial class AddComputerDialog : Window
    {
        public string ModelName { get { return modelName.Text; } }
        public int Price { get { return int.Parse(price.Text); } }
        public int Weight { get { return int.Parse(weight.Text); } }
        public bool HasAdapter { get { return CheckValue(); } }
        public bool IsAddClicked { get; set; } = false;

        public AddComputerDialog()
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
        private void AddComputer_Click(object sender, RoutedEventArgs e)
        {
            IsAddClicked = true;
            Close();
        }

        private bool CheckValue()
        {
            if (hasAdapter.IsChecked == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
