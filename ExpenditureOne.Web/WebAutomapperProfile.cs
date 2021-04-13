using AutoMapper;
using ExpenditureOne.BL.Expenditure;
using ExpenditureOne.BL.Models;
using ExpenditureOne.Web.Models.Category;
using ExpenditureOne.Web.Models.Expenditure;
using ExpenditureOne.Web.Responses;
using ExpenditureOne.Web.Responses.Models;

namespace ExpenditureOne.Web
{
    public class WebAutomapperProfile : Profile
    {
        public WebAutomapperProfile()
        {
            CreateMap<CategoryBL, CategoryModel>().ReverseMap();
            CreateMap<CategoryBL, CategoryCreateRequest>().ReverseMap();
            CreateMap<ExpenditureBL, ExpenditureModel>().ReverseMap();
            CreateMap<ExpenditureBL, ExpenditureRequest>().ReverseMap();
            CreateMap<CategoryBL, CategoryEditRequest>().ReverseMap();
        }
    }
}
