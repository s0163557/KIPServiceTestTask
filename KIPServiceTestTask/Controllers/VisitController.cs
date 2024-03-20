using KIPServiceTestTask.Models;
using KIPServiceTestTask.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KIPServiceTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly VisitRepository _visitRepository;

        public VisitController(VisitRepository visitRepositroy)
        {
            _visitRepository = visitRepositroy;
        }

        [HttpGet]
        public async Task<IEnumerable<Visit>> GetVisits()
        {
            return await _visitRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Visit>> GetVisit(Guid id)
        {
            var response = await _visitRepository.GetByIdAsync(id);
            if (response == null)
                return Content("Элемент не найден");
            return response;
        }

        [HttpPut("{visitId}")]
        public async Task<IActionResult> PutVisit(Visit visit)
        {
            return await _visitRepository.PutAsync(visit) ? Content("Успешно изменено.") : Content("Не удалось измененить.");
        }

        [HttpPost]
        public async Task<ActionResult<Visit>> PostVisit(Guid userId, DateOnly dateOfLogin)
        {
            Visit visit = new Visit(Guid.NewGuid(), userId, dateOfLogin);
            return await _visitRepository.PostAsync(visit) ? Content("Успешно добавлено.") : Content("Не удалось добавить.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit(Guid id)
        {
            return await _visitRepository.DeleteAsync(id) ? Content("Успешно удалено.") : Content("Не удалось удалить.");
        }
    }
}
