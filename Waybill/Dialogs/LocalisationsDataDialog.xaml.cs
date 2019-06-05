using System.Data;
using System.Windows;
using System.Windows.Data;
using WpfApp2.CustomDialogs;
using WpfApp2.FormatkaDataSetTableAdapters;
using WpfApp2.Services;

namespace WpfApp2.Dialogs
{
    /// <summary>
    /// Interaction logic for Dialogo.xaml
    /// </summary>
    public partial class LocalisationsDataDialog : Window
    {
        private FormatkaDataSet formatkaDataSet;
        private LocalisationsTableAdapter adapter;
        private CollectionViewSource localisationsViewSource;
        public LocalisationsDataDialog()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            formatkaDataSet = (FormatkaDataSet)(this.FindResource("formatkaDataSet"));
            adapter = new LocalisationsTableAdapter();
            adapter.Fill(formatkaDataSet.Localisations);
            localisationsViewSource = ((CollectionViewSource)(this.FindResource("localisationsViewSource")));
            localisationsViewSource.View.MoveCurrentToFirst();
        }
        private void UpdateView()
        {
            adapter.Update(formatkaDataSet.Localisations);
            adapter.Fill(formatkaDataSet.Localisations);

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
            AddLocalisationDialog addLocalisationDialog = new AddLocalisationDialog();
            if (addLocalisationDialog.ShowDialog() == false && addLocalisationDialog.IsAddClicked == true) // getting info of city and zipcode from dialog and assigning it with already get street to LocalisationModel
            {
                LocalisationManager.AddLocalisation(new LocalisationModel()
                {
                    City = addLocalisationDialog.City,
                    Street = addLocalisationDialog.Street,
                    ZipCode = addLocalisationDialog.ZipCode
                });
            }
            UpdateView();
        }

        private void EditLocalisation_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)localisationsDataGrid.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Nie wybrano żadnej lokalizacji do edycji");
            }
            else
            {
                int localisationID = (int) dataRowView.Row[0];
                string street = (string) dataRowView.Row[1];
                string city = (string) dataRowView.Row[2];
                string zipCode = (string) dataRowView.Row[3];
                EditLocalisationDialog editLocalisationDialog = new EditLocalisationDialog(street, city, zipCode);
                if (editLocalisationDialog.ShowDialog() == false
                ) // getting info for edit when window closes to LocalisationModel
                {
                    LocalisationManager.EditLocalisation(new LocalisationModel()
                    {
                        LocalisationID = localisationID,
                        City = editLocalisationDialog.City,
                        Street = editLocalisationDialog.Street,
                        ZipCode = editLocalisationDialog.ZipCode
                    });
                }
                UpdateView();
            }
        }

        private void DeleteLocalisation_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView) localisationsDataGrid.SelectedItem;
            if (dataRowView == null)
            {
                MessageBox.Show("Nie wybrano żadnej lokalizacji do usunięcia");
            }
            else
            {
                int localisationID = (int) dataRowView.Row[0];
                LocalisationManager.DeleteLocalisation(localisationID);
                UpdateView();
            }
        }

        private void UpdateLocalisations_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = "xlsx",
                Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                LocalisationManager.UpdateLocalisations(fileDialog.FileName);
            }
        }
    }
    }

