using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExpenditureOne.BL;
using ExpenditureOne.BL.Expenditure;
using ExpenditureOne.Web.Models;
using ExpenditureOne.Web.Models.Expenditure;
using Microsoft.AspNetCore.Mvc;

namespace ExpenditureOne.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenditureController : ControllerBase
    {
        public readonly IExpenditureService _expenditureService;
        private readonly IMapper _mapper;

        public ExpenditureController(IMapper mapper, IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<ExpenditureModel>>> Get()
        {
            var expenditureBL = await _expenditureService.GetAll();
            var expenditures = _mapper.Map<IEnumerable<ExpenditureModel>>(expenditureBL);

            return new BaseResponse<IEnumerable<ExpenditureModel>>
            {
                Data = expenditures
            };
        }

        [HttpPost]
        public async Task<BaseResponse<ExpenditureModel>> Post(ExpenditureRequest expenditureRequest)
        {
            var expenditureBL = _mapper.Map<ExpenditureBL>(expenditureRequest);
            expenditureBL = await _expenditureService.Create(expenditureBL);

            var response = _mapper.Map<ExpenditureModel>(expenditureBL);
            return new BaseResponse<ExpenditureModel> { Data = response };
        }
    }
}
