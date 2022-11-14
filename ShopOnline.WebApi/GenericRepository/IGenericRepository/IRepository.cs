using ShopOnline.Models;

namespace ShopOnline.WebApi.GenericRepository.IGenericRepository;

public interface IRepository<TEntity> where TEntity: IEntity
{
    Task<TEntity> GetById(int id);
    Task<IReadOnlyList<TEntity>> GetAll();
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task DeleteById(int id);

}