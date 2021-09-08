using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExpenditureOne.BL;
using ExpenditureOne.BL.Expenditure;
using ExpenditureOne.Web.Enums;
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
        public readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ExpenditureController(IMapper mapper, IExpenditureService expenditureService, ICategoryService categoryService)
        {
            _expenditureService = expenditureService;
            _categoryService = categoryService;
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

        [HttpGet("{id}")]
        public async Task<BaseResponse<ExpenditureModel>> Get(int id)
        {
            var expenditureBL = await _expenditureService.FindById(id);

            if (expenditureBL == null)
            {
                return new BaseResponse<ExpenditureModel> { Code = ErrorCodes.Success, Message = $"Can't find category with id = {id}" };
            }

            var expenditure = _mapper.Map<ExpenditureModel>(expenditureBL);

            return new BaseResponse<ExpenditureModel> { Data = expenditure };
        }

        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(ExpenditureEditRequest expenditureRequest)
        {
            var isExpenditureExist = await _expenditureService.CheckIfExists(expenditureRequest.Id);

            if (!isExpenditureExist)
            {
                return new BaseResponse { Code = ErrorCodes.NotFound, Message = $"Can't find item with id= {expenditureRequest.Id}" };
            }

            var category = _mapper.Map<ExpenditureBL>(expenditureRequest);

            await _expenditureService.Update(category);

            return new BaseResponse();
        }

        [HttpPost]
        public async Task<BaseResponse<ExpenditureModel>> Post(ExpenditureRequest expenditureRequest)
        {
            var expenditureBL = _mapper.Map<ExpenditureBL>(expenditureRequest);
            expenditureBL = await _expenditureService.Create(expenditureBL);

            var response = _mapper.Map<ExpenditureModel>(expenditureBL);
            return new BaseResponse<ExpenditureModel> { Data = response };
        }

        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(int id)
        {
            var isExpenditureExist = await _expenditureService.CheckIfExists(id);

            if (!isExpenditureExist)
            {
                return new BaseResponse { Code = ErrorCodes.NotFound, Message = $"Can't find item with id= {id}" };
            }

            await _expenditureService.Delete(id);

            return new BaseResponse { Code = ErrorCodes.Success, Message = $"Deletet item with id={id}" };
        }


        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<BaseResponse<IEnumerable<ExpenditureModel>>> GetByCategory(int categoryId)
        {
            var expenditureBL = await _expenditureService.GetAll();
            var expenditures = _mapper.Map<IEnumerable<ExpenditureModel>>(expenditureBL);

            return new BaseResponse<IEnumerable<ExpenditureModel>>
            {
                Data = expenditures
            };
        }
    }
}
