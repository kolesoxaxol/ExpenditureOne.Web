using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenditureOne.DAL.Entities
{
    public class Expenditure : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateOfExpenditure { get; set; }

        public string Description { get; set; }

        public string Title { get; set; } 

        public /*virtual*/ ICollection<ExpenditureCategory> ExpenditureCategory { get; set; }
        public /*virtual*/ List<Category> Categoryies { get; set; }
    }
}
