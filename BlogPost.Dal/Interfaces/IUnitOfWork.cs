using System;

namespace BlogPost.Dal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction();
    }
}