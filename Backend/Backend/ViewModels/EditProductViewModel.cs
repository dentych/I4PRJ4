using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models;
using SharedLib.Models;

namespace Backend.ViewModels
{
    class EditProductViewModel
    {
        public EditProductViewModel(Product toe, BackendProductCategory cat, BackendProductCategoryList catlist)
        {
            ProductToEdit = toe;
            ProductCategory = cat;
            CatList = catlist;

        }

        public Product ProductToEdit { get; set; }
        public BackendProductCategory ProductCategory { get; set; }
        public Product EditedProduct { get; set; }
        public BackendProductCategoryList CatList { get; set; }

    }
}
