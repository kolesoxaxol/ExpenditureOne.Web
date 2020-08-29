using AutoMapper;
using ExpenditureOne.BL.Models;
using ExpenditureOne.DAL;
using ExpenditureOne.DAL.Entities;

namespace ExpenditureOne.BL
{

    public interface ICategoryService : IGenereicService<CategoryBL>
    {

    }
    public class CategoryService : GenericService<CategoryBL, Category>, ICategoryService
    {
        public CategoryService(IGenericRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
