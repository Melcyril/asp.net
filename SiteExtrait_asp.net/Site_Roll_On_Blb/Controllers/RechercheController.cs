using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site_Roll_On_Blb.Models;

namespace Site_Roll_On_Blb.Controllers
{
    public class RechercheController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        Workflow wkf = new Workflow();
        int clefinscrit = Int32.Parse(ConfigurationManager.AppSettings["clefinscrit"]);
        int clefadherant = Int32.Parse(ConfigurationManager.AppSettings["clefadherant"]);
        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);
        public ActionResult Index()
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

        public PartialViewResult rechercher(string recherche)
        {
            if (Session["niveauStatut"] == null || Int32.Parse(Session["niveauStatut"].ToString()) == 0)
            {
                ViewBag.Myrecherche = (from p in db.Publication where p.titrePublication.Contains(recherche) || p.sousTitrePublication.Contains(recherche) || p.contenuPublication.Contains(recherche) && p.Statut.accesStatut < clefinscrit select p).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 1)
            {
                ViewBag.Myrecherche = (from p in db.Publication where p.titrePublication.Contains(recherche) || p.sousTitrePublication.Contains(recherche) || p.contenuPublication.Contains(recherche) && p.Statut.accesStatut <= clefinscrit select p).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 10)
            {
                ViewBag.Myrecherche = (from p in db.Publication where p.titrePublication.Contains(recherche) || p.sousTitrePublication.Contains(recherche) || p.contenuPublication.Contains(recherche) && p.Statut.accesStatut <= clefadherant select p).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 20)
            {
                ViewBag.Myrecherche = (from p in db.Publication where p.titrePublication.Contains(recherche) || p.sousTitrePublication.Contains(recherche) || p.contenuPublication.Contains(recherche) && p.Statut.accesStatut <=ClefAdmin select p).ToList().AsEnumerable();
            }

            TempData["Marecherche"] = ViewBag.Marecherche;

            return PartialView(new ViewDataDictionary { { "Marecherche", ViewData["Marecherche"] } });

        }

    }
}