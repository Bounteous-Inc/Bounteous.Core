using System.Data;

namespace Bounteous.Core.Data;

public class ReadonlyUnitOfWork : IUnitOfWork
{
    public ReadonlyUnitOfWork(IDbConnection connection)
    {
        Connection = connection;
    }

    public IDbTransaction Transaction => null;
    public IDbConnection Connection { get; }

    public void Dispose()
    {
        Connection?.Dispose();
    }

    public void Commit()
    {
    }

    public void Rollback()
    {
    }
}