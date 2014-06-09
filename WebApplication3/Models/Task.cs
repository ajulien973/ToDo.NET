using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace WebApplication3.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime Datetime { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }
        public int Importance { get; set; }
        public int Urgency { get; set; }

        public string UserId { get; set; }
    }

    public class TaskDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }


}