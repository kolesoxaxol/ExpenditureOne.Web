using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenditureOne.DAL
{
    public class ExpenditureInitializer : IExpenditureInitializer
    {
        public void Initialize(ExpenditureContext context)
        {
            context.Database.EnsureCreated();

          //  context.Expenditures.Add();

            context.SaveChanges();
        }
    }
}
