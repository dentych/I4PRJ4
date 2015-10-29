using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace Backend.Models.Events
{
    public class CategoryListUpdated : PubSubEvent<BackendProductCategoryList>{ }
    public class NewEditProductData : PubSubEvent<EditProductParameters> { }

    public class AddProductWindowLoaded : PubSubEvent<bool> { }
    public class EditProductWindowLoaded : PubSubEvent<bool> { }

}
