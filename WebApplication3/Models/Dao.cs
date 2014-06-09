using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using WebGrease;

namespace WebApplication3.Models
{

    public class Dao
    {
        
    }

    public class TaskDao : Dao 
    {

        private TaskDbContext db = new TaskDbContext();

        public List<Task> List(string userId)
        {
            var tasks = from m in db.Tasks
                        where m.UserId == userId
                        select m;
            return tasks.ToList();

            //tasks = db.Tasks.ToList().Where(item => item.UserId == User.Identity.GetUserId()).ToList();
        }

        public List<Task> List(string userId, string category)
        {
            //var tasks = from m in db.Tasks
             //           where m.Category == category
               //         select m;
            var tasks =
                db.Tasks.ToList()
                    .Where(item => item.Category == category && item.UserId == userId);
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
            var catQuery = db.Tasks.ToList().OrderBy(d => d.Category).Select(d => d.Category);
            var catLst = new List<string>();
            catLst.AddRange(catQuery.Distinct());

            return catLst;
        }
    }

}