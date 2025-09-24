using Microsoft.EntityFrameworkCore;
using SimpleExample.Data;


namespace SimpleExample.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly MyDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            // return _dbSet.AsNoTracking().ToList();
            var list = _dbSet.FromSqlRaw($"EXEC GetAllOrders").AsNoTracking();
            return [.. list];
        }
        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Update(T existing , T entity)
        {
            
            _context.Entry(existing).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
