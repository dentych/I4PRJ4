using Backend.Models.Datamodels;
using SharedLib.Models;

namespace Backend.Models.Events
{
    /// <summary>
    /// Parameters for NewEditProductData.
    /// </summary>
    public class EditProductParameters
    {
        public BackendProductCategoryList cats;
        public Product product;
        public BackendProductCategory CurrentCategory;
        public int currentCatIndex;
    }

    /// <summary>
    /// Parameters for NewEditCategoryData.
    /// </summary>
    public class EditCategoryParms
    {
        public string Name;
        public int Id;
        public BackendProductCategoryList cats;
    }

    /// <summary>
    /// Parameters for NewDeleteCategoryData.
    /// </summary>
    public class DeleteCategoryParms
    {
        public int ToDelteIndex;
        public BackendProductCategoryList cats;
    }

}