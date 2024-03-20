using KIPServiceTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace KIPServiceTestTask.Repositories
{
    public class VisitRepository : ModelRepository<Visit>
    {
        public VisitRepository(ReportDBContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Visit>> GetAllAsync()
        {
            return await _context.Visits.ToListAsync();
        }

        public override async Task<Visit?> GetByIdAsync(Guid id)
        {
            return await _context.Visits.FindAsync(id);
        }
    }
}
