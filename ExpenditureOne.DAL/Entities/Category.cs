using System.Collections.Generic;

namespace ExpenditureOne.DAL.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }

        public ICollection<Expenditure> Expenditures { get; set; }
    }
}
