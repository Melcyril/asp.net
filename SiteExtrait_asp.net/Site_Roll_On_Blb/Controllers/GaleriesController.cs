using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Site_Roll_On_Blb.Models;
using System.Web.Helpers;
using System.Configuration;

namespace Site_Roll_On_Blb.Controllers
{
    public class GaleriesController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        int visibleGalerie= Int32.Parse(ConfigurationManager.AppSettings["visibleGalerie"]);
        int touTypeGalerie = Int32.Parse(ConfigurationManager.AppSettings["touTypeGalerie"]);
        int accesStatut = Int32.Parse(ConfigurationManager.AppSettings["accesStatut"]);
        string cheminImagesGaleries = ConfigurationManager.AppSettings["cheminImagesGaleries"];
        int clefvisiteur = Int32.Parse(ConfigurationManager.AppSettings["clefvisiteur"]);
        int clefinscrit = Int32.Parse(ConfigurationManager.AppSettings["clefinscrit"]);
        int clefadherant = Int32.Parse(ConfigurationManager.AppSettings["clefadherant"]);
        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);
        Workflow wkf = new Workflow();
        // GET: Galeries
        public ActionResult Index()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                var galerie = db.Galerie.Include(g => g.Sport).Include(g=>g.Statut).Include(g=>g.TypeGalerie) ;
                return View(galerie.ToList());
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Galeries/Details/5
        public ActionResult Details(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Galerie galerie = db.Galerie.Find(id);
                if (galerie == null)
                {
                    return HttpNotFound();
                }
                return View(galerie);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Galeries/Create
        public ActionResult Create()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                ViewBag.idSport = new SelectList(db.Sport, "idSport", "nomSport");
                ViewBag.idTypeGalerie = new SelectList(db.TypeGalerie, "idTypeGalerie", "nomTypeGalerie");
                ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");
                return View();
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Galeries/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idGalerie,datePostGalerie,idSport,urlGalerie,AltGalerie,nomGalerie,ordreGalerie,idStatut,idTypeGalerie,visibleGalerie")] Galerie galerie)
        {
            string extension = "";
            if (Request.Files.Count > 0)
            {
              
                string path = cheminImagesGaleries;
                HttpPostedFileBase fichier = Request.Files["filesGaleries"];
                galerie.urlGalerie = path + fichier.FileName;
                galerie.ordreGalerie = (from g in db.Galerie where g.visibleGalerie == visibleGalerie && g.idTypeGalerie == touTypeGalerie orderby g.datePostGalerie descending select g).Count()+1;
                if (fichier.FileName != "")
                {
                    extension = wkf.Extension(fichier.FileName);
                    if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png") || extension.Equals("3gp") || extension.Equals("avi") || extension.Equals("mov") || extension.Equals("mp4"))
                    {
                        FileUpload(fichier);
                        if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                        {
                            FileUploadmini(fichier);
                        }
                    }
                    else
                    {
                        galerie.urlGalerie = "";
                    }
                }
                else
                {
                    galerie.urlGalerie = "";
                }

            }
            galerie.datePostGalerie = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Galerie.Add(galerie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idSport = new SelectList(db.Sport, "idSport", "nomSport");
            ViewBag.idTypeGalerie = new SelectList(db.TypeGalerie, "idTypeGalerie", "nomTypeGalerie");
            ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");
            return View(galerie);
        }
        public ActionResult FileUpload(HttpPostedFileBase file)
        {

            if (file.FileName != null)
            {
                string extension = "";
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                Server.MapPath("~/Content/Galeries"), pic);
                // file is uploaded
                file.SaveAs(path);
                extension = wkf.Extension(file.FileName);
                if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                {
                    WebImage img = new WebImage(path);
                    if (img.Width > 1080)
                    {
                        img.Resize(1080, 800, true , false);
                    }
                    img.Save(path);
                }

            }

            return RedirectToAction("Details");
        }
        public ActionResult FileUploadmini(HttpPostedFileBase file)
        {

            if (file.ToString() != "")
            {
                string extension = "";
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                Server.MapPath("~/Content/Galeries/mini"), pic);
                // file is uploaded
                file.SaveAs(path);

                extension = wkf.Extension(file.FileName);
                if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                {
                    WebImage img = new WebImage(path);
                    if (img.Width > 180)
                    {
                        img.Resize(180, 135, true, false);
                    }
                    img.Save(path);
                }

            }

            return RedirectToAction("Details");
        }
        // GET: Galeries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Galerie galerie = db.Galerie.Find(id);
                if (galerie == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idSport = new SelectList(db.Sport, "idSport", "nomSport", galerie.idSport);
                ViewBag.idTypeGalerie = new SelectList(db.TypeGalerie, "idTypeGalerie", "nomTypeGalerie");
                ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "idStatut");
                return View(galerie);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Galeries/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idGalerie,datePostGalerie,idSport,urlGalerie,AltGalerie,nomGalerie,ordreGalerie,idStatut,idTypeGalerie,visibleGalerie")] Galerie galerie)
        {
            string extension = "";
            if (Request.Files.Count > 0)
            {
                string path = cheminImagesGaleries;
                HttpPostedFileBase fichier = Request.Files["filesGaleries"];
                galerie.ordreGalerie = (from g in db.Galerie where g.visibleGalerie == visibleGalerie && g.idTypeGalerie == touTypeGalerie orderby g.datePostGalerie descending select g).Count();
                if (fichier.FileName != "")
                {
                    galerie.urlGalerie = path + fichier.FileName;
                    extension = wkf.Extension(fichier.FileName);
                    if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png") || extension.Equals("3gp") || extension.Equals("avi") || extension.Equals("mov") || extension.Equals("mp4"))
                    {
                        FileUpload(fichier);
                        if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                        {
                            FileUploadmini(fichier);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(galerie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSport = new SelectList(db.Sport, "idSport", "nomSport");
            ViewBag.idTypeGalerie = new SelectList(db.TypeGalerie, "idTypeGalerie", "nomTypeGalerie");
            ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");
            return View(galerie);
        }

        // GET: Galeries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Galerie galerie = db.Galerie.Find(id);
                if (galerie == null)
                {
                    return HttpNotFound();
                }
                return View(galerie);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Galeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Galerie galerie = db.Galerie.Find(id);
            db.Galerie.Remove(galerie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult _Galerie(int? page)
        {
            var dummyItems = (from g in db.Galerie where g.visibleGalerie == visibleGalerie && g.idTypeGalerie== touTypeGalerie && g.Statut.accesStatut>=accesStatut orderby g.datePostGalerie descending select g).ToList().AsEnumerable();
            if (Session["niveauStatut"] == null || Int32.Parse(Session["niveauStatut"].ToString()) == 0)
            {
                dummyItems = (from g in db.Galerie where g.visibleGalerie == visibleGalerie && g.idTypeGalerie == touTypeGalerie && g.Statut.accesStatut < clefinscrit orderby g.datePostGalerie descending select g).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 1)
            {
                dummyItems = (from g in db.Galerie where g.visibleGalerie == visibleGalerie && g.idTypeGalerie == touTypeGalerie && g.Statut.accesStatut <= clefinscrit orderby g.datePostGalerie descending select g).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 10)
            {
                dummyItems = (from g in db.Galerie where g.visibleGalerie == visibleGalerie && g.idTypeGalerie == touTypeGalerie && g.Statut.accesStatut <=clefadherant orderby g.datePostGalerie descending select g).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 20)
            {
                dummyItems = (from g in db.Galerie where g.visibleGalerie == visibleGalerie && g.idTypeGalerie == touTypeGalerie && g.Statut.accesStatut <=ClefAdmin orderby g.datePostGalerie descending select g).ToList().AsEnumerable();
            }
            var pager1 = new Pager1(dummyItems.Count(), page);

            var viewModel = new IndexViewModel
            {
                ItemsGalerie = dummyItems.Skip((pager1.CurrentPage - 1) * pager1.ElementPage).Take(pager1.ElementPage),
                Pager1 = pager1
            };
            return View(viewModel);
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
