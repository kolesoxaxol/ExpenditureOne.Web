using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenditureOne.DAL
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
        public virtual ICollection<ExpenditureCategory> Expenditures { get; set; }
    }
}
