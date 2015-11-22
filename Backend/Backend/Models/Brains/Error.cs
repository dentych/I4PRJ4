using System.Windows;

namespace Backend.Models.Brains
{
    /// <summary>
    /// Error interface.
    /// </summary>
    public interface IError
    {
        void StdErr(string someerror);
    }

    /// <summary>
    /// Class to show an error popup.
    /// </summary>
    public class Error : IError
    {
        /// <summary>
        /// Show an error message box.
        /// </summary>
        /// <param name="err">The text to show in the message box.</param>
        public void StdErr(string err)
        {
            MessageBox.Show(err, "Error",MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
