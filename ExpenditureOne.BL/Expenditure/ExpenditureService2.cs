using AutoMapper;
using ExpenditureOne.BL.Expenditure;
using ExpenditureOne.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpenditureOne.BL
{
    public interface IExpenditureService2 
    {
        Task<ExpenditureBL> FindById(int id);
        Task<ExpenditureBL> Create(ExpenditureBL modelBL);
        Task Delete(int id);
        Task<ExpenditureBL> Update(ExpenditureBL modelBL);
        Task<IEnumerable<ExpenditureBL>> GetAll();
        Task<IEnumerable<ExpenditureBL>> RawQuery(string sql);
        Task<bool> CheckIfExists(int id);
    }
    public class ExpenditureService2 : IExpenditureService2
    {
        protected readonly IGenericRepository2<DAL.Entities.Expenditure> _repository;
        protected readonly IMapper _mapper;

        public ExpenditureService2(IGenericRepository2<DAL.Entities.Expenditure> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async virtual Task<ExpenditureBL> FindById(int id)
        {
            var entity = await _repository.FindById(id);
            return Map(entity);
        }

        public async virtual Task<ExpenditureBL> Create(ExpenditureBL modelBL)
        {
            var entity = Map(modelBL);
            entity = await _repository.Create(entity);
            return Map(entity);
        }

        public async virtual Task Delete(int id)
        {
            await _repository.RemoveById(id);
        }

        public async virtual Task<ExpenditureBL> Update(ExpenditureBL modelBL)
        {
            var entity = Map(modelBL);

            entity = await _repository.UpdateWithIncludes(entity, x => x.Categories);
            return Map(entity);
        }

        public async virtual Task<IEnumerable<ExpenditureBL>> GetAll()
        {
            var entities = await _repository.Get(null, null, x => x.Categories);
            return Map(entities);
        }
        public async virtual Task<IEnumerable<ExpenditureBL>> RawQuery(string sql)
        {
            var entities = await _repository.RawQuery(sql);
            return Map(entities);
        }

        public async Task<bool> CheckIfExists(int id)
        {
            return await _repository.FindById(id) != null ? true : false;
        }


        protected ExpenditureBL Map(DAL.Entities.Expenditure model)
        {
            return _mapper.Map<ExpenditureBL>(model);
        }

        protected DAL.Entities.Expenditure Map(ExpenditureBL model)
        {
            return _mapper.Map<DAL.Entities.Expenditure>(model);
        }

        protected IEnumerable<ExpenditureBL> Map(IEnumerable<DAL.Entities.Expenditure> entitiesList)
        {
            return _mapper.Map<IEnumerable<ExpenditureBL>>(entitiesList);
        }

        protected IEnumerable<DAL.Entities.Expenditure> Map(IEnumerable<ExpenditureBL> entitiesList)
        {
            return _mapper.Map<IEnumerable<DAL.Entities.Expenditure>>(entitiesList);
        }
    }
}
