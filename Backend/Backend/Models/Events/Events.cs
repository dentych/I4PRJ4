using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Models.Datamodels;
using Prism.Events;

namespace Backend.Models.Events
{
    /// <summary>
    /// Event for category list updated.
    /// </summary>
    public class CategoryListUpdated : PubSubEvent<BackendProductCategoryList>{ }
    /// <summary>
    /// Event for edit product
    /// </summary>
    public class NewEditProductData : PubSubEvent<EditProductParameters> { }
    /// <summary>
    /// Event for edit category.
    /// </summary>
    public class NewEditCategoryData : PubSubEvent<EditCategoryParms> { }
    /// <summary>
    /// Event for delete category.
    /// </summary>
    public class NewDeleteCategoryData : PubSubEvent<DeleteCategoryParms> { }

    /// <summary>
    /// Event for add product window loaded.
    /// </summary>
    public class AddProductWindowLoaded : PubSubEvent<bool> { }
    /// <summary>
    /// Event for delete category window loaded.
    /// </summary>
    public class DeleteCategoryWindowLoaded : PubSubEvent<bool> { }
    /// <summary>
    /// Event for edit category window loaded.
    /// </summary>
    public class EditCategoryWindowLoaded : PubSubEvent<bool> { }
    /// <summary>
    /// Event for add category window loaded.
    /// </summary>
    public class AddCategoryWindowLoaded : PubSubEvent<bool> { }
    /// <summary>
    /// Event for edit product window laoded.
    /// </summary>
    public class EditProductWindowLoaded : PubSubEvent<bool> { }

}
