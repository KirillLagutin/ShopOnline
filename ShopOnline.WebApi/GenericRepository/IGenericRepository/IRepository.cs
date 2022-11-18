using ShopOnline.Models;

namespace ShopOnline.WebApi.GenericRepository.IGenericRepository;

public interface IRepository<TEntity> where TEntity: IEntity
{
    Task<TEntity> GetById(Guid id);
    Task<IReadOnlyList<TEntity>> GetAll();
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task DeleteById(Guid id);

}