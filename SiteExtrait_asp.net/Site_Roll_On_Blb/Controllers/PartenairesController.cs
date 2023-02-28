using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Site_Roll_On_Blb.Models;

namespace Site_Roll_On_Blb.Controllers
{
    public class PartenairesController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        string cheminImagesPartenaires = ConfigurationManager.AppSettings["cheminImagesPartenaires"];
        int visiblePartenaire = Int32.Parse(ConfigurationManager.AppSettings["visiblePartenaire"]);
        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);
        Workflow wkf = new Workflow();
        // GET: Partenaires
        public ActionResult Index()
        {

            if (wkf.gestionConnexion(ClefAdmin))
            {
                return View(db.Partenaire.ToList());
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Partenaires/Details/5
        public ActionResult Details(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Partenaire partenaire = db.Partenaire.Find(id);
                if (partenaire == null)
                {
                    return HttpNotFound();
                }
                return View(partenaire);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Partenaires/Create
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

        // POST: Partenaires/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPartenaire,nomPartenaire,urlPartenaire,lienPartenaire,altPartenaire,OrdrePartenaire,visiblePartenaire")] Partenaire partenaire)
        {
            string extension = "";
            if (Request.Files.Count > 0)
            {
                string path = cheminImagesPartenaires;
                HttpPostedFileBase fichier = Request.Files["filesPartenaire"];
                partenaire.urlPartenaire = path + fichier.FileName;
                extension = wkf.Extension(fichier.FileName);
                if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                {
                    FileUpload(fichier);
                }
                else
                {
                    partenaire.urlPartenaire = "";
                }
            }
                if (ModelState.IsValid)
                {
                    db.Partenaire.Add(partenaire);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            
            return View(partenaire);
        }
        public ActionResult FileUpload(HttpPostedFileBase file)
        {

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                Server.MapPath("~/Content/Partenaires"), pic);
                // file is uploaded
                file.SaveAs(path);
                WebImage img = new WebImage(path);
                if (img.Width > 220)
                {
                    img.Resize(211, 105, false, false);
                }
                img.Save(path);
            }

            return RedirectToAction("Details");
        }
        // GET: Partenaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Partenaire partenaire = db.Partenaire.Find(id);
                if (partenaire == null)
                {
                    return HttpNotFound();
                }
                return View(partenaire);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Partenaires/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPartenaire,nomPartenaire,urlPartenaire,lienPartenaire,altPartenaire,OrdrePartenaire,visiblePartenaire")] Partenaire partenaire)
        {
            string extension = "";
            if (Request.Files.Count > 0)
            {
                string path = cheminImagesPartenaires;
                HttpPostedFileBase fichier = Request.Files["filesPartenaire"];
                partenaire.urlPartenaire = path + fichier.FileName;
                extension = wkf.Extension(fichier.FileName);
                if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                {
                    FileUpload(fichier);
                
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(partenaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partenaire);
        }

        // GET: Partenaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Partenaire partenaire = db.Partenaire.Find(id);
                if (partenaire == null)
                {
                    return HttpNotFound();
                }
                return View(partenaire);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Partenaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partenaire partenaire = db.Partenaire.Find(id);
            db.Partenaire.Remove(partenaire);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult _Partenaire()
        {
            ViewBag.Partenaire = (from p in db.Partenaire where p.visiblePartenaire == visiblePartenaire orderby p.OrdrePartenaire ascending select p).ToList().AsEnumerable();
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
