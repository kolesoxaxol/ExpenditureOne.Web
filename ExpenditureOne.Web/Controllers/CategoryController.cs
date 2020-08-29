using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExpenditureOne.BL;
using ExpenditureOne.BL.Models;
using ExpenditureOne.Web.Responses;
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
        public async Task<GetCategoryResponse> Get()
        {
            var categories = await _categoryService.GetAll();
            var categoriesModel = _mapper.Map<IEnumerable<GetCategoryModel>>(categories);

            return new GetCategoryResponse { 
                Categories = categoriesModel
            };
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        public void Delete(int id)
        {
        }
    }
}
