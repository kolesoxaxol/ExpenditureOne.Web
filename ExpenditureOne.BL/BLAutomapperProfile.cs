using ExpenditureOne.BL.Models;
using ExpenditureOne.DAL.Entities;
using AutoMapper;

namespace ExpenditureOne.BL
{
    public class BLAutomapperProfile : Profile
    {
        public BLAutomapperProfile()
        {
            CreateMap<Category, CategoryBL>();
        }
    }
}
