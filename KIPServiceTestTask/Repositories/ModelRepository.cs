using Microsoft.EntityFrameworkCore;

namespace KIPServiceTestTask.Repositories
{
    public abstract class ModelRepository<T>
    {
        protected readonly ReportDBContext _context;
        public ModelRepository(ReportDBContext context)
        {
            _context = context;
        }
        abstract public Task<IEnumerable<T>> GetAllAsync();
        abstract public Task<T?> GetByIdAsync(Guid id);
        public async Task<bool> PostAsync(T model)
        {
            await _context.AddAsync(model);
            return await SaveAsync();
        }
        public async Task<bool> PutAsync(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var model = await GetByIdAsync(id);
            if (model != null)
                _context.Remove(model);
            return await SaveAsync();
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            var model = await GetByIdAsync(id);
            if (model == null)
                return false;
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
