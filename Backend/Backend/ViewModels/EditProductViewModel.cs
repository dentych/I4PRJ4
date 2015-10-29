using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Backend.Models;
using Backend.Models.Events;
using Prism.Events;
using SharedLib.Models;

namespace Backend.ViewModels
{
    class EditProductViewModel
    {
        public IEventAggregator Aggregator;
        public Product ProductToEdit { get; set; }
        public BackendProductCategory ProductCategory { get; set; }
        public Product EditedProduct { get; set; }
        public BackendProductCategoryList Categories { get; set; }
        public int currentCatIndex { get; set;  }

        public EditProductViewModel()
        {
            Aggregator = SingleEventAggregator.Aggregator;
            Aggregator.GetEvent<NewEditProductData>().Subscribe(ProductDataToEdit, true);
            Aggregator.GetEvent<EditProductWindowLoaded>().Publish(true);

        }

        public void ProductDataToEdit(EditProductParameters details)
        {
            ProductToEdit = details.product;
            ProductCategory = details.CurrentCategory;
            Categories = details.cats;
            currentCatIndex = details.currentCatIndex;

        }

    }
}
