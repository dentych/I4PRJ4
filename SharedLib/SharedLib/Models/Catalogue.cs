using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Models
{   
    /// <summary>
    /// Datamodel for the productcatalogue
    /// </summary>
    public class Catalogue
    {
        /// <summary>
        /// List which holds the several ProductCategory objects which make up the productcatalogue.
        /// </summary>
        public readonly List<ProductCategory> ProductCategories = new List<ProductCategory>();
    }
}
