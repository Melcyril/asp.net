using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Site_Roll_On_Blb.Models;

namespace Site_Roll_On_Blb.Controllers
{
    public class PublicationImagesController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        Workflow wkf = new Workflow();
        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);
        // GET: PublicationImages
        public ActionResult Index()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                var publicationImage = db.PublicationImage.Include(p => p.Publication);
                return View(publicationImage.ToList());
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: PublicationImages/Details/5
        public ActionResult Details(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PublicationImage publicationImage = db.PublicationImage.Find(id);
                if (publicationImage == null)
                {
                    return HttpNotFound();
                }
                return View(publicationImage);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: PublicationImages/Create
        public ActionResult Create()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                ViewBag.idPublication = new SelectList(db.Publication, "idPublication", "titrePublication");
                return View();
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: PublicationImages/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPublicationImage,urlPublicationImage,idPublication")] PublicationImage publicationImage)
        {
            if (ModelState.IsValid)
            {
                db.PublicationImage.Add(publicationImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPublication = new SelectList(db.Publication, "idPublication", "titrePublication", publicationImage.idPublication);
            return View(publicationImage);
        }

        // GET: PublicationImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PublicationImage publicationImage = db.PublicationImage.Find(id);
                if (publicationImage == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idPublication = new SelectList(db.Publication, "idPublication", "titrePublication", publicationImage.idPublication);
                return View(publicationImage);
            }
            else
            {
                return RedirectToAction("index", "home");
            }

        }

        // POST: PublicationImages/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPublicationImage,urlPublicationImage,idPublication")] PublicationImage publicationImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicationImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPublication = new SelectList(db.Publication, "idPublication", "titrePublication", publicationImage.idPublication);
            return View(publicationImage);
        }

        // GET: PublicationImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PublicationImage publicationImage = db.PublicationImage.Find(id);
                if (publicationImage == null)
                {
                    return HttpNotFound();
                }
                return View(publicationImage);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: PublicationImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PublicationImage publicationImage = db.PublicationImage.Find(id);
            db.PublicationImage.Remove(publicationImage);
            db.SaveChanges();
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
