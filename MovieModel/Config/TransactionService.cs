using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieModel.Config
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public void ExecuteTransaction(Action action)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                action();
                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
