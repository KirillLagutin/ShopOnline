using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Ef.Data;
using ShopOnline.Domain.IGenericRepository;
using ShopOnline.Models;

namespace ShopOnline.Data.Ef.GenericRepository;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity: class, IEntity
{
    protected readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<TEntity> Entities => _dbContext.Set<TEntity>();


    public virtual Task<TEntity> GetById(Guid id)
        => Entities.FirstAsync(it => it.Id == id);

    public virtual async Task<IReadOnlyList<TEntity>> GetAll()
        => await Entities.ToListAsync();

    public virtual async Task Add(TEntity entity)
    {
        await Entities.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteById(Guid id)
    {
        var entity = await Entities
            .FirstAsync(p => p.Id == id);
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task ClearItems()
    {
        foreach (var item in _dbContext.CartItems)
        {
            _dbContext.CartItems.Remove(item);
        }
        await _dbContext.SaveChangesAsync();
    }
}
