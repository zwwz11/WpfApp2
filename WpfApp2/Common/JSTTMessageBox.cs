using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2.Common
{
    public class JSTTMessageBox
    {
        public static MessageBoxResult Show(string message)
        {
            return MessageBox.Show(message, "알림");
        }
    }
}
