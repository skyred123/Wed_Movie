using Microsoft.EntityFrameworkCore;
using MovieModel.Config;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace MovieModel.Generic
{
    public abstract class GenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            //ApplicationDbContext context = ApplicationDbContext.Create();
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T? GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int skip = 0, int take = 0)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                query = orderBy(query);

            if (skip != 0)
                _ = query.Skip(skip);

            if (take != 0)
                _ = query.Take(take);

            return query;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
            {
                _dbSet.Remove(obj);
            }
                
        }

        public int Count(Expression<Func<T, bool>> where) {
            return _dbSet.Count(where);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where) 
        {
            return _dbSet.Where(where);
        }
    }
}
