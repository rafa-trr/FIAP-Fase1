using FIAP.TECH.CORE.DOMAIN.Entities;
using System.Linq.Expressions;

namespace FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task Create(T entity);
    Task Delete(int id);
    Task Update(T entity);
    Task<T?> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<bool> Exists(Expression<Func<T, bool>> expression);
}
