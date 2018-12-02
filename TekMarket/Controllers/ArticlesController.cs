using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TekMarket.Models;
using System.IO;




namespace TekMarket.Controllers
{
    [Authorize]
     [RoutePrefix("Admin/Articles")]
    public class ArticlesController : Controller
    {
        private DBModel db = new DBModel();

        // GET: Articles
        [HttpGet]
        [Route]
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var articles = db.Articles.Include(a => a.Categorie);
            return View(await articles.ToListAsync());
        }

        // GET: Articles/Details/5
        [HttpGet]
        [Route("details/{id}")]
        [Authorize]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create

       //[Authorize]
        [HttpGet]
        [Route("Create")]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.idcategorie = new SelectList(db.Categories, "id", "libelle");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "refarticle,libelle,description,prix,qtedisponible,couleurdispo,idcategorie")] Article article)
        {
            if (ModelState.IsValid)
            {
               
                

         


                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idcategorie = new SelectList(db.Categories, "id", "libelle", article.idcategorie);
            return View(article);
        }

        // GET: Articles/Edit/5
        
        [Route("Edit/{id}")]
        [Authorize]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcategorie = new SelectList(db.Categories, "id", "libelle", article.idcategorie);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "refarticle,libelle,description,prix,image,qtedisponible,couleurdispo,idcategorie")] Article article)
        {
            if (ModelState.IsValid)
            {
              

                db.Entry(article).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idcategorie = new SelectList(db.Categories, "id", "libelle", article.idcategorie);
            return View(article);
        }

        // GET: Articles/Delete/5
        [HttpGet]
        [Route("Delete/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await db.Articles.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Article article = await db.Articles.FindAsync(id);
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpGet]
        [Route("uploadimage")]
        [Authorize]
        public ActionResult uploadImage()
        {
            return View("UploadImage");

        }



        [HttpPost]
        [Route("UploadFiles")]
        [Authorize]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //setImage Path In Model Article 



                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (file != null)
                    {

                        DBModel db=new DBModel();
                        List<Article> laArticles = db.Articles.ToList();

                        int i = 0;
                        while (i < laArticles.Count && laArticles.ElementAt(i).refarticle.CompareTo(file.FileName) != 0)
                        {
                            i++;

                        }

                     




                        string path = Path.Combine(Server.MapPath("~/UploadedImage"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);


                        if (i < laArticles.Count)
                        {
                            laArticles.ElementAt(i).image = path;

                            db.SaveChanges();


                        }

                        String ftpUrl = "ftp://ftpupload.net/htdocs/images/";
                        String ftpUser = "epiz_22842660";
                        String ftpPass = "LIcxYNffKR7U9";

                        string filename = Path.GetFileName(file.FileName);

                        string fullPath = ftpUrl + filename;



                        FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(fullPath);
                        ftp.Credentials = new NetworkCredential(ftpUser, ftpPass);

                        ftp.KeepAlive = true;
                        ftp.UseBinary = true;
                        ftp.Method = WebRequestMethods.Ftp.UploadFile;




                        FileStream fs = System.IO.File.OpenRead(Path.GetFullPath(path));


                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Close();

                        Stream ftpstream = ftp.GetRequestStream();
                        ftpstream.Write(buffer, 0, buffer.Length);
                        ftpstream.Close();
                        Console.WriteLine("Done !");


                        ViewBag.FileStatus = "File uploaded successfully.";
                    }

                }
                catch (Exception e)
                {
                    ViewBag.FileStatus = "Error while file uploading." + e.Message;
                }
            }

            return View("UploadImage");
        }


    }
}
