using ExpenditureOne.BL.Models;
using ExpenditureOne.DAL.Entities;
using ExpenditureOne.BL.Expenditure;
using AutoMapper;



namespace ExpenditureOne.BL
{
    public class BLAutomapperProfile : Profile
    {
        public BLAutomapperProfile()
        {
            CreateMap<Category, CategoryBL>().ReverseMap();
            CreateMap<DAL.Entities.Expenditure, ExpenditureBL>().ReverseMap();
        }
    }
}
