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
            var entityType = _context.Model.FindEntityType(typeof(T));
            var keyName = entityType.FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
            var keyProperty = typeof(T).GetProperty(keyName);
            var keyValue = keyProperty.GetValue(entity);

            var existingEntity = await _dbSet.FindAsync(keyValue);
            if (existingEntity == null)
            {
                throw new InvalidOperationException($"Entity of type {typeof(T).Name} with id {keyValue} not found.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // İlişkili entity'leri işle
            foreach (var navigation in _context.Entry(existingEntity).Navigations)
            {
                if (navigation.CurrentValue == null)
                {
                    navigation.IsModified = false;
                }
            }

            await _context.SaveChangesAsync();
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
