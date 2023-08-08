using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface ICrudRepository<ID, E> where E : BaseEntity<ID>
    {
        Task<IEnumerable<E>> GetAllAsync();
        Task<E> GetAsync(ID id);
        Task AddAsync(E entity);
        Task UpdateAsync(E entity);
        Task DeleteAsync(ID id);
    }
}
