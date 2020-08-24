using System;

namespace ExpenditureOne.DAL
{
    public class ExpenditureCategory
    {
        public int CategoryId { get; set; }

        public int ExpenditureId { get; set; }

        public Category Category { get; set; }

        public Expenditure Expenditure { get; set; }
    
    }
}
