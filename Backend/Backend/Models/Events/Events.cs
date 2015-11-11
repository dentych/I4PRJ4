using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Datamodels;
using Prism.Events;

namespace Backend.Models.Events
{
    public class CategoryListUpdated : PubSubEvent<BackendProductCategoryList>{ }
    public class NewEditProductData : PubSubEvent<EditProductParameters> { }
    public class NewEditCategoryData : PubSubEvent<EditCategoryParms> { }
    public class NewDeleteCategoryData : PubSubEvent<DeleteCategoryParms> { }



    public class AddProductWindowLoaded : PubSubEvent<bool> { }
    public class DeleteCategoryWindowLoaded : PubSubEvent<bool> { }
    public class EditCategoryWindowLoaded : PubSubEvent<bool> { }
    public class AddCategoryWindowLoaded : PubSubEvent<bool> { }
    public class EditProductWindowLoaded : PubSubEvent<bool> { }

}
