using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpenditureOne.DAL.Entities
{
    public class Category : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
        public /*virtual*/ ICollection<ExpenditureCategory> ExpenditureCategory { get; set; }
        public /*virtual*/ List<Expenditure> Expendituries { get; set; }

    }
}
