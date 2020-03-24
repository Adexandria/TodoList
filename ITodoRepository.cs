using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Data.Entity;
using Tasks.Model;

namespace Tasks.Data
{
   public interface ITodoRepository 
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<Todo[]> GetTodoModels();
        Task<Todo> GetTodoModel(int name);
        Task<Todo> GetTodoModelName(string name);

        Task<bool> SaveChanges();
        Task<Todo[]> GetTodoModelsByDate(DateTime date);
        
    }
}
