using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdritDhakal.Infrastructure.IRepository;

namespace UdritDhakal.Infrastructure.Repository.CRUDServices
{
    public class CRUDServices<TEntity> : ICRUDServices<TEntity> where TEntity : BaseEntity
    {
        private readonly SMSDbContext _smsDbContext;

        public CRUDServices(SMSDbContext smsDbContext)
        {
            _smsDbContext = smsDbContext;
        }
        public int Insert(TEntity entity)
        {
            var result = _smsDbContext.Set<TEntity>().Add(entity);
            _smsDbContext.SaveChanges();
            return result.Entity.Id;
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            var result = await _smsDbContext.Set<TEntity>().AddAsync(entity);
            await _smsDbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public int Update(TEntity entity)
        {
            var result = _smsDbContext.Set<TEntity>().Update(entity);
            _smsDbContext.SaveChanges();
            return result.Entity.Id;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            var result = _smsDbContext.Set<TEntity>().Update(entity);
            await _smsDbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public int Delete(TEntity entity)
        {
            var result = _smsDbContext.Set<TEntity>().Remove(entity);
            _smsDbContext.SaveChanges();
            return result.Entity.Id;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            var result = _smsDbContext.Set<TEntity>().Remove(entity);
            await _smsDbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public TEntity Get(int id)
        {
            return _smsDbContext.Set<TEntity>().Find(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _smsDbContext.Set<TEntity>().Where(expression).SingleOrDefault();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _smsDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _smsDbContext.Set<TEntity>().Where(expression).SingleOrDefaultAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _smsDbContext.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return _smsDbContext.Set<TEntity>().Where(expression).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _smsDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _smsDbContext.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsyncIncludingProperties(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _smsDbContext.Set<TEntity>().Where(predicate);

            foreach (var include in includes)
            {
                query = Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(query, include);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> QueryAsync(string commandText, object param = null, CommandType commandType = CommandType.Text)
        {
            return await _smsDbContext.Set<TEntity>().FromSqlRaw(commandText, param).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<object>> QueryListAsync(string commandText, object param = null, CommandType commandType = CommandType.Text)
        {
            var result = await _smsDbContext.Set<TEntity>().FromSqlRaw(commandText, param).ToListAsync();
            var result_list = new List<object>();
            foreach (var item in result)
            {
                result_list.Add(item);
            }
            return result_list;
        }
    }
}
