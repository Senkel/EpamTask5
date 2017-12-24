using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EpamTask4.Repository;
using EpamTask4;

namespace Task_5.Controllers
{
    public class ProductsController : Controller
    {

        // GET: Products
        public ActionResult Index()
        {
            var products = new ProductRepository()
                .GetAll()
                .GroupBy(p => p.Title.Substring(0, 1))
                .Select(p => new { k = p.Key, value = p })
                .ToDictionary(
                    p => p.k,
                    p => p.value.Select(
                        x => new EpamTask5.Models.Product()
                        { Id = x.Id, Title = x.Title }));
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productId = id ?? default(int);

            var product = new ProductRepository()
                .GetById(productId)
                .Select(y => new EpamTask5.Models.Product() { Id = y.Id, Title = y.Title })
                .FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize]
        [Authorize(Roles = "Administrator")]
        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,Title")] EpamTask5.Models.Product product)
        {
            if (ModelState.IsValid)
            {
                var item = new EpamTask4.Product() { Id = product.Id, Title = product.Title };
                new ProductRepository()
                    .Insert(item);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productId = id ?? default(int);

            var product = new ProductRepository()
                .GetById(productId).Select(x => new EpamTask5.Models.Product() { Id = x.Id, Title = x.Title })
                .FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Title")] EpamTask5.Models.Product product)
        {
            if (ModelState.IsValid)
            {
                var item = new ProductRepository()
                    .GetById(product.Id)
                    .FirstOrDefault();

                if (item != null)
                {
                    item.Title = product.Title;
                    new ProductRepository()
                        .Update(item);

                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productId = id ?? default(int);

            var product = new ProductRepository()
                .GetById(productId)
                .Select(m => new EpamTask5.Models.Product() { Id = m.Id, Title = m.Title })
                .FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = new ProductRepository()
                .GetById(id)
                .FirstOrDefault();

            new ProductRepository()
                .Remove(product);

            return RedirectToAction("Index");
        }

    }
}
