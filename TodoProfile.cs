using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Data.Entity;
using Tasks.Model;

namespace Tasks.Data
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            this.CreateMap<Todo, TodoModel>().ReverseMap();
        }
    }
}
