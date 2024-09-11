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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            // Entity'deki ID'yi dinamik olarak alın. Burada InvoiceId örnek olarak kullanılmıştır.
            var id = ((dynamic)entity).InvoiceId; // Veya ilgili entity'nin ID alanı neyse onu kullanın.

            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity != null)
            {
                // Mevcut kaydı entity'den gelen verilerle güncelle.
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydet
            }
            else
            {
                throw new Exception("Entity not found for update.");
            }
        }



        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
