using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Domain.Entities;

namespace ToDoList.Core.Domains.ToDoList.Adapters.Repository
{
    public interface IToDoListRepository
    {
        IList<ToDoListEntity> GetAllList();
        void PostToDoList(ToDoListEntity toDoItem);
        ToDoListEntity GetToDoListById(int id);
        void DeleteById(ToDoListEntity toDoItem);
        void UpdateToDoList(ToDoListEntity toDoList);
    }
}
