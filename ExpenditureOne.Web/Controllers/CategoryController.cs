using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExpenditureOne.BL;
using ExpenditureOne.BL.Models;
using ExpenditureOne.Web.Enums;
using ExpenditureOne.Web.Models;
using ExpenditureOne.Web.Responses;
using ExpenditureOne.Web.Responses.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenditureOne.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        public readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<BaseResponse<IEnumerable<GetCategoryModel>>> Get()
        {
            var categoriesBL = await _categoryService.GetAll();
            var categories = _mapper.Map<IEnumerable<GetCategoryModel>>(categoriesBL);

            return new BaseResponse<IEnumerable<GetCategoryModel>>
            {
                Data = categories
            };
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<BaseResponse<GetCategoryModel>> Get(int id)
        {
            var categoryBL = await _categoryService.FindById(id);

            if (categoryBL == null)
            {
                return new BaseResponse<GetCategoryModel> { Code = ErrorCodes.Success, Message = $"Can't find category with id = {id}" };  
            }

            var category = _mapper.Map<GetCategoryModel>(categoryBL);

            return new BaseResponse<GetCategoryModel> { Data = category };
        }

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(int id)
        {
            var isCategoryExist = await _categoryService.CheckIfExists(id);

            if (isCategoryExist)
            {
                return new BaseResponse { Code = ErrorCodes.NotFound, Message = $"Can't find item with id= {id}" };
            }

            await _categoryService.Delete(id);

            return new BaseResponse();

        }
    }
}
