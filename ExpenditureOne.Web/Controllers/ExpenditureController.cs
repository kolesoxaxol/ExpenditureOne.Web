using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenditureOne.BL;
using ExpenditureOne.BL.Expenditure;
using ExpenditureOne.DAL;
using ExpenditureOne.DAL.Entities;
using ExpenditureOne.Web.Enums;
using ExpenditureOne.Web.Models;
using ExpenditureOne.Web.Models.Expenditure;
using ExpenditureOne.Web.Responses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenditureOne.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenditureController : ControllerBase
    {
        public readonly IExpenditureService _expenditureService;
        public readonly IExpenditureService2 _expenditureService2;
        public readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ExpenditureController(IMapper mapper, IExpenditureService expenditureService, IExpenditureService2 expenditureService2, ICategoryService categoryService)
        {
            _expenditureService2 = expenditureService2;
            _expenditureService = expenditureService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<ExpenditureModel>>> Get()
        {
            //var expenditureBL = await _expenditureService.GetAll();
            //var expenditures = _mapper.Map<IEnumerable<ExpenditureModel>>(expenditureBL);

            var expenditureBL = await _expenditureService2.GetAll();
            var expenditures = _mapper.Map<IEnumerable<ExpenditureModel>>(expenditureBL);

            var result = new BaseResponse<IEnumerable<ExpenditureModel>>
            {
                Data = expenditures
            };

            return result;
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
            var isExpenditureExist = await _expenditureService2.CheckIfExists(expenditureRequest.Id);

            if (!isExpenditureExist)
            {
                return new BaseResponse { Code = ErrorCodes.NotFound, Message = $"Can't find item with id= {expenditureRequest.Id}" };
            }
            
            var expenditure = _mapper.Map<ExpenditureBL>(expenditureRequest);

            await _expenditureService2.Update(expenditure);

            return new BaseResponse<ExpenditureBL> { Data = expenditure };
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
            var isExpenditureExist = await _expenditureService2.CheckIfExists(id);

            if (!isExpenditureExist)
            {
                return new BaseResponse { Code = ErrorCodes.NotFound, Message = $"Can't find item with id= {id}" };
            }

            await _expenditureService2.Delete(id);

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
