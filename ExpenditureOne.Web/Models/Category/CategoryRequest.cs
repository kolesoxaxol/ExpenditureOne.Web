using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenditureOne.Web.Models.Category
{
    public class CategoryRequest
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
    }
}
