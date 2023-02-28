using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Site_Roll_On_Blb.Models;

namespace Site_Roll_On_Blb.Controllers
{
    public class UtilisateursController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        Workflow wkf = new Workflow();

        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);

        // GET: Utilisateurs
        public ActionResult Index()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                var utilisateur = db.Utilisateur.Include(u => u.Adhesion).Include(u => u.Sport).Include(u => u.Statut);
                return View(utilisateur.ToList());
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Utilisateur utilisateur = db.Utilisateur.Find(id);
                if (utilisateur == null)
                {
                    return HttpNotFound();
                }
                return View(utilisateur);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                ViewBag.idAdhesion = new SelectList(db.Adhesion, "idAdhesion", "nomAdhesion");
                ViewBag.idSport = new SelectList(db.Sport, "idSport", "nomSport");
                ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");
                return View();
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Utilisateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUtilisateur,mailUtilisateur,pseudoUtilisateur,nomUtilisateur,prenomUtilisateur,DateNaissanceUtilisateur,mdpUtilisateur,dateInscriptionUtilisateur,tokenUtilisateur,idAdhesion,idStatut,idSport")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Utilisateur.Add(utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAdhesion = new SelectList(db.Adhesion, "idAdhesion", "nomAdhesion", utilisateur.idAdhesion);
            ViewBag.idSport = new SelectList(db.Sport, "idSport", "nomSport", utilisateur.idSport);
            ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut", utilisateur.idStatut);
            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utilisateur utilisateur = db.Utilisateur.Find(id);
                if (utilisateur == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idAdhesion = new SelectList(db.Adhesion, "idAdhesion", "nomAdhesion", utilisateur.idAdhesion);
                ViewBag.idSport = new SelectList(db.Sport, "idSport", "nomSport", utilisateur.idSport);
                ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut", utilisateur.idStatut);
                return View(utilisateur);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Utilisateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUtilisateur,mailUtilisateur,pseudoUtilisateur,nomUtilisateur,prenomUtilisateur,DateNaissanceUtilisateur,mdpUtilisateur,dateInscriptionUtilisateur,tokenUtilisateur,idAdhesion,idStatut,idSport")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAdhesion = new SelectList(db.Adhesion, "idAdhesion", "nomAdhesion", utilisateur.idAdhesion);
            ViewBag.idSport = new SelectList(db.Sport, "idSport", "nomSport", utilisateur.idSport);
            ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut", utilisateur.idStatut);
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utilisateur utilisateur = db.Utilisateur.Find(id);
                if (utilisateur == null)
                {
                    return HttpNotFound();
                }
                return View(utilisateur);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.Utilisateur.Find(id);
            db.Utilisateur.Remove(utilisateur);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public ActionResult Login(string Email, string Password, string rememberMe)
        //{
         
        //    int idUtilisateur = (from u in db.Utilisateur where u.mailUtilisateur == Email && u.mdpUtilisateur == Password select u.idUtilisateur).DefaultIfEmpty(0).First();
        //    if (idUtilisateur > 0)
        //    {
        //        Utilisateur utilisateur = (from u in db.Utilisateur where u.mailUtilisateur == Email && u.mdpUtilisateur == Password select u).First();
        //        System.Web.HttpContext.Current.Session["nomStatut"] = utilisateur.Statut.nomStatut;
        //        System.Web.HttpContext.Current.Session["niveauStatut"] = utilisateur.Statut.accesStatut;
        //        System.Web.HttpContext.Current.Session["Email"] = utilisateur.mailUtilisateur;
        //        System.Web.HttpContext.Current.Session["Pseudo"] = utilisateur.pseudoUtilisateur;
        //        System.Web.HttpContext.Current.Session["IdUtilisateur"] = utilisateur.idUtilisateur;
        //        TempData["niveauStatut"] = utilisateur.Statut.accesStatut;
        //        if (rememberMe != null && rememberMe == "true")
        //        {
        //            HttpCookie htc = new HttpCookie("BlbCookie");
        //            DateTime dtExpireCookie = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
        //            htc.Expires = dtExpireCookie;
        //            int idUser = utilisateur.idUtilisateur;
        //            int resultID = 846 * idUser - 47;
        //            string userID = resultID.ToString() + "A8vg44";
        //            htc["IdUtilisateur"] = userID;
        //            Response.Cookies.Add(htc);
        //        }
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.Erreur = "Ce compte n'existe pas";
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        public ActionResult AjaxLogin(string Email, string Password, string rememberMe)
        {
            Statut statut=new Statut();
            var motdepasseCrypte = Workflow.getHashSha256(Password).Substring(0, 50);
            int idUtilisateur = (from u in db.Utilisateur where u.mailUtilisateur == u.mailUtilisateur && u.mdpUtilisateur == motdepasseCrypte &&u.Statut.accesStatut>0 select u.idUtilisateur).DefaultIfEmpty(0).First();
            int visiteur = (from u in db.Utilisateur where u.mailUtilisateur == u.mailUtilisateur && u.mdpUtilisateur == motdepasseCrypte && u.Statut.accesStatut == 0 select u.idUtilisateur).DefaultIfEmpty(0).First();

            //int idUtilisateur = (from u in db.Utilisateur where u.mailUtilisateur == u.mailUtilisateur && u.mdpUtilisateur == Password select u.idUtilisateur).DefaultIfEmpty(0).First();
            if (idUtilisateur > 0)
            {
                Utilisateur utilisateur = (from u in db.Utilisateur where u.mailUtilisateur == Email && u.mdpUtilisateur == motdepasseCrypte select u).First();
                //Utilisateur utilisateur = (from u in db.Utilisateur where u.mailUtilisateur == Email && u.mdpUtilisateur == Password select u).First();
                System.Web.HttpContext.Current.Session["nomStatut"] = utilisateur.Statut.nomStatut;
                System.Web.HttpContext.Current.Session["niveauStatut"] = utilisateur.Statut.accesStatut;            
                System.Web.HttpContext.Current.Session["Email"] = utilisateur.mailUtilisateur;
                System.Web.HttpContext.Current.Session["Pseudo"] = utilisateur.pseudoUtilisateur;
                System.Web.HttpContext.Current.Session["IdUtilisateur"] = utilisateur.idUtilisateur;
               
                //TempData["niveauStatut"]= Session["niveauStatut"];
                if (rememberMe != null && rememberMe == "true")
                {
                    HttpCookie htc = new HttpCookie("BlbCookie");
                    DateTime dtExpireCookie = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
                    htc.Expires = dtExpireCookie;
                    int idUser = utilisateur.idUtilisateur;
                    int resultID = 846 * idUser - 47;
                    string userID = resultID.ToString() + "A8vg44";
                    htc["IdUtilisateur"] = userID;
                    htc["niveauStatut"]= utilisateur.Statut.accesStatut.ToString();
                    Response.Cookies.Add(htc);
                }
                ViewBag.erreur = 1;

                return View();
            }
            else if (visiteur > 0)
            {
                //System.Web.HttpContext.Current.Session["visiteur"] = visiteur;
                //return RedirectToAction("Index", "Home");
                if (TempData["erreur"] == null)
                {
                    ViewBag.erreur = "Activez la confirmation de votre compte";
                    TempData["erreur"] = ViewBag.erreur;
                }
                else
                {
                    try { 
                    string token = Guid.NewGuid().ToString();
                    motdepasseCrypte = Workflow.getHashSha256(Password).Substring(0, 50);
                        Utilisateur utilisateur1 = new Utilisateur();
                        var pseudo= (from u in db.Utilisateur where u.idUtilisateur==visiteur select u.pseudoUtilisateur).First();


                        if (ModelState.IsValid)
                        {
                            Utilisateur utilisateur = new Utilisateur
                            {
                                idUtilisateur = visiteur,
                                mailUtilisateur = Email,
                                tokenUtilisateur = token,
                                idStatut = 1,
                                pseudoUtilisateur = pseudo,
                                mdpUtilisateur=motdepasseCrypte,
                                dateInscriptionUtilisateur = DateTime.Now
                            };
                            db.Entry(utilisateur).State = EntityState.Modified;
                        db.SaveChanges();
                        string emailInscription = Email;
                        string sujet = "Activez votre compte ";
                        string messageemail = "<p>Bonjour " + "</p> <p>Vous pouvez activer votre compte en cliquant sur le lien suivant :<a href=\"http://www.melcydev.com/Utilisateurs/ValideCompte?token=" + token + "\"><b>http://www.melcydev.com/Utilisateurs/ValideCompte?token=" + token + "</b><a></p><p>Si ce lien ne fonctionne pas. Veuillez le copier dans votre navigateur. </p><br/><p>Merci pour votre inscription</p>";
                        Workflow smtp = new Workflow();
                        smtp.SMTP(emailInscription, sujet, messageemail);
                 
                       
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                    return View();
                }
               
                return View();

            }
            else 
            {
                
                ViewBag.erreur= 2;
              
                return View();
            }
         

        }
        //Relation avec vue Inscription
        public PartialViewResult Inscription()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Inscription(string Email, string Pseudo, string MotdePasse)
        {
            try
            {
                if (RegexUtilities.IsValidEmail(Email))
                {
                    var mdpcryte = Workflow.getHashSha256(MotdePasse).Substring(0, 50);
                    string token = Guid.NewGuid().ToString();
                    //Enregistrement des données 
                    Utilisateur utilisateur = new Utilisateur();
                    utilisateur.mailUtilisateur = Email;
                    utilisateur.pseudoUtilisateur = Pseudo;
                    utilisateur.mdpUtilisateur = mdpcryte;
                    utilisateur.idStatut = 0;
                    //utilisateur.tokenUtilisateur = token;
                    utilisateur.dateInscriptionUtilisateur = DateTime.Now;


                    //Ajout et sauvegardes des donnée dans la table utilisateur
                    db.Utilisateur.Add(utilisateur);
                    db.SaveChanges();
                    TempData["Succes"] = "Inscription Réussie";
                    ViewBag.Message = TempData["Succes"].ToString();

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }
        //Relation avec vue AjaxInscription
        public ActionResult AjaxInscription()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AjaxInscription(string Email, string Pseudo, string MotDePasse)
        {
            var mdpcryte = Workflow.getHashSha256(MotDePasse).Substring(0, 50);
            //Verification si le mail ou le pseudo saisie existe dans la base de donnée
            int countMail = (from u in db.Utilisateur where u.mailUtilisateur == Email select u.idUtilisateur).DefaultIfEmpty(0).First();
            int countPseudo = (from u in db.Utilisateur where u.mailUtilisateur == Pseudo select u.idUtilisateur).DefaultIfEmpty(0).First();
            string token = Guid.NewGuid().ToString();
            if (countMail > 0)
            {
                ViewBag.MessageEmail = "L'email que vous avez choisit existe deja";
                return View(ViewBag.MessageEmail);
            }
           if (countPseudo > 0)
            {
                ViewBag.MessagePseudo = "Le pseudo que vous avez choisit existe deja";
                return View(ViewBag.MessagePseudo);
            }
            if(countMail==0 && countPseudo==0)
            {
                Utilisateur utilisateur = new Utilisateur
                {
                    //Nom = Nom,
                    //Prenom = Prenom,
                    mailUtilisateur = Email,
                    pseudoUtilisateur = Pseudo,
                    mdpUtilisateur = mdpcryte,
                    tokenUtilisateur = token,
                    idStatut = 1,
                    dateInscriptionUtilisateur = DateTime.Now
                };

                db.Utilisateur.Add(utilisateur);
                try
                {
                    db.SaveChanges();

                    ViewBag.success = "Inscription OK";
                    string emailInscription = Email;
                    string sujet = "Activez votre compte ";
                    string messageemail = "<p>Bonjour " + Pseudo + "</p> <p>Vous pouvez activer votre compte en cliquant sur le lien suivant :<a href='http://www.melcydev.com/Utilisateurs/ValideCompte?token=" + token + "'><b>http://www.melcydev.com/Utilisateurs/ValideCompte?token=" + token + "</b><a></p><p>Si ce lien ne fonctionne pas. Veuillez le copier dans votre navigateur. </p><br/><p>Merci pour votre inscription</p>";
                    Workflow smtp = new Workflow();
                    smtp.SMTP(emailInscription, sujet, messageemail);

                }
                catch (Exception ex)
                {

                    ViewBag.MeSSAGEERREUR = ex.Message + "" + ex.InnerException;
                }
            }
            return View();
        }

        public ActionResult ValideCompte(string token)
        {
            int veriftoken = (from u in db.Utilisateur where u.tokenUtilisateur == token select u.idUtilisateur).DefaultIfEmpty(0).First();

            if (veriftoken > 0)
            {
                var validationcompte = (from u in db.Utilisateur where u.idUtilisateur == veriftoken select u.idUtilisateur).First();

                Utilisateur utilisateur = db.Utilisateur.Find(validationcompte);

                utilisateur.tokenUtilisateur = Guid.NewGuid().ToString();
                utilisateur.idStatut =2;
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.msg = "Votre compte est validé";
            }
            else
            {
                ViewBag.msg = "Veuillez recommencer";
            }
            return PartialView();
        }


        //mot de passe oublier


        public ActionResult DemandeMotdepasse()
        {
            return View();
        }

        // POST: Utilisateurs/Demande de mot de passe
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DemandeMotdepasse([Bind(Include = "idUtilisateur,mailUtilisateur")] Utilisateur utilisateur)
        {
            int count = (from u in db.Utilisateur where u.mailUtilisateur == utilisateur.mailUtilisateur select u.idUtilisateur).DefaultIfEmpty(0).First();

            if (utilisateur.mailUtilisateur == null)
            {
                ViewBag.Email_lost = "Entrez votre email pour demande un nouveau mot de passe";
            }
            else if (RegexUtilities.IsValidEmail(utilisateur.mailUtilisateur) == false)
            {
                ViewBag.Email_regex = "Entrer un email valide";
            }
            else
            {
                try
                {
                    if (count > 0)
                    {
                        var verifutilisateur = (from u in db.Utilisateur where u.mailUtilisateur == utilisateur.mailUtilisateur select u).First();
                        ViewBag.succesdemande = "Un Email pour valider le changement de mot de passe vient d'être envoyé";
                        string mailinscritadmin = utilisateur.mailUtilisateur;
                        string sujet = "Sujet : demande de mot de passe ";
                        string messageemail = "<p>Bonjour " + verifutilisateur.pseudoUtilisateur + "</p> <p>Vous avez demande un changement de mot de passe cliquez sur le lien suivant :<a href=\"http://www.melcydev.com/Utilisateurs/ChangerMdp?email=" + verifutilisateur.mailUtilisateur + "&token=" + verifutilisateur.tokenUtilisateur + "\"><b>http://www.melcydev.com/Utilisateurs/ChangerMdp?email=" + verifutilisateur.mailUtilisateur + "&token=" + verifutilisateur.tokenUtilisateur + "</b><a></p><p>Si ce lien ne fonctionne pas. Veuillez le copier dans votre navigateur. </p><br/><p>Merci</p>";
                        Workflow smtp = new Workflow();
                        smtp.SMTP(mailinscritadmin, sujet, messageemail);
                    }
                    else
                    {
                        ViewBag.connu = "Ce compte n'existe pas";
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.MeSSAGEERREUR = ex.Message + "" + ex.InnerException;
                }
            }
            return View();
        }

        public ActionResult ChangerMdp(string email, string token)
        {
            try
            {
                int veriftoken = (from u in db.Utilisateur where u.mailUtilisateur == email && u.tokenUtilisateur == token select u.idUtilisateur).DefaultIfEmpty(0).First();

                if (veriftoken > 0)
                {
                    TempData["email"] = email;
                    return RedirectToAction("ResultatMdp", "Utilisateurs");
                }
                else
                {
                    ViewBag.expire = "votre token a expiré";
                }
            }
            catch (Exception ex)
            {
                ViewBag.MeSSAGEERREUR = ex.Message + "" + ex.InnerException;
            }
            return View();
        }

        public ActionResult ResultatMdp(string email, string mdpUtilisateur, string CPassword)
        {
            if (email == null)
            {
                return View();
            }
            else
            {
                try
                {
                    var Email = email;
                    int changementpassword = (from u in db.Utilisateur where u.mailUtilisateur == Email select u.idUtilisateur).DefaultIfEmpty(0).First();
                    if (mdpUtilisateur == "" || mdpUtilisateur.Length < 5)
                    {
                        ViewBag.longueur = "Le champ est vide, vous devez avoir 5 caractères minimums";
                    }
                    else if (mdpUtilisateur != CPassword)
                    {
                        ViewBag.Identique = "Les mot de passe ne sont pas identique";
                    }
                    else if (changementpassword > 0)
                    {
                        var motdepasseCrypte = Workflow.getHashSha256(mdpUtilisateur).Substring(0, 50);

                        Utilisateur utilisateur = db.Utilisateur.Find(changementpassword);
                        utilisateur.mdpUtilisateur = motdepasseCrypte;
                        utilisateur.tokenUtilisateur = Guid.NewGuid().ToString();
                        db.Entry(utilisateur).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["test"] = "Votre mot de passe a bien ete modifié";
                        return RedirectToAction("DemandeMotdepasse", "Utilisateurs");
                    }
                    else
                    {
                        ViewBag.Email = "Votre Email n'ai pas reconnu recommencé la procedure";
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.MeSSAGEERREUR = ex.Message + "" + ex.InnerException;
                }
            }
            return View();
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
