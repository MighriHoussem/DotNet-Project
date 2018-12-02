using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TekMarket.Models;

namespace TekMarket.Controllers
{
    public class ProductController : Controller
    {
        dynamic mymodel = new ExpandoObject();

        // GET: Product
        public ActionResult Products()
        {

            Session["search"] = null;
            Session["idcategorie"] = null;
            // DBModel db = new DBModel();
            // List<Categorie> la = db.Categories.ToList();
            DBModel db = new DBModel();
            mymodel.Article = GetArticle();
            mymodel.Categorie = GetCategorie();
            return View(mymodel);
        }
        private List<Article> GetArticle()
        {
            DBModel db = new DBModel();
            List<Article> la = db.Articles.ToList();
            return la;
        }

        public List<Categorie> GetCategorie()
        {
            DBModel db = new DBModel();
            List<Categorie> la = db.Categories.ToList();
            return la;
        }

        public ActionResult Single(String refarticle)
        {
            Session["search"] = null;
            Session["idcategorie"] = null;
            DBModel db = new DBModel();
            Article a = db.Articles.Find(refarticle);
            return View(a);

        }
        public ActionResult Search(String ch)
        {
            Session["search"] = "chaine";
             ch = Request.Form["Search"];
            DBModel db = new DBModel();
            var article = from m in db.Articles
                          where m.libelle.Contains(ch)
                          select m;
            List<Article> articles = article.ToList();

            Session["idcategorie"] = null;
            mymodel.articlesearch = articles;
            mymodel.Categorie = GetCategorie();
            return View("Products", mymodel);
        }
        public ActionResult SearchCategorie(String id)
        {
         
            DBModel db = new DBModel();
            var article = from m in db.Articles
                          where (m.idcategorie == id)
                          select m;
            List<Article> articles = article.ToList();
            Session["search"] = null;
            Session["idcategorie"] = id;
            mymodel.articlech = articles;
            mymodel.Categorie = GetCategorie();
            return View("Products", mymodel);
        }
    }
}