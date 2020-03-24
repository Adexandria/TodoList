using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Data.Entity;
using Tasks.Model;

namespace Tasks.Data
{
    public class TodoRepository : ITodoRepository
{
       private readonly ILogger<TodoRepository> logger;
        private readonly TodoDbContext todo;

        public TodoRepository(TodoDbContext todoDb,ILogger<TodoRepository> logger)
        {
           this.logger = logger;
            this.todo = todoDb;
        }
        public void Add<T>(T entity) where T : class
        {
           logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
           todo.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            todo.Remove(entity);
        }

        public async Task<Todo> GetTodoModel(int name)
        {
           logger.LogInformation($"Getting {name} information");
            IQueryable<Todo> query = todo.Todos;
            query = query.Where(c => c.Id ==name);
            return await query.FirstOrDefaultAsync();

        }
        public async Task<Todo> GetTodoModelName(string name)
        {
            logger.LogInformation($"Getting {name} information");
            IQueryable<Todo> query = todo.Todos;
            query = query.Where(c => c.Event == name);
            return await query.FirstOrDefaultAsync();

        }
        public async Task<Todo[]> GetTodoModels()
        {
           logger.LogInformation($"Getting all information");
            IQueryable<Todo> query = todo.Todos;
            query = query.OrderBy(c =>c.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Todo[]> GetTodoModelsByDate(DateTime date)
        {
         logger.LogInformation($"Getting all information");
            IQueryable<Todo> query = todo.Todos;
            query = query.OrderByDescending(c => c.DateTime).Where(c => c.DateTime.Date== date.Date);
            return await query.ToArrayAsync();
        }

        public  async Task<bool> SaveChanges()
        {
            logger.LogInformation($"Saving changes");
            return (await todo.SaveChangesAsync() > 0);
        }
    }
}
