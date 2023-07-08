using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Domain.Entities;

namespace ToDoList.Core.Domains.ToDoList.Adapters.Service
{
    public interface IToDoListService
    {
        IList<ToDoListEntity> GetAllList();
        bool PostToDoList(ToDoListEntity toDoItem);
        ToDoListEntity GetToDoListById(int id);
        bool DeleteById(int id);
        bool UpdateToDoList(ToDoListEntity updatedToDoList); 
    } 
}
