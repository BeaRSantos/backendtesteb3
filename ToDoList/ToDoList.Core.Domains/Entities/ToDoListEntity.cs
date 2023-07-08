using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Core.Domain.Entities
{
    public class ToDoListEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Description { get; set; }

        public ToDoListEntity(int id, string name, DateTime startDate, DateTime finishDate, string description)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            FinishDate = finishDate;
            Description = description;
        }
    }
}
