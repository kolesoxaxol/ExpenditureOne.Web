using AutoMapper;
using ExpenditureOne.BL.Models;
using ExpenditureOne.Web.Responses;
using ExpenditureOne.Web.Responses.Models;

namespace ExpenditureOne.Web
{
    public class WebAutomapperProfile : Profile
    {
        public WebAutomapperProfile()
        {
            CreateMap<CategoryBL, GetCategoryModel>().ReverseMap();
        }
    }
}
