using System;

namespace BlogPost.Dal.Interfaces
{
    public interface IDbTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}