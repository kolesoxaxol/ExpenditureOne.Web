using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenditureOne.DAL
{
    public interface IExpenditureInitializer
    {
        void Initialize(ExpenditureContext context);
    }
}
