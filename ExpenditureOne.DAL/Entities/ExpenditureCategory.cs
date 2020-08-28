using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenditureOne.DAL
{
   
    public class ExpenditureCategory
    {
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("Expenditure")]
        public int ExpenditureId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Expenditure Expenditure { get; set; }
    
    }
}
