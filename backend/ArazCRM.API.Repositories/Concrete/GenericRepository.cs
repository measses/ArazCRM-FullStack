using ArazCRM.API.Data;
using ArazCRM.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArazCRM.API.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity); // Yeni veriyi ekler
            await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydeder
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity); // Doğrudan verilen entity'yi siler
            await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydeder
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); // Tüm verileri getirir
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id); // ID'ye göre tek bir veri getirir
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity); // Var olan veriyi günceller
            await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydeder
        }
    }
}
