
using System.Linq.Expressions;
using HL.Core.Common;
using HL.Core.Interfaces;
using HL.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace HL.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{

    protected readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet; 

    public GenericRepository(AppDbContext context)
    {
        _context=context;
        _dbSet=_context.Set<T>();
    }
    public async Task AddAsync(T entity)=>await _dbSet.AddAsync(entity);

    public void Delete(T entity)=>_dbSet.Remove(entity);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public void Update(T entity) => _dbSet.Update(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) =>_dbSet.Where(predicate);
}