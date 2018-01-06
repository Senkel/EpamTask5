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
    public class SaleInfoesController : Controller
    {
        // GET: SaleInfoes
       
        [HttpGet]
        public ActionResult Index()
        {
            var items = new SaleRepository()
                .GetAll()
                .Select(x => new SaleInfo()
                {
                    Id = x.Id,
                    Sum = x.Sum,
                    Date = x.SaleDate,
                    IdManager =(int)x.ID_Manager,
                    IdClient = (int)x.ID_Client,
                    IdProduct = (int)x.ID_Product
                }).ToArray();
            return View(items);
        }

    }
}
