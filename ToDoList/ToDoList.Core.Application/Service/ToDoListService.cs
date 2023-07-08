using ToDoList.Core.Domain.Entities;
using ToDoList.Core.Domains.ToDoList.Adapters.Repository;
using ToDoList.Core.Domains.ToDoList.Adapters.Service;

namespace ToDoList.Core.Application.Service
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _toDoListRepository;

        public ToDoListService(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }

        public IList<ToDoListEntity> GetAllList()
        {
            try
            {
                var result = _toDoListRepository.GetAllList();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool PostToDoList(ToDoListEntity toDoList)
        {
            try
            {
                _toDoListRepository.PostToDoList(toDoList);
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }

        public ToDoListEntity GetToDoListById(int id)
        {
            try
            {
                var result = _toDoListRepository.GetToDoListById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                var toDoList = _toDoListRepository.GetToDoListById(id);

                if (toDoList == null) 
                {
                    return false;
                }

                _toDoListRepository.DeleteById(toDoList);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateToDoList(ToDoListEntity updatedToDoList)
        {
            try
            {
                var toDoList = _toDoListRepository.GetToDoListById(updatedToDoList.Id);

                if (toDoList == null)
                {
                    throw new Exception("Tarefa não encontrada.");
                }
                if (toDoList.Id != updatedToDoList.Id)
                {
                    throw new Exception("Não é possivel alterar o ID da tarefa.");
                }

                // Update the properties of the existing ToDoList with the updated values
                toDoList.Name = updatedToDoList.Name;
                toDoList.Description = updatedToDoList.Description;
                toDoList.StartDate = updatedToDoList.StartDate;
                toDoList.FinishDate = updatedToDoList.FinishDate;

                _toDoListRepository.UpdateToDoList(toDoList);
                return true;
            }
            catch (Exception)
            {
                throw;
                return false;
            }
        }
    }
}