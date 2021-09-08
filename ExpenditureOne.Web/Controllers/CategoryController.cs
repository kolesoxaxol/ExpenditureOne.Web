using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExpenditureOne.BL;
using ExpenditureOne.BL.Models;
using ExpenditureOne.DAL;
using ExpenditureOne.DAL.Entities;
using ExpenditureOne.Web.Enums;
using ExpenditureOne.Web.Models;
using ExpenditureOne.Web.Models.Category;
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
        //protected readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, ICategoryService categoryService, IGenericRepository<Category> repository)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            //_repository = repository;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<BaseResponse<IEnumerable<CategoryModel>>> Get()
        {
            //var t = await _repository.Get();
            var categoriesBL = await _categoryService.GetAll();
            var categories = _mapper.Map<IEnumerable<CategoryModel>>(categoriesBL);

            return new BaseResponse<IEnumerable<CategoryModel>>
            {
                Data = categories
            };
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<BaseResponse<CategoryModel>> Get(int id)
        {
            var categoryBL = await _categoryService.FindById(id);

            if (categoryBL == null)
            {
                return new BaseResponse<CategoryModel> { Code = ErrorCodes.Success, Message = $"Can't find category with id = {id}" };
            }

            var category = _mapper.Map<CategoryModel>(categoryBL);

            return new BaseResponse<CategoryModel> { Data = category };
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<BaseResponse<CategoryModel>> Post(CategoryCreateRequest categoryRequest)
        {
            var categoryBL = _mapper.Map<CategoryBL>(categoryRequest);
            categoryBL = await _categoryService.Create(categoryBL);

            var response = _mapper.Map<CategoryModel>(categoryBL);
            return new BaseResponse<CategoryModel> { Data = response};
        }

        //TODO: Implement paging
        //public async virtual Task<GenericPagedList<ModelBL>> GetPaged(int itemsPerPage, int Page, params Expression<Func<ModelBL, bool>>[] filters)
        //{
        //    var skip = itemsPerPage * (Page - 1);
        //    var entitiesQuery = _repository.Query();
        //    var modelsQuery = _mapper.ProjectTo<ModelBL>(entitiesQuery);
        //    foreach (var filter in filters)
        //    {
        //        modelsQuery = modelsQuery.Where(filter);
        //    }
        //    var totalPages = (modelsQuery.Count() - 1) / itemsPerPage + 1;
        //    var pagedItems = await modelsQuery.Skip(skip).Take(itemsPerPage).ToListAsync();

        //    var pagedList = new GenericPagedList<ModelBL>
        //    {
        //        Items = pagedItems,
        //        TotalPages = totalPages,
        //        CurrentPage = Page
        //    };

        //    return pagedList;
        //}

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<BaseResponse> Put(CategoryEditRequest categoryRequest)
        {
            var isCategoryExist = await _categoryService.CheckIfExists(categoryRequest.Id);

            if (!isCategoryExist)
            {
                return new BaseResponse { Code = ErrorCodes.NotFound, Message = $"Can't find item with id= {categoryRequest.Id}" };
            }

            var category = _mapper.Map<CategoryBL>(categoryRequest);

            await _categoryService.Update(category);

            return new BaseResponse();

        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<BaseResponse> Delete(int id)
        {
            var isCategoryExist = await _categoryService.CheckIfExists(id);

            if (!isCategoryExist)
            {
                return new BaseResponse { Code = ErrorCodes.NotFound, Message = $"Can't find item with id= {id}" };
            }

            await _categoryService.Delete(id);

            return new BaseResponse();
        }
    }
}
