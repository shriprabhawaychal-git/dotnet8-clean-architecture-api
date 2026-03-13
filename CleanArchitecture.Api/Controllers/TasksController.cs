using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TasksController> _logger;
        public TasksController(ITaskRepository taskRepository, ILogger<TasksController> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            _logger.LogInformation("Fetching tasks for page {PageNumber} with page size {PageSize}.", pageNumber, pageSize);

            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than 0.");
            }

            var tasks = await _taskRepository.GetAllAsync(pageNumber, pageSize);

            var result = tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedDate = t.CreatedDate
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound($"Task with Id {id} not found.");

            var result = new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedDate = task.CreatedDate
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto dto)
        {
            var taskItem = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description
            };

            var createdTask = await _taskRepository.AddAsync(taskItem);

            var result = new TaskDto
            {
                Id = createdTask.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                IsCompleted = createdTask.IsCompleted,
                CreatedDate = createdTask.CreatedDate
            };

            return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            var taskItem = new TaskItem
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted
            };

            var updated = await _taskRepository.UpdateAsync(taskItem);

            if (!updated)
                return NotFound($"Task with Id {id} not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskRepository.DeleteAsync(id);

            if (!deleted)
                return NotFound($"Task with Id {id} not found.");

            return NoContent();
        }
    }
}
