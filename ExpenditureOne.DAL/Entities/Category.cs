using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenditureOne.DAL
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
        public List<ExpenditureCategory> Expenditures { get; set; }
    }
}
