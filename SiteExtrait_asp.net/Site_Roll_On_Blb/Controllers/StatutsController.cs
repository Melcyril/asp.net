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
    public class StatutsController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        Workflow wkf = new Workflow();
        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);
        // GET: Statuts
        public ActionResult Index()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                return View(db.Statut.ToList());
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Statuts/Details/5
        public ActionResult Details(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Statut statut = db.Statut.Find(id);
                if (statut == null)
                {
                    return HttpNotFound();
                }
                return View(statut);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Statuts/Create
        public ActionResult Create()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                return View();
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Statuts/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idStatut,nomStatut,accesStatut")] Statut statut)
        {
            if (ModelState.IsValid)
            {
                db.Statut.Add(statut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statut);
        }

        // GET: Statuts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Statut statut = db.Statut.Find(id);
                if (statut == null)
                {
                    return HttpNotFound();
                }
                return View(statut);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Statuts/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idStatut,nomStatut,accesStatut")] Statut statut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statut);
        }

        // GET: Statuts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Statut statut = db.Statut.Find(id);
                if (statut == null)
                {
                    return HttpNotFound();
                }
                return View(statut);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Statuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statut statut = db.Statut.Find(id);
            db.Statut.Remove(statut);
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
