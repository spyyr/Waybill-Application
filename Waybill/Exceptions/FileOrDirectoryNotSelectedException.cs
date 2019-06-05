using System;
using System.Windows;

namespace WpfApp2
{
    public class FileOrDirectoryNotSelectedException : Exception
    {
        public FileOrDirectoryNotSelectedException(string message)
        {
            PrintException(message);
        }

        private void PrintException(string message)
        {
            MessageBox.Show(message);
        }
    }
}