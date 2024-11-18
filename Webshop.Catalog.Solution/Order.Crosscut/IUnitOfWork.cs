using System.Data;

namespace Order.Crosscut
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
        void BeginTransaction(IsolationLevel isolationLevel);
    }
}
