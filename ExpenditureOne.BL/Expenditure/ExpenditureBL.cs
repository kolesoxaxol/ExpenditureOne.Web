using ExpenditureOne.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenditureOne.BL.Expenditure
{
    public class ExpenditureBL
    {
        public int Id { get; set; }

        public DateTime DateOfExpenditure { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public ICollection<CategoryBL> Categories { get; set; }
    }
}
