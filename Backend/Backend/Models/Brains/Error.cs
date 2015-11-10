using System.Windows;

namespace Backend.Models.Brains
{

    public interface IError
    {
        void StdErr(string someerror);
    }

    public class Error : IError
    {
        public void StdErr(string err)
        {
            MessageBox.Show(err, "Error",MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
