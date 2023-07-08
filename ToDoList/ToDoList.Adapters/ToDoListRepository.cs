using Microsoft.Extensions.Caching.Memory;
using ToDoList.Core.Domain.Entities;
using ToDoList.Core.Domains.ToDoList.Adapters.Repository;

namespace ToDoList.Adapters
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "ToDoListItems";

        public ToDoListRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IList<ToDoListEntity> GetAllList()
        {
            if (!_cache.TryGetValue(CacheKey, out IList<ToDoListEntity> toDoList))
            {
                toDoList = new List<ToDoListEntity>();
                _cache.Set(CacheKey, toDoList, TimeSpan.FromMinutes(30));
            }

            return toDoList;
        }

        public void PostToDoList(ToDoListEntity toDoList)
        {
            if (toDoList == null)
            {
                throw new ArgumentNullException(nameof(toDoList));
            }

            IList<ToDoListEntity> existingList = GetAllList();

            if (existingList.Any(item => item.Id == toDoList.Id))
            {
                throw new Exception("A duplicate id list cannot be added.");
            }

            int nextId = GetNextIdFromCache(); // Obtenha o próximo ID autoincrementado do cache

            toDoList.Id = nextId;

            existingList.Add(toDoList);
            _cache.Set(CacheKey, existingList, TimeSpan.FromMinutes(30));
        }

        public ToDoListEntity GetToDoListById(int id)
        {
            var response = GetAllList();
            var result = response.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public void DeleteById(ToDoListEntity toDoList)
        {
            var result = GetAllList();
            result.Remove(toDoList);
            _cache.Set(CacheKey, result, TimeSpan.FromMinutes(30));
        }

        public void UpdateToDoList(ToDoListEntity toDoList)
        {
            var toDoLists = GetAllList();
            var existingToDoList = toDoLists.FirstOrDefault(x => x.Id == toDoList.Id);

            if (existingToDoList == null)
            {
                throw new Exception("Lista não encontrada.");
            }

            existingToDoList.Name = toDoList.Name;
            existingToDoList.Description = toDoList.Description;
            existingToDoList.StartDate = toDoList.StartDate;
            existingToDoList.FinishDate = toDoList.FinishDate;

            _cache.Set(CacheKey, toDoLists, TimeSpan.FromMinutes(30));
        }

        private int GetNextIdFromCache()
        {
            const string idKey = "NextId";
            int nextId = _cache.Get<int>(idKey); // Obtém o próximo ID do cache (0 se não existir)

            nextId++; // Incrementa o ID

            _cache.Set(idKey, nextId, TimeSpan.FromMinutes(30)); // Armazena o próximo ID no cache

            return nextId;
        }
    }
}