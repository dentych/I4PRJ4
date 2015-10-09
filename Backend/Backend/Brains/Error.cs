using System.Windows;

namespace Backend.Brains
{

    public interface IError
    {
        void StdErr(string someerror);
    }

    class Error : IError
    {
        public void StdErr(string err)
        {
            MessageBox.Show(err, "Error",MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
