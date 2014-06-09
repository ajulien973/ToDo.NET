using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class TaskController : Controller
    {
        private TaskDbContext db = new TaskDbContext();
        private TaskDao dao = new TaskDao();

        public TaskController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public TaskController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }


        // GET: /Task/
        [Authorize]
        public ActionResult Index(string category, string searchString)
        {
            // List all distinct categories
            ViewBag.category = new SelectList(dao.ListCategories());

            // List all tasks not filtered
            var tasks = new List<Task>();
            tasks = dao.List(User.Identity.GetUserId());
            
            // If a category is selected, filter by this category
            if (!string.IsNullOrEmpty(category))
            {
                tasks = dao.List(User.Identity.GetUserId(), category);
            }

            // Search bar
            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = dao.Search(User.Identity.GetUserId(), searchString);
            }
            return View(tasks);
        }

        // GET: /Task/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = dao.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: /Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Datetime,Description,Category,Importance,Urgency,UserId")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.UserId = User.Identity.GetUserId();
                dao.Create(task);
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: /Task/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = dao.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: /Task/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Datetime,Description,Category,Importance,Urgency,UserId")] Task task)
        {
            if (ModelState.IsValid)
            {
                dao.Update(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: /Task/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = dao.Get(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: /Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dao.Delete(dao.Get(id));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
