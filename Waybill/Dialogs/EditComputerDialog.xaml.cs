using System.Windows;

namespace WpfApp2.Dialogs
{
    /// <summary>
    /// Logika interakcji dla klasy EditCOmputersDialog.xaml
    /// </summary>
    public partial class EditComputerDialog : Window
    {
        public string ModelName { get { return modelName.Text; } }
        public int Price { get { return int.Parse(price.Text); } }
        public int Weight { get { return int.Parse(weight.Text); } }
        public bool HasAdapter { get { return CheckValue(); } }

        public EditComputerDialog(string modelName, int price, int weight, bool hasAdapter)
        {
            InitializeComponent();
            this.modelName.Text = modelName;
            this.price.Text = price.ToString();
            this.weight.Text = weight.ToString();
            this.hasAdapter.IsChecked = hasAdapter;
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

        private void EditComputer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
