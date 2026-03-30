using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>();

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskItem task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return NotFound();

            tasks.Remove(task);

            return Ok();
        }
    }
}