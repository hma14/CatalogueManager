using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{

    public  class SubCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public List<Product> ProdList { get; set; }
    }
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId{ get; set; }
        public string ParentCategoryName { get; set; }
        public List<SubCategory> CategoryList { get; set; }
    }

}