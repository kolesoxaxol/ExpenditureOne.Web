using AutoMapper;
using ExpenditureOne.BL.Expenditure;
using ExpenditureOne.DAL;


namespace ExpenditureOne.BL
{

    public interface IExpenditureService : IGenericService<ExpenditureBL>
    { 
    }
    public class ExpenditureService : GenericService<ExpenditureBL, ExpenditureOne.DAL.Expenditure>, IExpenditureService
    {

        public ExpenditureService(IGenericRepository<ExpenditureOne.DAL.Expenditure> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
