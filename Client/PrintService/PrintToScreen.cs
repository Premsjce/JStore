using System.Windows;

namespace Client.PrintService
{
    public class PrintToScreen : IPrint
    {
        public void Print(string message)
        {
            MessageBox.Show(message);
        }
    }
}