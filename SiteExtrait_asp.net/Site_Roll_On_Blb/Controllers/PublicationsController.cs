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
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Site_Roll_On_Blb.App_Start
{
    public class PublicationsController : Controller
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        string cheminImagesPublications = ConfigurationManager.AppSettings["cheminImagesPublications"];
        int newstypePublication = Int32.Parse(ConfigurationManager.AppSettings["newstypePublication"]);
        int eventtypePublication = Int32.Parse(ConfigurationManager.AppSettings["eventtypePublication"]);
        int clefvisiteur = Int32.Parse(ConfigurationManager.AppSettings["clefvisiteur"]);
        int clefinscrit = Int32.Parse(ConfigurationManager.AppSettings["clefinscrit"]);
        int clefadherant = Int32.Parse(ConfigurationManager.AppSettings["clefadherant"]);
        int ClefAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["AccesStatut"]);
        string urlSite = ConfigurationManager.AppSettings["urlSite"];
        Workflow wkf = new Workflow();
        // GET: Publications
        public ActionResult Index()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                var publication = db.Publication.Include(p => p.Participation).Include(p => p.TypePublication).Include(p => p.Utilisateur).Include(p => p.Statut).OrderByDescending(p=>p.datePostPublication);
                return View(publication.ToList());
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Publications/Details/5
        public ActionResult Details(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Publication publication = db.Publication.Find(id);
                if (publication == null)
                {
                    return HttpNotFound();
                }
                return View(publication);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // GET: Publications/Create
        public ActionResult Create()
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                ViewBag.idParticipation = new SelectList(db.Participation, "idParticipation", "nomParticipation");
                ViewBag.TypesPublication= (from t in db.TypePublication select t).ToList();

            //List<SelectListItem> publications = new List<SelectListItem>();

            //foreach (var item in typePublications)
            //{
            //    SelectListItem listdestypes = new SelectListItem();
            //    listdestypes.Value = item.idTypePublication.ToString();
            //    listdestypes.Text = item.nomTypePublication;
            //    publications.Add(listdestypes);
            //}

            //SelectList list=new SelectList(publications);

           
           

            //ViewBag.idTypePublication = list;// new SelectList(db.TypePublication, "idTypePublication", "nomTypePublication").AsEnumerable();
                ViewBag.idUtilisateur = new SelectList(db.Utilisateur, "idUtilisateur", "mailUtilisateur");
                ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");
                return View();
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Publications/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPublication,idTypePublication,datePostPublication,datePrevisionPublication,titrePublication,sousTitrePublication,souscontenuPublication,contenuPublication,urlPublication,url1Publication,url2Publication,url3Publication,url4Publication,url5Publication,idStatut,visiblePublication,idUtilisateur,idParticipation")] Publication publication)
        {
            //si le contenu >80 caracteres alors on renseigne le sous contenu
            if (publication.contenuPublication != "" && publication.contenuPublication != null)
            {

                string souscontenu = "";
                string[] tabcontenu;
                string contenu = publication.contenuPublication;
                if (contenu.Length > 80)
                {
                    tabcontenu = contenu.Split(' ');
                    for (int cpt = 0; cpt < tabcontenu.Length; cpt++)
                    {
                        if (souscontenu.Length < 80)
                        {
                            souscontenu = souscontenu + " " + tabcontenu[cpt];
                        }
                    }
                    publication.souscontenuPublication = souscontenu + "...";
                }
                else
                {
                    publication.souscontenuPublication = contenu;
                }

                publication.datePostPublication = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Publication.Add(publication);
                    db.SaveChanges();

                    int idPublication = (from p in db.Publication where p.contenuPublication.Contains(publication.contenuPublication) select p.idPublication).FirstOrDefault();
                    if (idPublication > 0)
                    {
                        if (Request.Files.Count > 0)
                        {
                            string path = cheminImagesPublications;



                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                PublicationImage image = new PublicationImage();
                                image.idPublication = idPublication;
                                
                                HttpPostedFileBase httpPostedFile = Request.Files[i];
                                string extens = wkf.Extension(httpPostedFile.FileName).ToLower().ToString();
                                if (httpPostedFile.FileName != "")
                                {
                                    if (extens.Equals("jpg") || extens.Equals("jpeg") || extens.Equals("gif") || extens.Equals("png") || extens.Equals("3gp") || extens.Equals("avi") || extens.Equals("mov") || extens.Equals("mp4"))
                                    {
                                        image.urlPublicationImage = path + httpPostedFile.FileName;
                                        if (i == 0)
                                        {
                                            publication.urlPublication = path + httpPostedFile.FileName;
                                        }
                                        
                                    }
                                    else
                                    {
                                        image.urlPublicationImage = "";
                                    }
                                }
                                else
                                {
                                    image.urlPublicationImage = "";
                                }
                                if(image.urlPublicationImage!="")
                                {
                                    db.PublicationImage.Add(image);
                                }

                                string extension = "";

                                    if (!Request.Files[i].FileName.ToString().Equals(""))
                                    {
                                        extension = wkf.Extension(Request.Files[i].FileName);
                                        FileUpload(Request.Files[i]);
                                        if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                                        {
                                            FileUploadmini(Request.Files[i]);
                                        }
                                    }
                                
                            }
                            db.SaveChanges();     
                        }
                    }
                }
                return RedirectToAction("index");
            }
            else {
                ViewBag.idParticipation = new SelectList(db.Participation, "idParticipation", "nomParticipation");
                ViewBag.TypesPublication = (from t in db.TypePublication select t).ToList();

                //ViewBag.idTypePublication = list;// new SelectList(db.TypePublication, "idTypePublication", "nomTypePublication").AsEnumerable();
                ViewBag.idUtilisateur = new SelectList(db.Utilisateur, "idUtilisateur", "mailUtilisateur");
                ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");

                //verification si un fichier exist

                return View(publication);
            }

           
        }
        public ActionResult FileUpload(HttpPostedFileBase file)
        {

            if (file.FileName !=null)
            {
                string extension = "";
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                Server.MapPath("~/Content/medias"), pic);
                // file is uploaded
                file.SaveAs(path);                
                extension = wkf.Extension(file.FileName);
                if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                {
                    WebImage img = new WebImage(path);
                    if (img.Width > 653)
                    {
                        img.Resize(653, 405, preserveAspectRatio: true, preventEnlarge: false);
                    }

                    img.Save(path);
                }

            }

            return RedirectToAction("Details");
        }
        public ActionResult FileUploadmini(HttpPostedFileBase file)
        {

            if (file.ToString()!="")
            {
                string extension = "";
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                Server.MapPath("~/Content/medias/mini"), pic);
                // file is uploaded
                file.SaveAs(path);

                extension = wkf.Extension(file.FileName);
                if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                {
                    WebImage img = new WebImage(path);
                    //double sourceRatio = img.Width / img.Height;
                    //double targetRatio = targetRect.Width / targetRect.Height;

                    //Size finalSize;
                    //if (sourceRatio > targetRatio)
                    //{
                    //    finalSize = new Size(targetRect.Width, targetRect.Width / sourceRatio);
                    //}
                    //else
                    //{
                    //    finalSize = new Size(targetRect.Height * sourceRatio, targetRect.Height);
                    //}
               
                    if (img.Width > 180)
                    {
                        img.Resize(360, 270, preserveAspectRatio: true, preventEnlarge: false);                   
                    }
                    img.Save(path);
                }
               
            }

            return RedirectToAction("Details");
        }
        // GET: Publications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Publication publication = db.Publication.Find(id);
                if (publication == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ListeImage=(from i in db.PublicationImage where i.idPublication==id select i).ToList();
                ViewBag.idParticipation = new SelectList(db.Participation, "idParticipation", "nomParticipation", publication.idParticipation);
                ViewBag.idTypePublication = new SelectList(db.TypePublication, "idTypePublication", "nomTypePublication", publication.idTypePublication);
                ViewBag.idUtilisateur = new SelectList(db.Utilisateur, "idUtilisateur", "mailUtilisateur", publication.idUtilisateur);
                ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");
                
                return View(publication);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Publications/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPublication,idTypePublication,datePostPublication,datePrevisionPublication,titrePublication,sousTitrePublication,souscontenuPublication,contenuPublication,urlPublication,url1Publication,url2Publication,url3Publication,url4Publication,url5Publication,idStatut,visiblePublication,idUtilisateur,idParticipation")] Publication publication)
        {

            //si le contenu >80 caracteres alors on renseigne le sous contenu
            if (publication.contenuPublication != "" && publication.contenuPublication != null)
            {

                string souscontenu = "";
                string[] tabcontenu;
                string contenu = publication.contenuPublication;
                if (contenu.Length > 80)
                {
                    tabcontenu = contenu.Split(' ');
                    for (int cpt = 0; cpt < tabcontenu.Length; cpt++)
                    {
                        if (souscontenu.Length < 80)
                        {
                            souscontenu = souscontenu + " " + tabcontenu[cpt];
                        }
                    }
                    publication.souscontenuPublication = souscontenu + "...";
                }
                else
                {
                    publication.souscontenuPublication = contenu;
                }

                publication.datePostPublication = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Entry(publication).State = EntityState.Modified;
                    db.SaveChanges();

                   
                        if (Request.Files.Count > 0)
                        {
                            string path = cheminImagesPublications;



                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                PublicationImage image = new PublicationImage();
                                image.idPublication = publication.idPublication;

                                HttpPostedFileBase httpPostedFile = Request.Files[i];
                                string extens = wkf.Extension(httpPostedFile.FileName).ToLower().ToString();
                                if (httpPostedFile.FileName != "")
                                {
                                    if (extens.Equals("jpg") || extens.Equals("jpeg") || extens.Equals("gif") || extens.Equals("png") || extens.Equals("3gp") || extens.Equals("avi") || extens.Equals("mov") || extens.Equals("mp4"))
                                    {
                                        image.urlPublicationImage = path + httpPostedFile.FileName;
                                        if (i == 0)
                                        {
                                            publication.urlPublication = path + httpPostedFile.FileName;
                                            db.Entry(publication).State = EntityState.Modified;
                                            db.SaveChanges();
                                        }

                                    }
                                    else
                                    {
                                        image.urlPublicationImage = "";
                                        publication.urlPublication = "";
                                        
                                    }
                                }
                                else
                                {
                                    image.urlPublicationImage = "";
                                    publication.urlPublication = "";
                                    db.Entry(publication).State = EntityState.Modified;
                                    db.SaveChanges();
                                }
                                try
                                {
                                if (image.urlPublicationImage != "")
                                {
                                    db.PublicationImage.Add(image);
                                    db.SaveChanges();
                                }
   
                                    string extension = "";

                                    if (!Request.Files[i].FileName.ToString().Equals(""))
                                    {
                                        extension = wkf.Extension(Request.Files[i].FileName);
                                        FileUpload(Request.Files[i]);
                                        if (extension.Equals("jpg") || extension.Equals("jpeg") || extension.Equals("gif") || extension.Equals("png"))
                                        {
                                            FileUploadmini(Request.Files[i]);
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    ViewBag.idParticipation = new SelectList(db.Participation, "idParticipation", "nomParticipation");
                                    ViewBag.TypesPublication = (from t in db.TypePublication select t).ToList();

                                    ViewBag.idTypePublication = new SelectList(db.TypePublication, "idTypePublication", "nomTypePublication", publication.idTypePublication);
                                    ViewBag.idUtilisateur = new SelectList(db.Utilisateur, "idUtilisateur", "mailUtilisateur", publication.idUtilisateur);
                                    ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");

                                    return View(publication);

                                }
                              

                            }

                    }

                }
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.idParticipation = new SelectList(db.Participation, "idParticipation", "nomParticipation");
                ViewBag.TypesPublication = (from t in db.TypePublication select t).ToList();

                  ViewBag.idTypePublication = new SelectList(db.TypePublication, "idTypePublication", "nomTypePublication", publication.idTypePublication);
                ViewBag.idUtilisateur = new SelectList(db.Utilisateur, "idUtilisateur", "mailUtilisateur", publication.idUtilisateur);
                ViewBag.idStatut = new SelectList(db.Statut, "idStatut", "nomStatut");

                return View(publication);
            }
        }

        // GET: Publications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (wkf.gestionConnexion(ClefAdmin))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Publication publication = db.Publication.Find(id);
                if (publication == null)
                {
                    return HttpNotFound();
                }
                return View(publication);
                        }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Requete supression des images associer aux publications
            ViewBag.supp = (from p  in db.PublicationImage  where p.idPublication == id select p).ToList();
     
            foreach (var item in ViewBag.supp)
            {
                
                db.PublicationImage.Remove(item);
                db.SaveChanges();
            }
            Publication publication = db.Publication.Find(id);
            
            db.Publication.Remove(publication);
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult _Presentation()
        {
            //ViewBag.presentation = (from p in db.Publication where p.TypePublication.idTypePublication == 3 select p).ToList().AsEnumerable();
            return PartialView();
        }

        public PartialViewResult _Sports()
        {
            //ViewBag.skateboard = (from p in db.Publication where p.TypePublication.idTypePublication == 4 select p).ToList().AsEnumerable();
            return PartialView();
        }
       

        public ActionResult _DevenirAdherant()
        {
            //ViewBag.DevenirAdherant = (from p in db.Publication where p.TypePublication.idTypePublication == 8 select p).ToList().AsEnumerable();
            return View();
        }
        public ActionResult _skatepark()
        {
            //ViewBag.skatepark= (from p in db.Publication where p.TypePublication.idTypePublication == 9 select p).ToList().AsEnumerable();
            return View();
        }

        public ActionResult _interieur()
        {
            //ViewBag.intérieur = (from p in db.Publication where p.TypePublication.idTypePublication == 12 select p).ToList().AsEnumerable();
            return View();
        }

        public PartialViewResult _Evenement_realise_dernier()
        {
            ViewBag.evenement_realise_dernier = (from p in db.Publication where p.TypePublication.idTypePublication ==eventtypePublication && p.datePrevisionPublication < DateTime.Now select p).Take(3);
            return PartialView();
        }
        public ActionResult _Evenement_future_dernier()
        {
            ViewBag.evenement_future_dernier =  (from p in db.Publication where p.TypePublication.idTypePublication == eventtypePublication & p.datePrevisionPublication >DateTime.Now orderby p.datePrevisionPublication ascending select p).ToList().AsEnumerable();
            
            return View();
        }
        public ViewResult _Evenement_realise(int? page)
        {
            ViewBag.evenement_realise = (from p in db.Publication where p.TypePublication.idTypePublication == eventtypePublication && p.datePrevisionPublication < DateTime.Now select p).ToList().AsEnumerable();
            var dummyItems = (from p in db.Publication where p.TypePublication.idTypePublication == eventtypePublication && p.datePrevisionPublication < DateTime.Now select p).ToList().AsEnumerable();
            var pager = new Pager(dummyItems.Count(), page);

            var viewModel = new IndexViewModel
            {
                ItemsEventRealise = dummyItems.Skip((pager.CurrentPage - 1) * pager.ElementPage).Take(pager.ElementPage),
                Pager = pager
            };
            List<PublicationImage> images = (from p in db.PublicationImage select p).ToList();
            List<PublicationImage> listeImages = new List<PublicationImage>();
            if (images != null)
            {
                foreach (var item in viewModel.ItemsEventRealise)
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

            return View(viewModel);
        }
        public PartialViewResult _Evenement_future(int? page)
        {
            ViewBag.evenement_future = (from p in db.Publication where p.TypePublication.idTypePublication == eventtypePublication & p.datePrevisionPublication > DateTime.Now orderby p.datePrevisionPublication ascending select p).ToList().AsEnumerable();
            var dummyItems = (from p in db.Publication where p.TypePublication.idTypePublication == eventtypePublication & p.datePrevisionPublication > DateTime.Now orderby p.datePrevisionPublication ascending select p).ToList().AsEnumerable();
            var pager = new Pager(dummyItems.Count(), page);

            var viewModel = new IndexViewModel
            {
                ItemsEventFuture = dummyItems.Skip((pager.CurrentPage - 1) * pager.ElementPage).Take(pager.ElementPage),
                Pager = pager
            };
            List<PublicationImage> images = (from p in db.PublicationImage select p).ToList();
            List<PublicationImage> listeImages = new List<PublicationImage>();
            if (images != null)
            {
                foreach (var item in viewModel.ItemsEventFuture)
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
            return PartialView(viewModel);
        }
        public ActionResult _Initiation_Decouverte()
        {
            //ViewBag.InitiationDecouverte = (from p in db.Publication where p.TypePublication.idTypePublication == 13 select p).ToList().AsEnumerable();
            return View();
        }
        public ActionResult _Cours()
        {
            //ViewBag.Cours = (from p in db.Publication where p.TypePublication.idTypePublication == 14 select p).ToList().AsEnumerable();
            return View();
        }
        public ActionResult Recherche(String recherche)
        {
            if (Session["niveauStatut"] == null || Int32.Parse(Session["niveauStatut"].ToString()) == 0)
            {
                ViewBag.Myrecherche = (from p in db.Publication where (p.titrePublication.Contains(recherche) || p.sousTitrePublication.Contains(recherche) || p.contenuPublication.Contains(recherche)) && p.Statut.accesStatut < clefinscrit select p).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 1)
            {
                ViewBag.Myrecherche = (from p in db.Publication where (p.titrePublication.Contains(recherche) || p.sousTitrePublication.Contains(recherche) || p.contenuPublication.Contains(recherche)) && p.Statut.accesStatut <= clefinscrit select p).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 10)
            {
                ViewBag.Myrecherche = (from p in db.Publication where (p.titrePublication.Contains(recherche) || p.sousTitrePublication.Contains(recherche) || p.contenuPublication.Contains(recherche)) && p.Statut.accesStatut <= clefadherant select p).ToList().AsEnumerable();
            }
            else if (Int32.Parse(Session["niveauStatut"].ToString()) == 20)
            {
                ViewBag.Myrecherche = (from p in db.Publication where (p.titrePublication.Contains(recherche) || p.sousTitrePublication.Contains(recherche) || p.contenuPublication.Contains(recherche)) && p.Statut.accesStatut <= ClefAdmin select p).ToList().AsEnumerable();
            }

            TempData["Marecherche"] = ViewBag.Marecherche;
            return View();
        }

        [HttpPost]
        public ActionResult _AjaxSupprimerPublication(int id)
        {
            PublicationImage publicationImage = db.PublicationImage.Find(id);

            db.PublicationImage.Remove(publicationImage);
            db.SaveChanges();
            ViewBag.info = "Deleted";
            return View();
        }
        public void fichierpdf(Object Sender, System.EventArgs e)
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                //Chunk chunk = new Chunk("This is from chunk. ");
                //document.Add(chunk);
                Image gif = Image.GetInstance(urlSite+"/Content/Images/logo.png");
                document.Add(gif);

                Phrase mail = new Phrase("\n"+ "\n" + "Mail : _____________________________" + "\n");
                document.Add(mail);
                Phrase nom = new Phrase("Nom : _____________________________" + "Prénom : _____________________________" + "\n");
                document.Add(nom);

                Phrase datenaiss = new Phrase("Date de naissance : _____________________________ " + "\n");
                document.Add(datenaiss);
                Phrase adresse = new Phrase("Adresse : __________________________________________________________ " + "\n");
                document.Add(adresse);
                Phrase commune = new Phrase("CP : ____________________" +"Commune :_____________________________ " + "\n");
                document.Add(commune);
                Phrase numero = new Phrase("Numero de portable : _____________________________ "+ "\n"+"Numéro fixe : _____________________________" + "\n"+ "\n");
                document.Add(numero);
                Phrase numeroparents = new Phrase("Si l'adhérent est mineur," +
                    " veuillez renseigner le numéro des parents ou personne(s) responsable(s) adulte" + "\n"+
                    "Téléphone mère :_____________________________ "+ "\n"+ "Téléphone père :_____________________________" + "\n"+  "Téléphone titulaire légale de l'enfant : _____________________________" + "\n"+ "\n");
                document.Add(numeroparents);

                Paragraph para = new Paragraph("Pièce à fournir : \n"+"1 Cette fiche remplit avec votre signature(Si vous êtes adulte) " +
                "et la signature des parents(si la demande d'adhésion est faite par une personne mineur). \n"+
                "2 Certificat médical de non contre indication à la pratique de skateboard, bmx, trottinette ou roller(valable 3 ans). \n"+
                "3 le règlement pour l'adhésion."+ "\n"+"\n");
                document.Add(para);

                Paragraph protectionimage = new Paragraph("J’autorise l’association " +
                "Roll on Blb à photographier ou à filmer mon enfant lors des cours, des stages ou d'événements et" +
                " autorise la publication de ces photographies dans la presse, sur le site web ou dans tout autre cadre à but non lucratif" +
                " lié directement à l’activité de l’association." + "\n" + "\n" + "\n" + "\n");
                document.Add(protectionimage);

                Paragraph infosignature = new Paragraph("(Si mineur, les parents ou personne titulaire légale doivent signer ici)" +"\n");
                infosignature.Alignment = Element.ALIGN_RIGHT;
                document.Add(infosignature);
                Paragraph signature = new Paragraph("Signature");
                signature.Alignment =  Element.ALIGN_RIGHT;           
                document.Add(signature);
                string text = @"you are successfully created PDF file.";


                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";

                string pdfName = "User";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + pdfName + ".pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
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
