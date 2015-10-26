using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class BackendProductCategoryList : ObservableCollection<BackendProductCategory>
    {
        public int CurrentIndex { get; set; } = -1;

        public BackendProductCategoryList()
        {

            // Load alle produkter
            for (int i = 0; i < 10; i++)
            {
                Add(new BackendProductCategory() {Name = "Category " + (i+1)});
            }
        }

    }
}
