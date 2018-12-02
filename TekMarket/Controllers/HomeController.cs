using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using TekMarket.Models;
using Syn.Bot;
using Syn.Chat;
using System.IO;
using WebMatrix.WebData;
using System.Dynamic;

namespace TekMarket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }






        public ActionResult Register()
        {
            return RedirectToAction("Create", "Clients");
        }

        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Contact(MailModel mm)
        {

            SmtpClient client = new SmtpClient("smtp.live.com");
             client.Credentials = new NetworkCredential("tekmarket2018@hotmail.com", "ISSATso2018");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            //If you need to authenticate
            //client.UseDefaultCredentials = false;
            MailMessage mailMessage = new MailMessage();
              MailAddress address = new MailAddress("tekmarket2018@hotmail.com", "ISSATso2018");
            mailMessage.From = address;
            mailMessage.To.Add("rahma.trabelsi101@gmail.com");
            mailMessage.CC.Add(mm.Email);
            mailMessage.Subject = (mm.Name);
            mailMessage.Body = (mm.Message);
            client.Send(mailMessage);

            return View("Index");
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Authorize]


        public ActionResult Login(FormCollection formCollection)
        {
            String email = formCollection["email"];
            String pwd = formCollection["pwd"];


            if (ModelState.IsValid)
            {
                DBModel db = new DBModel();
                List<Client> la = db.Clients.ToList();
                int i = 0;
                while (i < la.Count && !la.ElementAt(i).email.Equals(email))
                {
                    i++;
                }

                if (i < la.Count)
                {
                    if (la.ElementAt(i).pwd.Equals(pwd))
                    {
                        FormsAuthentication.SetAuthCookie(email, false);
                        Session["prenom"] = la.ElementAt(i).prenom;
                        Session["idClient"] = la.ElementAt(i).id;
                        Client c = la.ElementAt(i);
                        // ViewBag.username = c.email;
                        return View("Index", c);
                    }
                    else
                    {

                        return Redirect("/Home/Login");


                    }

                }
                else
                {
                    return Redirect("/Home/Login");

                }




            }





            return Redirect("/Home/Login");


        }
        dynamic mymodel = new ExpandoObject();
        public ActionResult Checkout()
        {

            int idutilisateur = int.Parse(Session["idClient"].ToString());
            DBModel db = new DBModel();

            var qtep = from m in db.Qtepaniers
                       where (m.Panier.id_utilisateur == idutilisateur)
                       select m;
            List<Qtepanier> qtepanierlist = qtep.ToList();
            mymodel.listepanier = qtepanierlist;

            return View(mymodel);
        }
        [HttpPost]
        public ActionResult Checkout(String refarticle, float prix)
        {

            String name = refarticle;
            float ammount = prix;
            int idutilisateur = int.Parse(Session["idClient"].ToString());
            DBModel db = new DBModel();
            Article a = db.Articles.Find(refarticle);
            List<Panier> Listepanier = db.Paniers.ToList();

            Panier panier = new Panier();
            Qtepanier qtepanier = new Qtepanier();
            panier.id_utilisateur = idutilisateur;
            qtepanier.refarticle = refarticle;
            qtepanier.montant = ammount;
            qtepanier.Panier = panier;
            // qtepanier.Panier.id_utilisateur = idutilisateur
            var qtepanierre = from m in db.Qtepaniers
                              where (m.Panier.id_utilisateur == idutilisateur) && (m.refarticle == refarticle)
                              select m;

            List<Qtepanier> qqq = qtepanierre.ToList();


            if (qqq.Count == 0)
            {
                qtepanier.qte = 1;
                db.Qtepaniers.Add(qtepanier);
                db.SaveChanges();
            }
            else
            {
                qqq.ElementAt(0).qte++;
                // qqq.ElementAt(0).montant += qtepanier.Article.prix;
                var qte = db.Qtepaniers.Find(qqq.ElementAt(0).idpanier, qqq.ElementAt(0).refarticle);
                qte = qqq.ElementAt(0);
                db.SaveChanges();
            }
            var qtep = from m in db.Qtepaniers
                       where (m.Panier.id_utilisateur == idutilisateur)
                       select m;
            List<Qtepanier> qtepanierlist = qtep.ToList();
            mymodel.listepanier = qtepanierlist;
            //panier.Add(new Panier(1, ammount, idutilisateur));
            return View(mymodel);

        }

        public ActionResult Logout()
        {
            Session["prenom"] = null;
            Session["idClient"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult RemovePanier(String article)
        {
            DBModel db = new DBModel();
            int idutilisateur = int.Parse(Session["idClient"].ToString());
            var qtepanier = from m in db.Qtepaniers
                              where (m.Panier.id_utilisateur == idutilisateur)
                              && (m.refarticle == article)
                              select m;
            Qtepanier qte = qtepanier.ElementAt(0);
            db.Qtepaniers.Remove(qte);
            db.SaveChanges();
            return RedirectToAction("Checkout");
        }
        public ActionResult CheckoutSuc()
        {
            DBModel db = new DBModel();
            int idutilisateur = int.Parse(Session["idClient"].ToString());
            var qtepanierre = from m in db.Qtepaniers
                              where (m.Panier.id_utilisateur == idutilisateur)
                              select m;
            List<Qtepanier> qqq = qtepanierre.ToList();
            Commande commande = new Commande();
            Qtecommande qtecommande = new Qtecommande();
            String refcom;
            int k = 1;
            /*  for (int i= 0; i < qqq.Count(); i++)
              {     qtecommande.refarticle = qqq.ElementAt(i).refarticle;
                  qtecommande.qte = qqq.ElementAt(i).qte;
                  qtecommande.totalprix = qqq.ElementAt(i).montant;
                  commande.idutilisateur = idutilisateur;
                  commande.datecom = System.DateTime.Today;
                  refcom = k.ToString();
                  //commande.refcomm = refcom;
                  qtecommande.Commande = commande;

                  db.Qtecommandes.Add(qtecommande);
                  db.SaveChanges();
                  k++; 
              }*/
            return View();
        }


        public ActionResult Chatting(String ms)
        {
            try
            {

                SynBot bot = new SynBot();
                BotUser user = bot.CreateUser();

                var pk = System.IO.File.ReadAllText(Server.MapPath("~/Content/Assistant/PackageChat.txt"));
                bot.PackageManager.LoadFromString(pk);

                var chatreq = new ChatRequest(ms, RequestType.UserMessage, user);
                ChatResult res = bot.Chat(chatreq);
                string response = res.BotMessage;

                if (res.Success)
                {
                    return Content("<p>" + response + "</p><br>", "text/html");
                }
                else
                {
                    return Content("<p>unsuccessful</p><br>", "text/html");
                }

            }
            catch (Exception ex)
            {
                return Content("<p>" + ex.Message + "</p><br>", "text/html");
            }
        }

    }
}