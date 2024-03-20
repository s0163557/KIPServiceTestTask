using Humanizer;
//using KIPServiceTestTask.Migrations;
using KIPServiceTestTask.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Json;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using KIPServiceTestTask.Repositories;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace KIPServiceTestTask.Controllers
{
    [ApiController]
    public class ReportController : ControllerBase
    {
        private ReportDBContext _reportDBContext;
        private IHttpContextAccessor _httpContextAccessor;
        private QueryInfoRepository _queryInfoRepository;
        private int _delay;
        public ReportController(ReportDBContext reportDBContext, IConfiguration appConfig, IHttpContextAccessor httpContextAccessor, QueryInfoRepository queryInfoRepository)
        {
            _reportDBContext = reportDBContext;
            _delay = Int32.Parse(appConfig["Delay"]);
            _httpContextAccessor = httpContextAccessor;
            _queryInfoRepository = queryInfoRepository;
        }

        [RouteAttribute("/report/user_statistics")]
        [HttpPost]
        public async Task<Guid> UserStatistics(Guid userId, DateOnly from, DateOnly to)
        {
            QueryInfo queryInfo = new QueryInfo(userId, Guid.NewGuid(), from, to, DateTime.Now);
            _httpContextAccessor.HttpContext.Session.Set<QueryInfo>(queryInfo.guid.ToString(), queryInfo);
            await _queryInfoRepository.PostAsync(queryInfo);
            return queryInfo.guid;
        }

        [RouteAttribute("/report/info")]
        [HttpGet]
        public async Task<IActionResult> info(Guid requestGuid)
        {
            if (_httpContextAccessor.HttpContext.Session.Keys.Contains(requestGuid.ToString()))
            {
                QueryInfo queryInfo = _httpContextAccessor.HttpContext.Session.Get<QueryInfo>(requestGuid.ToString());
                if ((DateTime.Now - queryInfo.timer).TotalMilliseconds > _delay)
                {
                    List<Visit> result = await _reportDBContext.Visits.Where(visit =>
                    visit.UserId == queryInfo.userGuid &&
                    visit.DateOfVisit >= queryInfo.from &&
                    visit.DateOfVisit <= queryInfo.to).ToListAsync();
                    Result res = new Result(queryInfo.userGuid, result.Count);
                    return new JsonResult(new Report(queryInfo.guid, 100, res));
                }
                else
                {
                    TimeSpan passedTime = DateTime.Now - queryInfo.timer;
                    double percent = passedTime.TotalMilliseconds / _delay;
                    int millisecondsInPercent = (int)(percent * 100);
                    return new JsonResult(new Report(queryInfo.guid, millisecondsInPercent, null));
                }
            }
            //94e4b863-a10a-4993-9627-52e2fd85f483
            else if (await _queryInfoRepository.ExistsAsync(requestGuid))
            {
                QueryInfo queryInfo = await _queryInfoRepository.GetByIdAsync(requestGuid);
                if ((DateTime.Now - queryInfo.timer).TotalMilliseconds > _delay)
                {
                    List<Visit> result = await _reportDBContext.Visits.Where(visit =>
                    visit.UserId == queryInfo.userGuid &&
                    visit.DateOfVisit >= queryInfo.from &&
                    visit.DateOfVisit <= queryInfo.to).ToListAsync();
                    Result res = new Result(queryInfo.userGuid, result.Count);
                    return new JsonResult(new Report(queryInfo.guid, 100, res));
                }
                else
                {
                    TimeSpan passedTime = DateTime.Now - queryInfo.timer;
                    double percent = passedTime.TotalMilliseconds / _delay;
                    int millisecondsInPercent = (int)(percent * 100);
                    return new JsonResult(new Report(queryInfo.guid, millisecondsInPercent, null));
                }
            }
            return new JsonResult("Не найден Guid запроса");
        }
    }
}
