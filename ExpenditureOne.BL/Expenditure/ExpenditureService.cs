using AutoMapper;
using ExpenditureOne.BL.Expenditure;
using ExpenditureOne.DAL;


namespace ExpenditureOne.BL
{

    public interface IExpenditureService : IGenericService<ExpenditureBL>
    { 
    }
    public class ExpenditureService : GenericService<ExpenditureBL, DAL.Entities.Expenditure>, IExpenditureService
    {

        public ExpenditureService(IGenericRepository<DAL.Entities.Expenditure> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
