using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Domain.Entities;
using ToDoList.Core.Domains.ToDoList.Adapters.Repository;

namespace ToDoList.Adapters.Cache
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
                throw new Exception("A duplicate ToDoList cannot be added.");
            }

            existingList.Add(toDoList);
            _cache.Set(CacheKey, existingList, TimeSpan.FromMinutes(30));
        }
    }
}
