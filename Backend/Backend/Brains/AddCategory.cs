using System.Windows;
using Backend.Models;

namespace Backend.Brains
{
    class AddCategory : IAddCategory
    {
        public void CreateCategory(BackendProductCategory toCrate)
        {
            MessageBox.Show("CreateCommandCalled");
        }
    }
}