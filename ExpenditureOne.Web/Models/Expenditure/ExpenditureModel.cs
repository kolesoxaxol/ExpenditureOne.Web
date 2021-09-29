using ExpenditureOne.Web.Responses.Models;
using System;
using System.Collections.Generic;

namespace ExpenditureOne.Web.Models.Expenditure
{
    public class ExpenditureModel
    {
        public int Id { get; set; }

        public DateTime DateOfExpenditure { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }
    }
}
