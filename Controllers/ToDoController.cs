using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        readonly ITA21Context _context;

        public ToDoController()
        {
            _context = new ITA21Context();
        }

        // GET: api/<ToDoController>
        /// <summary>
        /// Get all TODOs
        /// </summary>
        /// <returns>All todos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            List<ToDoTable> toDoItems = await _context.ToDoTables.ToListAsync();
            return toDoItems == null ? NotFound() : Ok(toDoItems);
        }

        // GET api/<ToDoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoTaskById(int id)
        {
            var toDoItem = await _context.ToDoTables.FindAsync(id);
            return toDoItem == null ? NotFound() : Ok(toDoItem);
        }

        // PUT api/<ToDoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoTask(int id, [FromBody] ToDoTable taskToUpdate)
        {
            if (id != taskToUpdate.Id)
            {
                return BadRequest("Not same ID");
            }

            try
            {
                var taskInDB = await _context.ToDoTables.FindAsync(id);

                if (taskInDB == null)
                {
                    return NotFound();
                }

                taskInDB.Title = taskToUpdate.Title;
                taskInDB.Content = taskToUpdate.Content;
                taskInDB.Date = taskToUpdate.Date;
                taskInDB.Completed = taskToUpdate.Completed;
                taskInDB.Deleted = taskToUpdate.Deleted;

                await _context.SaveChangesAsync();
                return Ok(taskToUpdate);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // POST api/<ToDoController>
        [HttpPost]
        public async Task<IActionResult> AddNewTaskToDo([FromBody] ToDoTable newTask)
        {
            try
            {

                if(newTask.Id != 0)
                {
                    return BadRequest("ID should not be provided when addin a new task, ID is autoicrement, please send me data without ID");
                }
                _context.ToDoTables.Add(newTask);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(AddNewTaskToDo), new { id = newTask.Id }, newTask);

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        // DELETE api/<ToDoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoTask(int id)
        {
            try
            {
                var itemToDelete = await _context.ToDoTables.FindAsync(id);
                if(itemToDelete != null)
                {
                    _context.ToDoTables.Remove(itemToDelete);
                    await _context.SaveChangesAsync();

                    return Ok(itemToDelete);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
