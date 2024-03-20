using KIPServiceTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace KIPServiceTestTask.Repositories
{
    public class QueryInfoRepository : ModelRepository<QueryInfo>
    {
        public QueryInfoRepository(ReportDBContext context) : base(context)
        {

        }

        public async override Task<IEnumerable<QueryInfo>> GetAllAsync()
        {
            return await _context.QueryInfos.ToListAsync();
        }

        public async override Task<QueryInfo?> GetByIdAsync(Guid id)
        {
            return await _context.QueryInfos.FindAsync(id);
        }
    }
}
