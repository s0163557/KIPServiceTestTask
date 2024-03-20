using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace KIPServiceTestTask.Models
{
    public class QueryInfo
    {
        [Key]
        public Guid guid { get; set; }
        public Guid userGuid { get; set; }
        public DateOnly from { get; set; }
        public DateTime timer { get; set; }
        public DateOnly to { get; set; }
        public QueryInfo(Guid userGuid, Guid guid, DateOnly from, DateOnly to, DateTime timer)
        {
            this.userGuid = userGuid;
            this.guid = guid;
            this.from = from;
            this.to = to;
            this.timer = timer;
        }
    }
}
