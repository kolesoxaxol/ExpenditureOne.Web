using AutoMapper;
using ExpenditureOne.BL.Expenditure;
using ExpenditureOne.BL.Models;
using ExpenditureOne.DAL.Entities;
using ExpenditureOne.Web.Models.Category;
using ExpenditureOne.Web.Models.Expenditure;
using ExpenditureOne.Web.Responses.Models;

namespace ExpenditureOne.Web
{
    public class WebAutomapperProfile : Profile
    {
        public WebAutomapperProfile()
        {
            CreateMap<CategoryBL, CategoryModel>().ReverseMap();
            CreateMap<CategoryBL, CategoryCreateRequest>().ReverseMap();
            CreateMap<CategoryBL, CategoryEditRequest>().ReverseMap();

            CreateMap<ExpenditureBL, ExpenditureModel>().ReverseMap();
            CreateMap<ExpenditureBL, ExpenditureRequest>().ReverseMap();
            CreateMap<ExpenditureBL, ExpenditureEditRequest>().ReverseMap();

            CreateMap<Expenditure, ExpenditureModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}
