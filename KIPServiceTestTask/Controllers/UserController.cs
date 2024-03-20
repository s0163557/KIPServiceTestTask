using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KIPServiceTestTask;
using KIPServiceTestTask.Models;
using KIPServiceTestTask.Repositories;

namespace KIPServiceTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepositroy)
        {
            _userRepository = userRepositroy;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var response =  await _userRepository.GetByIdAsync(id);
            if (response == null)
                return Content("Элемент не найден");
            return response;
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(User user)
        {
            return await _userRepository.PutAsync(user) ? Content("Успешно изменено.") : Content("Не удалось измененить.");
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(string name)
        {
            User user = new User(Guid.NewGuid(), name);
            return await _userRepository.PostAsync(user) ? Content("Успешно добавлено.") : Content("Не удалось добавить.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            return await _userRepository.DeleteAsync(id) ? Content("Успешно удалено.") : Content("Не удалось удалить.");
        }
    }
}
