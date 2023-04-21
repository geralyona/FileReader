using FileReader.Interfaces.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileReader.UI
{
    public class Messager : IMessager
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
