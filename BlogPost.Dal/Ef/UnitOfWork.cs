using System;
using BlogPost.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Dal.Ef
{
    public class UnitOfWork<T> : IUnitOfWork
      where T : DbContext
    {
        private bool _disposed;
        protected readonly T DbContext;

        public UnitOfWork(T dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                DbContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbTransaction BeginTransaction()
        {
            return new DbTransaction(DbContext.Database.BeginTransaction());
        }
    }
}