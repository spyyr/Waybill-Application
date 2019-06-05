using System;
using System.Windows;

namespace WpfApp2
{
    public class WrongNumberOfRowsException : Exception
    {
        public WrongNumberOfRowsException(string message)
        {
            PrintException(message);
        }

        private void PrintException(string message)
        {
            MessageBox.Show(message);
        }
        
    }
}