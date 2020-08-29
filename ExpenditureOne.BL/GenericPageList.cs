using System.Collections.Generic;

namespace ExpenditureOne.BL.Models
{
    public class GenericPagedList<ModelBL>
       where ModelBL : class
    {
        public IEnumerable<ModelBL> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
