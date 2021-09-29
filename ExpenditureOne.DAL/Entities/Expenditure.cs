using System;
using System.Collections.Generic;

namespace ExpenditureOne.DAL.Entities
{
    public class Expenditure : IEntity
    {
        public int Id { get; set; }

        public DateTime DateOfExpenditure { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
