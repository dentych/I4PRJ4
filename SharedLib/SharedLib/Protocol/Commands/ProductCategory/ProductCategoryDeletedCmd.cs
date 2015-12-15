using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands.ProductCategoryCommands
{
    /// <summary>
    /// Information that a ProductCategory has been deleted.
    /// </summary>
    public class ProductCategoryDeletedCmd: Command
    {
        private readonly string _name;
        private readonly int _productCategoryId;

        /// <summary>
        /// Name of the ProductCategory
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// ProductCategoryId of the ProductCategory.
        /// </summary>
        public int ProductCategoryId { get { return _productCategoryId; } }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productCategoryId">ProductCategoryId of the ProductCategory that is to be deleted.</param>
        public ProductCategoryDeletedCmd(int productCategoryId)
        {
            _productCategoryId = productCategoryId;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productCategory">ProductCategory which attributes is to be copied into the command.</param
        public ProductCategoryDeletedCmd(ProductCategory productCategory)
        {
            _name = productCategory.Name;
            _productCategoryId = productCategory.ProductCategoryId;
        }
    }
}
