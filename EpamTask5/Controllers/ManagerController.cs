using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EpamTask5.Models;
using EpamTask4.Repository;

namespace EpamTask5.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {


        // GET: Manager
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manager/Details/5
        [Ajax]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var managerId = id ?? default(int);
            var manager = new ManagerRepository().GetById(managerId).Select(x => new Manager() { Id = x.Id, Name = x.ManagerName }).FirstOrDefault();
            if (manager == null)
            {
                return HttpNotFound();
            }
            return PartialView(manager);
        }

        // GET: Manager/Create
        [Ajax]
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Manager/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Ajax]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                var item = new EpamTask4.Manager() { Id = manager.Id, ManagerName = manager.Name };
                new ManagerRepository().Insert(item);
                return RedirectToAction("Index");
            }

            return View(manager);
        }

        [Ajax]
        [HttpGet]
        public ActionResult ManagerList()
        {
            var item = new ManagerRepository().GetAll().Select(x => new Manager() { Id = x.Id, Name = x.ManagerName });
            return PartialView("PartialManagerList", item);
        }

        // GET: Manager/Edit/5
        [Ajax]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var managerId = id ?? default(int);

            var manager = new ManagerRepository().GetById(managerId).Select(x => new Manager() { Id = x.Id, Name = x.ManagerName }).FirstOrDefault();

            if (manager == null)
            {
                return HttpNotFound();
            }
            return PartialView(manager);
        }

        // POST: Manager/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                var item = new ManagerRepository().GetById(manager.Id).FirstOrDefault();
                if (item != null)
                {
                    item.ManagerName = manager.Name;
                    new ManagerRepository().Update(item);
                }
                return RedirectToAction("Index");
            }
            return PartialView(manager);
        }

        // GET: Manager/Delete/5

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var managerId = id ?? default(int);

            var manager = new ManagerRepository().GetById(managerId).Select(x => new Manager() { Id = x.Id, Name = x.ManagerName }).FirstOrDefault();

            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Manager/Delete/5
        [HttpPost, ActionName("Delete")]

        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var item = new ManagerRepository().GetById(id).FirstOrDefault();
            new ManagerRepository().Remove(item);
            return RedirectToAction("Index");
        }

        [Ajax]
        public JsonResult GetChartData(int? id)
        {
            if (id == null)
            {
                var items = new ManagerRepository()
                    .GetAll()
                    .Select(d => new object[]
                    {
                        d.ManagerName,
                        new ManagerRepository()
                        .GetSumById(d.Id)
                    })
                    .ToArray();

                return Json(items, JsonRequestBehavior.AllowGet);
            }

            int idManager = id ?? default(int);
            Random rnd = new Random();
            var sales = new ManagerRepository()
                .GetSalesByManagerId(idManager)
                .OrderBy(x => x.SaleDate)
                .GroupBy(x => x.SaleDate.Date)
                .Select(d => new object[] { d.Key, d.Sum(s => s.Sum) })
                .ToArray();

            return Json(sales, JsonRequestBehavior.AllowGet);
        }
    }
}

