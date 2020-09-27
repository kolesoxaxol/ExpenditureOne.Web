using AutoMapper;
using ExpenditureOne.BL.Models;
using ExpenditureOne.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpenditureOne.BL
{
    public interface IGenericService<ModelBL>
    where ModelBL : class
    {
        Task<ModelBL> FindById(int id);
        Task<ModelBL> Create(ModelBL modelBL);
        Task Delete(int id);
        Task<ModelBL> Update(/*int id, */ ModelBL modelBL);
        Task<IEnumerable<ModelBL>> GetAll();
        Task<GenericPagedList<ModelBL>> GetPaged(int itemsPerPage, int Page, params Expression<Func<ModelBL, bool>>[] filters);
        Task<bool> CheckIfExists(int id);
    }

    public abstract class GenericService<ModelBL, Entity> : IGenericService<ModelBL>
        where ModelBL : class
        where Entity : class
    {
        protected readonly IGenericRepository<Entity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async virtual Task<ModelBL> FindById(int id)
        {
            var entity = await _repository.FindById(id);
            return Map(entity);
        }

        public async virtual Task<ModelBL> Create(ModelBL modelBL)
        {
            var entity = Map(modelBL);
            entity = await _repository.Create(entity);
            return Map(entity);
        }

        public async virtual Task Delete(int id)
        {
            await _repository.RemoveById(id);
        }

        public async virtual Task<ModelBL> Update(/*int id,*/ ModelBL modelBL)
        {
            //_repository.Detatch(id);
            var entity = Map(modelBL);
            entity = await _repository.Update(entity);
            return Map(entity);
        }

        public async virtual Task<IEnumerable<ModelBL>> GetAll()
        {
            var entities = await _repository.Get();
            return Map(entities);
        }

        public async virtual Task<GenericPagedList<ModelBL>> GetPaged(int itemsPerPage, int Page, params Expression<Func<ModelBL, bool>>[] filters)
        {
            var skip = itemsPerPage * (Page - 1);
            var entitiesQuery = _repository.Query();
            var modelsQuery = _mapper.ProjectTo<ModelBL>(entitiesQuery);
            foreach (var filter in filters)
            {
                modelsQuery = modelsQuery.Where(filter);
            }
            var totalPages = (modelsQuery.Count() - 1) / itemsPerPage + 1;
            var pagedItems = await modelsQuery.Skip(skip).Take(itemsPerPage).ToListAsync();

            var pagedList = new GenericPagedList<ModelBL>
            {
                Items = pagedItems,
                TotalPages = totalPages,
                CurrentPage = Page
            };

            return pagedList;
        }

        public async Task<bool> CheckIfExists(int id)
        {
            return await _repository.FindById(id) != null ? true : false;
        }


        protected ModelBL Map(Entity model)
        {
            return _mapper.Map<ModelBL>(model);
        }

        protected Entity Map(ModelBL model)
        {
            return _mapper.Map<Entity>(model);
        }

        protected IEnumerable<ModelBL> Map(IEnumerable<Entity> entitiesList)
        {
            return _mapper.Map<IEnumerable<ModelBL>>(entitiesList);
        }

        protected IEnumerable<Entity> Map(IEnumerable<ModelBL> entitiesList)
        {
            return _mapper.Map<IEnumerable<Entity>>(entitiesList);
        }


    }
}
