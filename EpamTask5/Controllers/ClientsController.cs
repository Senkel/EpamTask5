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
    public class ClientsController : Controller
    {
        // GET: Clients
        public ActionResult Index()
        {
            var items = new ClientRepository()
               .GetAll()
               .OrderBy(a=>a.ClientName)
               .Select(x => new Client()
               {
                   Id = x.Id,
                   Name= x.ClientName
               }).ToArray();

            return View(items);
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientId = id ?? default(int);

            var client = new ClientRepository()
                .GetById(clientId)
                .Select(m => new EpamTask5.Models.Client() { Id = m.Id, Name = m.ClientName })
                .FirstOrDefault();

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                var client = new ClientRepository()
                .GetById(id)
                .FirstOrDefault();

                new ClientRepository()
                    .Remove(client);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Denied()
        {
            return View();
        }

        [Authorize]
        [Authorize(Roles = "Administrator")]
        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Name")] EpamTask5.Models.Client client)
        {
            if (ModelState.IsValid)
            {
                var item = new EpamTask4.Client() { Id = client.Id, ClientName = client.Name };
                new ClientRepository()
                    .Insert(item);
                return RedirectToAction("Index");
            }

            return View(client);
        }
    }
}
