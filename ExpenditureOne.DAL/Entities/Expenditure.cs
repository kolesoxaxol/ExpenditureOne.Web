using System;
using System.Collections.Generic;

namespace ExpenditureOne.DAL
{
    public class Expenditure
    {
        public int Id { get; set; }

        public DateTime DateOfExpenditure { get; set; }

        public string Description { get; set; }

        public string Title { get; set; } 

        public List<ExpenditureCategory> Categories { get; set; }
    }
}
