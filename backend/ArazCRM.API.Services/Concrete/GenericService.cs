using ArazCRM.API.Repositories.Abstract;
using ArazCRM.API.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Concrete
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync(); // SaveChangesAsync burada sorunsuz çalışmalı
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity != null)
            {
                await _repository.UpdateAsync(entity);
                await _repository.SaveChangesAsync(); // SaveChangesAsync burada sorunsuz çalışmalı
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
            await _repository.SaveChangesAsync(); // SaveChangesAsync burada sorunsuz çalışmalı
        }
    }
}
