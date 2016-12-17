namespace TK.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}