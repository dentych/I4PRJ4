using SharedLib.Models;

namespace Backend.Models.Events
{
    public class EditProductParameters
    {
        public BackendProductCategoryList cats;
        public Product product;
        public BackendProductCategory CurrentCategory;
        public int currentCatIndex;
    }

    public class EditCategoryParms
    {
        public string Name;
        public int Id;
        public BackendProductCategoryList cats;
    }

}