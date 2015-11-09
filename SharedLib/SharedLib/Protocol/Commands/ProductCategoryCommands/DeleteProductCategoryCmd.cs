using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Models;

namespace SharedLib.Protocol.Commands.ProductCategoryCommands
{
    public class DeleteProductCategoryCmd: Command
    {
        private readonly string _name;
        private readonly int _productCategoryId;

        public string Name { get { return _name; } }
        public int ProductCategoryId { get { return _productCategoryId; } }

        public DeleteProductCategoryCmd(int productCategoryId)
        {
            _productCategoryId = productCategoryId;
        }

        public DeleteProductCategoryCmd(ProductCategory productCategory)
        {
            _name = productCategory.Name;
            _productCategoryId = productCategory.ProductCategoryId;
        }
    }
}
