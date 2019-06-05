using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using WpfApp2.Dialogs;

namespace WpfApp2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        /// <summary>
        /// Prevents user from inputing not valid data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowsPickerValidation(object sender, TextCompositionEventArgs e) 
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void SelectFirstFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = "xlsx", Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                selectFirstFileLabel.Content = fileDialog.FileName;
            }
        }

        private void SelectSecondFile_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = "xlsx", Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                selectSecondFileLabel.Content = fileDialog.FileName;
            }
        }

        private void SelectSavingDirectory_Click(object sender, RoutedEventArgs e)
        {
            var directoryDialog = new CommonOpenFileDialog();
            directoryDialog.Title = "folder zapisu";
            directoryDialog.IsFolderPicker = true;
            CommonFileDialogResult result = directoryDialog.ShowDialog();
            selectSavingDirectoryLabel.Content = directoryDialog.FileName;
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var firstFileName = selectFirstFileLabel.Content.ToString();
                var secondFileName = selectSecondFileLabel.Content.ToString();
                var savingDirectory = selectSavingDirectoryLabel.Content.ToString();
                
                if ((!firstFileName.EndsWith(".xlsx")  || !secondFileName.EndsWith(".xlsx") || !Directory.Exists(savingDirectory) ) )
                {
                    throw new FileOrDirectoryNotSelectedException("Folder lub jeden z plików został źle wybrany");
                }
                Console.WriteLine(savingDirectory);
                ExcelFile excelFile = new ExcelFile(firstFileName, secondFileName, savingDirectory, ExcelFile.ReadRows(rowsPicker.Text));
                MessageBox.Show("Zrobione");
            }
            catch (WrongNumberOfRowsException exception)
            {

            }
            catch (FileOrDirectoryNotSelectedException exception)
            {

            }
        }

        private void ShowLocalisations_Click(object sender, RoutedEventArgs e)
        {
            LocalisationsDataDialog dialog = new LocalisationsDataDialog();

        }

        private void ShowComputers_Click(object sender, RoutedEventArgs e)
        {
            ComputersDataDialog dialog = new ComputersDataDialog();
        }
    }
}
