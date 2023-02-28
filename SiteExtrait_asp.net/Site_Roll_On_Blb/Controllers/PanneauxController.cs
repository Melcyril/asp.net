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
    public class PanneauxController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        Workflow wkf = new Workflow();
        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);
        // GET: Panneaux
        public ActionResult Index()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                return View(db.Panneau.ToList());
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Panneaux/Details/5
        public ActionResult Details(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Panneau panneau = db.Panneau.Find(id);
                if (panneau == null)
                {
                    return HttpNotFound();
                }
                return View(panneau);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Panneaux/Create
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

        // POST: Panneaux/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPanneau,textePanneau,OrdrePanneau,visiblePanneau")] Panneau panneau)
        {
            if (ModelState.IsValid)
            {
                db.Panneau.Add(panneau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(panneau);
        }

        // GET: Panneaux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Panneau panneau = db.Panneau.Find(id);
                if (panneau == null)
                {
                    return HttpNotFound();
                }
                return View(panneau);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Panneaux/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPanneau,textePanneau,OrdrePanneau,visiblePanneau")] Panneau panneau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(panneau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(panneau);
        }

        // GET: Panneaux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Panneau panneau = db.Panneau.Find(id);
                if (panneau == null)
                {
                    return HttpNotFound();
                }
                return View(panneau);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Panneaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Panneau panneau = db.Panneau.Find(id);
            db.Panneau.Remove(panneau);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult _Panneau()
        {
            return PartialView();
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
