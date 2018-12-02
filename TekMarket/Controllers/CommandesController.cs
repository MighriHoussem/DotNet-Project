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

namespace TekMarket.Controllers
{   
    [Authorize]
     [RoutePrefix("Admin/Commandes")]
    public class CommandesController : Controller
    {
        private DBModel db = new DBModel();

        // GET: Commandes
        [HttpGet]
        [Route]
        //[Authorize]
        public async Task<ActionResult> Index()
        {
            var commandes = db.Commandes.Include(c => c.Client);
            return View(await commandes.ToListAsync());
        }

        // GET: Commandes/Details/5
        [HttpGet]
        [Route("Details/{id}")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = await db.Commandes.FindAsync(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // GET: Commandes/Create
        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            ViewBag.idutilisateur = new SelectList(db.Clients, "id", "email");
            return View();
        }

        // POST: Commandes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create([Bind(Include = "refcomm,datecom,totalprix,idutilisateur")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Commandes.Add(commande);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idutilisateur = new SelectList(db.Clients, "id", "email", commande.idutilisateur);
            return View(commande);
        }

        // GET: Commandes/Edit/5
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = await db.Commandes.FindAsync(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            ViewBag.idutilisateur = new SelectList(db.Clients, "id", "email", commande.idutilisateur);
            return View(commande);
        }

        // POST: Commandes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<ActionResult> Edit([Bind(Include = "refcomm,datecom,totalprix,idutilisateur")] Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commande).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idutilisateur = new SelectList(db.Clients, "id", "email", commande.idutilisateur);
            return View(commande);
        }

        // GET: Commandes/Delete/5
        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = await db.Commandes.FindAsync(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            return View(commande);
        }

        // POST: Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Commande commande = await db.Commandes.FindAsync(id);
            db.Commandes.Remove(commande);
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
    }
}
