using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;

namespace MVCStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degeler = (from i in db.TBLKATEGORI.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.KATEGORIAD,
                                                Value = i.KATEGORIID.ToString()
                                            }).ToList();
            ViewBag.dgr=degeler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return View();
        }
    }
}