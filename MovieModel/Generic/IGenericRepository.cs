using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        T? GetById(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetList(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int skip = 0, int take = 0);
        void Add(T entity);
        void Update(T entity);
        void Delete(Expression<Func<T, bool>> where);
        int Count(Expression<Func<T, bool>> where);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}
