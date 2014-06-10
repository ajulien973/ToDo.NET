using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI.WebControls.Expressions;
using WebGrease;

namespace WebApplication3.Models
{

    public interface Dao<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(int? entityId);
        List<T> List();
    }

    public class TaskDao : Dao<Task>
    {

        private TaskDbContext db = new TaskDbContext();

        public void Create(Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void Update(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Task task)
        {
            db.Tasks.Remove(task);
            db.SaveChanges();
        }


        public Task Get(int? taskId)
        {
            return db.Tasks.Find(taskId);
        }

        public List<Task> List()
        {
            var tasks = db.Tasks.ToList().OrderBy(d => d.Datetime).ThenBy(d => d.Name);
            return tasks.ToList();
        }

        public List<Task> List(string userId)
        {
            //var tasks = from m in db.Tasks
            //            where m.UserId == userId
            //            orderby m.Datetime
            //            select m;
            var tasks = db.Tasks.ToList().Where(d => d.UserId == userId).OrderBy(d => d.Datetime).ThenBy(d => d.Name);
            return tasks.ToList();

            //tasks = db.Tasks.ToList().Where(item => item.UserId == User.Identity.GetUserId()).ToList();
        }

        public List<Task> List(string userId, List<string> category)
        {
            //var tasks = from m in db.Tasks
             //           where m.Category == category
               //         select m;
            var tasks =
                db.Tasks.ToList()
                    .Where(item => category.Contains(item.Category) && item.UserId == userId);

            return tasks.ToList();
        }

        public List<Task> Search(string userId, string search)
        {
            var tasks =
                db.Tasks.ToList()
                    .Where(item => item.Category.Contains(search) || item.Description.Contains(search) || item.Name.Contains(search));
            return tasks.ToList();
        } 

        public List<string> ListCategories()
        {
            //var catQuery = from d in db.Tasks
            //               orderby d.Category
            //               select d.Category;
            var catQuery = db.Tasks.ToList().OrderBy(d => d.Category).Select(d => d.Category).Distinct();
            var catLst = new List<string>();
            catLst.AddRange(catQuery);

            return catLst;
        }
    }

}