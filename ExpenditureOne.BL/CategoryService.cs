using ExpenditureOne.BL.Models;
using ExpenditureOne.DAL;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;

namespace ExpenditureOne.BL
{
    public interface ICategoryService
    {
        void Add(CategoryBL category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericRepository;
        public CategoryService(IGenericRepository<Category> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Add(CategoryBL category)
        {
            //TODO: rework it ot mapping
            Category newCategory = new Category
            {
                CategoryName = "test",
                Color = "red",
                Id = 2
            };

             _genericRepository.Create(newCategory);
        }
    }
}
