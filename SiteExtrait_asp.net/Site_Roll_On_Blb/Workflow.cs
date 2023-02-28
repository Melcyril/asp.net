using Site_Roll_On_Blb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Site_Roll_On_Blb
{
    public class Workflow
    {
        private RollOnBlbEntities db = new RollOnBlbEntities();
        string MailWebmaster = ConfigurationManager.AppSettings["Mailadmin"];
        string SmtpWebmaster = ConfigurationManager.AppSettings["Smtpadmin"];
        public bool gestionConnexion(int niveauAutorise)
        {
            if (HttpContext.Current.Session["IdUtilisateur"] == null) { 
                if (HttpContext.Current.Request.Cookies["BlbCookie"] != null)
                {
                    HttpCookie htc = HttpContext.Current.Request.Cookies.Get("BlbCookie");
                    string[] resultID = htc["IdUtilisateur"].Split('A');

                    int idUtilisateur = (Int32.Parse(resultID[0]) + 47) / 846;

                    Utilisateur utilisateur = (from u in db.Utilisateur where u.idUtilisateur == idUtilisateur select u).First();
                    HttpContext.Current.Session["nomStatut"] = utilisateur.Statut.nomStatut;
                    HttpContext.Current.Session["niveauStatut"] = utilisateur.Statut.accesStatut;
                    HttpContext.Current.Session["Email"] = utilisateur.mailUtilisateur;
                    HttpContext.Current.Session["Pseudo"] = utilisateur.pseudoUtilisateur;
                    HttpContext.Current.Session["IdUtilisateur"] = utilisateur.idUtilisateur;
                }
            }
            if (HttpContext.Current.Session["niveauStatut"] != null)
            {
                int niveauConnecte = Int32.Parse(HttpContext.Current.Session["niveauStatut"].ToString());
                if (niveauAutorise <= niveauConnecte) return true;
            }
            return false;
        }
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
        public void SMTP(string mail,string sujetMail, string messageEmail)
        {
            try
            {
                MailMessage courrier = new MailMessage(MailWebmaster, mail, sujetMail, messageEmail);
                courrier.BodyEncoding = System.Text.Encoding.UTF8;
                courrier.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(SmtpWebmaster);
     
                smtp.Send(courrier);
            }
            catch (Exception e)
            {

                string Message = e.Message + ":" + e.InnerException;
            }
        }
        ////On indique au client d'utiliser les informations qu'on va lui fournir
        //smtp.UseDefaultCredentials = true;
        ////Ajout des informations de connexion
        //smtp.Credentials = new System.Net.NetworkCredential(MailWebmaster, "sample11");
        ////On indique que l'on envoie le mail par le réseau
        //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //On active le protocole SSL

        //smtp.Port= 25;    
        public string Extension(string fichier)
        {
            string[] tabextension;
            int dernierpoint = 0;
            int indice;
            string extension = "";
            //recherche de l'extension
            tabextension = fichier.Split('.');
            dernierpoint = tabextension.Count();
            indice = dernierpoint - 1;
            extension = tabextension[indice].ToLower();
            return extension;
        }
    }
}