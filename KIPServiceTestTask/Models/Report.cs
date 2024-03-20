namespace KIPServiceTestTask.Models
{
    public class Report
    {
        public Guid query { get; set; }
        public int percent { get; set; }
        public Result? result { get; set; }
        public Report(Guid queryId, int percentValue, Result? result)
        {
            query = queryId;
            percent = percentValue;
            this.result = result;
        }
    }
}
