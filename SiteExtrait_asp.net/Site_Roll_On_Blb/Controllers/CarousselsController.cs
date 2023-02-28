using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Site_Roll_On_Blb.Models;

namespace Site_Roll_On_Blb.Controllers
{
    public class CarousselsController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        Workflow wkf = new Workflow();
        string cheminImagesCaroussel = ConfigurationManager.AppSettings["cheminImagesCaroussel"];
        string cheminImagesMiniCaroussel = ConfigurationManager.AppSettings["cheminImagesMiniCaroussel"];
        int visibleCaroussel = Int32.Parse(ConfigurationManager.AppSettings["visibleCaroussel"]);
        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);

        // GET: Caroussels
        public ActionResult Index()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                return View(db.Caroussel.ToList());
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Caroussels/Details/5
        public ActionResult Details(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Caroussel caroussel = db.Caroussel.Find(id);
                if (caroussel == null)
                {
                    return HttpNotFound();
                }
                return View(caroussel);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Caroussels/Create
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

        // POST: Caroussels/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCaroussel,visibleCaroussel,urlminiCaroussel,altCaroussel,urlCaroussel")] Caroussel caroussel)
        {
            string extension = "";
            if (Request.Files.Count > 0)
            {
                string path = cheminImagesCaroussel;
                string pathmini = cheminImagesMiniCaroussel;
                HttpPostedFileBase fichier = Request.Files["filesCaroussel"];
                caroussel.urlCaroussel = path + fichier.FileName;
                caroussel.urlminiCaroussel= pathmini+ fichier.FileName;
                if (fichier.FileName != "")
                {
                    extension = wkf.Extension(fichier.FileName);
                    if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                    {
                        FileUpload(fichier);
                        FileUploadMini(fichier);
                    }
                    else
                    {
                        caroussel.urlCaroussel = "";
                    }
                }
                else
                {
                    caroussel.urlCaroussel = "";
                }

                if (ModelState.IsValid)
                {
                    db.Caroussel.Add(caroussel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }


            return View(caroussel);
        }
        public ActionResult FileUpload(HttpPostedFileBase file)
        {

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/Caroussel"), pic);
                // file is uploaded
                file.SaveAs(path);
                WebImage img = new WebImage(path);
                if (img.Width > 1350)
                {
                    img.Resize(1349, 400, preserveAspectRatio: true, preventEnlarge: true);
                }
                img.Save(path);
            }

            return RedirectToAction("Details");
        }
        public ActionResult FileUploadMini(HttpPostedFileBase file)
        {

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/Caroussel/Mini"), pic);
                // file is uploaded
                file.SaveAs(path);
                WebImage img = new WebImage(path);
                if (img.Width > 502)
                {
                    img.Resize(500,148, preserveAspectRatio: true, preventEnlarge: true);
                }
                img.Save(path);
            }

            return RedirectToAction("Details");
        }
        // GET: Caroussels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Caroussel caroussel = db.Caroussel.Find(id);
                if (caroussel == null)
                {
                    return HttpNotFound();
                }
                return View(caroussel);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Caroussels/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCaroussel,visibleCaroussel,urlminiCaroussel,altCaroussel,urlCaroussel")] Caroussel caroussel)
        {
            string extension = "";
            if (Request.Files.Count > 0)
            {
                string path = cheminImagesCaroussel;
                string pathmini = cheminImagesMiniCaroussel;
                HttpPostedFileBase fichier = Request.Files["filesCaroussel"];
                

                if (fichier.FileName != "")
                {
                    caroussel.urlminiCaroussel = pathmini + fichier.FileName;
                    caroussel.urlCaroussel = path + fichier.FileName;
                    extension = wkf.Extension(fichier.FileName);
                    if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                    {
                        FileUpload(fichier);
                        FileUploadMini(fichier);
                    }

                }
            }
                if (ModelState.IsValid)
                {
                    db.Entry(caroussel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
          
            return View(caroussel);
        }

        // GET: Caroussels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Caroussel caroussel = db.Caroussel.Find(id);
                if (caroussel == null)
                {
                    return HttpNotFound();
                }
                return View(caroussel);
                }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Caroussels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Caroussel caroussel = db.Caroussel.Find(id);
            db.Caroussel.Remove(caroussel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult _Caroussel()
        {
            ViewBag.caroussel = (from c in db.Caroussel where  c.visibleCaroussel== visibleCaroussel orderby c.idCaroussel ascending select c).ToList().AsEnumerable();
            return PartialView();
        }
        public PartialViewResult _CarousselMini()
        {
            ViewBag.carousselMini = (from c in db.Caroussel where c.visibleCaroussel == visibleCaroussel orderby c.idCaroussel ascending select c).ToList().AsEnumerable();
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
