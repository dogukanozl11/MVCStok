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
            var ktg = db.TBLKATEGORI.Where(m=>m.KATEGORIID==p1.TBLKATEGORI.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORI=ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL (int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degeler = (from i in db.TBLKATEGORI.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.KATEGORIAD,
                                                Value = i.KATEGORIID.ToString()
                                            }).ToList();
            ViewBag.dgr = degeler;
            return View("UrunGetir",urun);
        }
        public ActionResult Guncelle (TBLURUNLER p1)
        {
            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            urun.MARKA = p1.MARKA;
            urun.STOK = p1.STOK;
            urun.FIYAT = p1.FIYAT;
            //urun.URUNKATEGORI = p1.URUNKATEGORI;
            var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == p1.TBLKATEGORI.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction ("Index");
        }
    }
}