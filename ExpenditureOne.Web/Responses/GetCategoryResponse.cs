using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenditureOne.Web.Responses
{
    public class GetCategoryResponse
    {
       public IEnumerable<GetCategoryModel> Categories { get; set; }
    }

    public class GetCategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
    }
}
