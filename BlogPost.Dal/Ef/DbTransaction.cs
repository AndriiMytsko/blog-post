using System;
using BlogPost.Dal.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlogPost.Dal.Ef
{
    public class DbTransaction : IDbTransaction
    {
        private bool _disposed;
        private readonly IDbContextTransaction _transaction;

        public DbTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _transaction.Dispose();
            }
            _disposed = true;
        }
    }
}