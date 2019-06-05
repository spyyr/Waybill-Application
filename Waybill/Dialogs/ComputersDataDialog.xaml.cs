using System.Data;
using System.Windows;
using System.Windows.Data;
using WpfApp2.FormatkaDataSetTableAdapters;
using WpfApp2.Services;

namespace WpfApp2.Dialogs
{
    /// <summary>
    /// Interaction logic for Dialogo.xaml
    /// </summary>
    public partial class ComputersDataDialog : Window
    {
        private FormatkaDataSet formatkaDataSet;
        private ComputersTableAdapter adapter;
        private CollectionViewSource computersViewSource;

        public ComputersDataDialog()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            formatkaDataSet = (FormatkaDataSet)(this.FindResource("formatkaDataSet"));
            adapter = new ComputersTableAdapter();
            adapter.Fill(formatkaDataSet.Computers);
            computersViewSource = ((CollectionViewSource)(this.FindResource("computersViewSource")));
            computersViewSource.View.MoveCurrentToFirst();
        }

        private void UpdateView()
        {
            adapter.Update(formatkaDataSet.Computers);
            adapter.Fill(formatkaDataSet.Computers);

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
            AddComputerDialog addComputerDialog = new AddComputerDialog();
            if (addComputerDialog.ShowDialog() == false && addComputerDialog.IsAddClicked == true) // getting info of city and zipcode from dialog and assigning it with already get street to LocalisationModel
            {
                ComputerManager.AddComputer(new ComputerModel()
                {
                    ModelName = addComputerDialog.ModelName,
                    Price = addComputerDialog.Price,
                    Weight = addComputerDialog.Weight,
                    HasAdapter = addComputerDialog.HasAdapter
                });
            }
            UpdateView();
        }

        
        private void EditComputer_Click(object sender, RoutedEventArgs e)
        {
             DataRowView dataRowView = (DataRowView)computersDataGrid.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Nie wybrano żadnej lokalizacji do edycji");
            }
            else
            {
                int computerID = (int) dataRowView.Row[0];
                string modelName = (string) dataRowView.Row[1];
                int price = (int) dataRowView.Row[4];
                int weight = (int) dataRowView.Row[2];
                bool hasAdapter = (bool)dataRowView.Row[3];
                EditComputerDialog editLocalisationDialog = new EditComputerDialog(modelName, price, weight, hasAdapter);
                if (editLocalisationDialog.ShowDialog() == false
                ) // getting info for edit when window closes to LocalisationModel
                {
                    ComputerManager.EditComputer(new ComputerModel()
                    {
                        ComputerID = computerID,
                        ModelName = editLocalisationDialog.ModelName,
                        Price = editLocalisationDialog.Price,
                        Weight = editLocalisationDialog.Weight,
                        HasAdapter = editLocalisationDialog.HasAdapter
                    });
                }
                UpdateView();
            }
        }

        private void DeleteComputer_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView) computersDataGrid.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Nie wybrano żadnego modelu komputera do usunięcia");
            }
            else
            {
                int computerID = (int) dataRowView.Row[0];
                ComputerManager.DeleteComputer(computerID);
                UpdateView();
            }
        }
        
    } 
    }


