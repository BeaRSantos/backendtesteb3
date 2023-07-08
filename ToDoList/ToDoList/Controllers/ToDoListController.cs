using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Domain.Entities;
using ToDoList.Core.Domains.ToDoList.Adapters.Service;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]

    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet]
        public IActionResult GetAllList()
        {
            try
            {
                var result = _toDoListService.GetAllList();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult PostToDoList(ToDoListEntity toDoItem)
        {
            try
            {
                var result = _toDoListService.PostToDoList(toDoItem);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetToDoListById(int id)
        {
            try
            {
                var result = _toDoListService.GetToDoListById(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                var result = _toDoListService.DeleteById(id);

                if(result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPut()]
        public IActionResult UpdateToDoList(ToDoListEntity updatedToDoItem)
        {
            try
            {
                var result = _toDoListService.UpdateToDoList(updatedToDoItem);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    
}
