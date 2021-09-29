using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ExpenditureOne.DAL
{
    //public interface IEntity
    //{
    //    int Id { get; set; }
    //}

    public interface IGenericRepository2<T> where T : class, IEntity
    {
        Task<T> FindById(int id);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                 params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> RawQuery(string sql);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> Create(T item);
        Task RemoveById(int id);
        Task<T> Update(T item, params Expression<Func<T, object>>[] includes);
        Task<T> UpdateWithIncludes(T item, params Expression<Func<T, object>>[] includes);
        void Detatch(int id);
        void Dispose();
    }

    public class GenericRepository2<T> : IDisposable, IGenericRepository2<T> where T : class, IEntity
    {
        readonly DbContext _context;
        readonly DbSet<T> _dbSet;

        public GenericRepository2(ExpenditureContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async virtual Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async virtual Task<IEnumerable<T>> RawQuery(string sql)
        {
            var result = _dbSet.FromSqlRaw(sql);

            return await result.ToListAsync();
        }

        public async virtual Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null,
        params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(filter);
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public async virtual Task<T> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Create(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> Update(T item, params Expression<Func<T, object>>[] includes)
        {
            //var entity = FindByIdWithIncludes(item.Id, includes);
            var entity = _dbSet.Find(item.Id);
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            updatedEntity.CurrentValues.SetValues(item);

            await _context.SaveChangesAsync();

            return item;
        }

        //TODO: https://entityframeworkcore.com/knowledge-base/55088933/update-parent-and-child-collections-on-generic-repository-with-ef-core
        public async Task<T> UpdateWithIncludes(T entity, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var dbEntity = await _context.FindAsync<T>(entity.Id);

                var dbEntry = _context.Entry(dbEntity);
                dbEntry.State = EntityState.Modified;
                dbEntry.CurrentValues.SetValues(entity);

                foreach (var property in includes)
                {
                    var propertyName = property.GetPropertyAccess().Name;
                    var dbItemsEntry = dbEntry.Collection(propertyName);
                    var accessor = dbItemsEntry.Metadata.GetCollectionAccessor();

                    await dbItemsEntry.LoadAsync();
                    var dbItemsMap = ((IEnumerable<IEntity>)dbItemsEntry.CurrentValue)
                        .ToDictionary(e => e.Id);

                    var items = (IEnumerable<IEntity>)accessor.GetOrCreate(entity, false);

                    foreach (var item in items)
                    {
                        if (!dbItemsMap.TryGetValue(item.Id, out var oldItem))
                            accessor.Add(dbEntity, item, false);
                        else
                        {
                            _context.Entry(oldItem).CurrentValues.SetValues(item);
                            dbItemsMap.Remove(item.Id);
                        }
                    }

                    foreach (var oldItem in dbItemsMap.Values)
                        accessor.Remove(dbEntity, oldItem);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

            return entity;
        }

        public T FindByIdWithIncludes(int id, params Expression<Func<T, object>>[] includes)
        {
            var entityQuery = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return entityQuery.SingleOrDefault(e => e.Id == id);
        }

        public void Detatch(int id)
        {
            var entity = _dbSet.Find(id);
            _context.Entry(entity).State = EntityState.Detached;
        }

        public async virtual Task RemoveById(int id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}