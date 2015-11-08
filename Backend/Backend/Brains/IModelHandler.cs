
using Backend.Models;
using SharedLib.Models;

namespace Backend.Brains
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
#endregion

    } //end IAddProduct
}