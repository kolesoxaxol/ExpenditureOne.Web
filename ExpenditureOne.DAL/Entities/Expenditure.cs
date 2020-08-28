using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenditureOne.DAL
{
    public class Expenditure
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateOfExpenditure { get; set; }

        public string Description { get; set; }

        public string Title { get; set; } 

        public virtual ICollection<ExpenditureCategory> Categories { get; set; }
    }
}
