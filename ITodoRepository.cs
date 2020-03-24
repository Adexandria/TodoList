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
      //To add
        void Add<T>(T entity) where T : class;
      //to delete
        void Delete<T>(T entity) where T : class;
      //for the Todolist
        Task<Todo[]> GetTodoModels();
        Task<Todo> GetTodoModel(int name);
        Task<Todo> GetTodoModelName(string name);
       Task<Todo[]> GetTodoModelsByDate(DateTime date);
      //to save changes
        Task<bool> SaveChanges();
        
        
    }
}
