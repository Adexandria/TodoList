using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Data;
using Tasks.Data.Entity;
using Tasks.Model;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListController : ControllerBase
    {
        private readonly ITodoRepository todo;
        private readonly IMapper mapper;
        private readonly LinkGenerator gene;

        public ListController(IMapper mapper, LinkGenerator generator, ITodoRepository todo)
        {
            this.todo = todo;
            this.mapper = mapper;
            this.gene = generator;
        }
        //to get all todo lists
        [HttpGet]
        public async Task<ActionResult<TodoModel[]>> Get()
        {
            try
            {
                var List = await todo.GetTodoModels();
                return mapper.Map<TodoModel[]>(List);

            }
            catch (Exception dn)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, dn.Message);
            }
        }
        //to get individual list
        [HttpGet("{name:int}")]
        public async Task<ActionResult<TodoModel>> Get(int name)
        {
            try
            {
                var list = await todo.GetTodoModel(name);
                if (list == null) return NotFound("Invalid number!! It doesn't exist");
                return mapper.Map<TodoModel>(list);
            }
            catch (Exception dn)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, dn.Message);
            }
        }
        //search by date

        [HttpGet("search")]
        public async Task<ActionResult<TodoModel[]>> SearchByDate(DateTime time)
        {
            try
            {
                var date = await todo.GetTodoModelsByDate(time);
                if (!date.Any()) return NotFound("Invalid Date. It doesn't exist!!");
                return mapper.Map<TodoModel[]>(date);
            }
            catch (Exception dn)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, dn.Message);
            }
        }

//to post a to do
        public async Task<ActionResult<TodoModel>> Post(TodoModel model) {
            try
            {
                var exist = await todo.GetTodoModelName(model.Event);
                if (exist != null)
                {
                    return BadRequest(" The Event is being used ");

                }

                var location = gene.GetPathByAction("Get",
                    "List",
                    new { name = model.Event });

                if (string.IsNullOrWhiteSpace(location)) return BadRequest("Could not use current event");

                var place = mapper.Map<Todo>(model);

                todo.Add(place);

                if (await todo.SaveChanges())
                {
                    return Created($"/api/list/{place.Event}", mapper.Map<TodoModel>(place));
                }

            }

            catch (Exception dn)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, dn.Message);
            }

            return BadRequest("The Todo list didn't save try again");


        }
        // to update
        [HttpPut("{number:int}")]
        public async Task<ActionResult<TodoModel>> Put(TodoModel model, int number)
        {
            try
            {
                var todos = await todo.GetTodoModel(number);
                if (todos == null) return NotFound("Event is being used");
                mapper.Map(model, todos);
                if (await todo.SaveChanges())
                {
                    return mapper.Map<TodoModel>(todos);
                }

            }
            catch (Exception dn)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, dn.Message);
            }
            return BadRequest("The todo list isn't updated");
        }
//to delete
        [HttpDelete("{number:int}")]
         public async Task<IActionResult> Delete(int number)
        {
            try
            {
                var old = await todo.GetTodoModel(number);
                if (old == null) return NotFound("Event not found!!");
                todo.Delete(old);
                if(await todo.SaveChanges())
                {
                    return Ok();
                }
              

            }
            catch (Exception dn)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, dn.Message);
            }
            return BadRequest("Event wasn't deleted");
        }

        
    }
}
