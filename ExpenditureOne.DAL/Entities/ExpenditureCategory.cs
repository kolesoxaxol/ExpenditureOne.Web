using ExpenditureOne.DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenditureOne.DAL
{

    public class ExpenditureCategory : IEntity
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int ExpenditureId { get; set; }

        public Category Category { get; set; }

        public Expenditure Expenditure { get; set; }
    
    }
}
