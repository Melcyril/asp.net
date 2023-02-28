using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site_Roll_On_Blb.Models;

namespace Site_Roll_On_Blb.Controllers
{
    public class HomeController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        Workflow wkf = new Workflow();
        int visibleCaroussel = Int32.Parse(ConfigurationManager.AppSettings["visibleCaroussel"]);
        int newstypePublication = Int32.Parse(ConfigurationManager.AppSettings["newstypePublication"]);
        int eventtypePublication = Int32.Parse(ConfigurationManager.AppSettings["eventtypePublication"]);
        int clefvisiteur= Int32.Parse(ConfigurationManager.AppSettings["clefvisiteur"]);
        int clefinscrit = Int32.Parse(ConfigurationManager.AppSettings["clefinscrit"]);
        int clefadherant = Int32.Parse(ConfigurationManager.AppSettings["clefadherant"]);
        int clefadmin = Int32.Parse(ConfigurationManager.AppSettings["AccesStatut"]);
        public ActionResult Index(int? page)
        {

            //var merde = "";
            if (HttpContext.Request.Cookies["BlbCookie"] != null && Session["niveauStatut"] == null )
            {

                Response.Write("<script language='javascript'>window.location=window.location</script>");
            }

            var dummyItems = (from p in db.Publication where p.TypePublication.idTypePublication == newstypePublication orderby p.datePostPublication descending select p).ToList().AsEnumerable();
            if (Session["niveauStatut"]==null || Int32.Parse(Session["niveauStatut"].ToString()) == 0)
            {
                dummyItems = (from p in db.Publication where p.TypePublication.idTypePublication == newstypePublication && p.Statut.accesStatut < clefinscrit orderby p.datePostPublication descending select p).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 1)
            {
                dummyItems = (from p in db.Publication where p.TypePublication.idTypePublication == newstypePublication && p.Statut.accesStatut<= clefinscrit orderby p.datePostPublication descending select p).ToList().AsEnumerable();
            }else if (Int32.Parse(Session["niveauStatut"].ToString()) == 10)
            {
                dummyItems = (from p in db.Publication where p.TypePublication.idTypePublication == newstypePublication && p.Statut.accesStatut <= clefadherant orderby p.datePostPublication descending select p).ToList().AsEnumerable();
            }else if (Int32.Parse(Session["niveauStatut"].ToString()) == 20)
            {
                dummyItems = (from p in db.Publication where p.TypePublication.idTypePublication == newstypePublication && p.Statut.accesStatut <= clefadmin orderby p.datePostPublication descending select p).ToList().AsEnumerable();
            }

                //ViewBag.News = (from p in db.Publication where p.TypePublication.idTypePublication == 1 orderby p.datePostPublication descending select p).ToList().AsEnumerable();

            var pager = new Pager(dummyItems.Count(), page);

            var viewModel = new IndexViewModel
            {
                ItemsNews = dummyItems.Skip((pager.CurrentPage - 1) * pager.ElementPage).Take(pager.ElementPage),
                Pager = pager
            };

            List<PublicationImage> images = (from p in db.PublicationImage select p).ToList();
            List<PublicationImage> listeImages = new List<PublicationImage>();
            if (images != null) { 
                foreach (var item in viewModel.ItemsNews)
                {
                    foreach (var pic in images)
                    {
                        if (pic.idPublication == item.idPublication)
                        {
                            listeImages.Add(pic);
                        }

                    }
                }
            }


            ViewBag.ListeImages = listeImages;


            ViewBag.caroussel = (from c in db.Caroussel where  c.visibleCaroussel == visibleCaroussel orderby c.idCaroussel ascending select c).ToList().AsEnumerable();
            ViewBag.carousselMini = (from c in db.Caroussel where c.visibleCaroussel == visibleCaroussel orderby c.idCaroussel ascending select c).ToList().AsEnumerable();
            ViewBag.EvenementFutureDernier = (from p in db.Publication where p.TypePublication.idTypePublication == eventtypePublication & p.datePrevisionPublication >DateTime.Now orderby p.datePrevisionPublication ascending select p).ToList().AsEnumerable();

            ViewBag.EvenementRealiseDernier = (from p in db.Publication where p.TypePublication.idTypePublication == eventtypePublication & p.datePrevisionPublication < DateTime.Now orderby p.datePrevisionPublication descending select p).ToList().AsEnumerable();
            TempData["Caroussel"] = ViewBag.caroussel;
            TempData["EvtFuturDernier"] = ViewBag.EvenementFutureDernier;
            TempData["EvtRealiseDernier"] = ViewBag.EvenementRealiseDernier;
            TempData["News"] = viewModel.ItemsNews;
            
            return View(viewModel);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}



        //Deconnexion de l'utilisateur
        public ActionResult Deconnexion()
        {
            //Expiration du cookie
            Utilisateur utilisateur = new Utilisateur();
            HttpCookie htc = new HttpCookie("BlbCookie");
            DateTime dtExpireCookie = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
            htc.Expires = dtExpireCookie;
            Response.Cookies.Add(htc);
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}