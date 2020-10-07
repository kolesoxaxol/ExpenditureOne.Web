using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenditureOne.Web.Models.Expenditure
{
    public class ExpenditureRequest
    {
        public int Id { get; set; }
        public DateTime DateOfExpenditure { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

       // public  ICollection<ExpenditureCategory> Categories { get; set; }
    }
}
