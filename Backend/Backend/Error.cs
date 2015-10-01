using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Backend
{
    class Error
    {
        public static void StdErr(string err)
        {
            MessageBox.Show(err);
        }
    }
}
