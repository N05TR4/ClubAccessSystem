

using ClubAccessSystem.Domain.Result;

namespace ClubAccessSystem.Domain.Repositories
{
    public interface IBaseRepository<TEntity, TData> where TEntity : class
    {
        Task<OperationResult<TData>> Save(TEntity entity);
        Task<OperationResult<TData>> Update(TEntity entity);
        Task<OperationResult<TData>> Delete(TEntity entity);
        Task<OperationResult<List<TData>>> GetAll();
        Task<OperationResult<TData>> GetById(int id);

    }
}
