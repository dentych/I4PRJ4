using Backend.Models.Datamodels;
using SharedLib.Models;

namespace Backend.Models.Brains
{
    public interface IModelHandler
    {
        string LastError { get; }
#region Products
        bool CreateProduct(BackendProduct product);
        bool EditProduct(BackendProduct product);
        bool DeleteProduct(Product product);
        #endregion
#region Categories
        bool EditCategory(BackendProductCategory category);
        bool AddCategory(BackendProductCategory category);
        bool DeleteCategory(BackendProductCategory category);
        bool MoveProductsInCategory(BackendProductCategory categoryToEmpty, int catId);
        #endregion

    } //end IAddProduct
}