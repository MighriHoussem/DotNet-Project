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
    public class QtecommandesController : Controller
    {
        private DBModel db = new DBModel();

        // GET: Qtecommandes
        public async Task<ActionResult> Index()
        {
            var qtecommandes = db.Qtecommandes.Include(q => q.Article).Include(q => q.Commande);
            return View(await qtecommandes.ToListAsync());
        }

        // GET: Qtecommandes/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qtecommande qtecommande = await db.Qtecommandes.FindAsync(id);
            if (qtecommande == null)
            {
                return HttpNotFound();
            }
            return View(qtecommande);
        }

        // GET: Qtecommandes/Create
        public ActionResult Create()
        {
            ViewBag.refarticle = new SelectList(db.Articles, "refarticle", "libelle");
            ViewBag.refcom = new SelectList(db.Commandes, "refcomm", "refcomm");
            return View();
        }

        // POST: Qtecommandes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "refcom,refarticle,qte,totalprix")] Qtecommande qtecommande)
        {
            if (ModelState.IsValid)
            {
                db.Qtecommandes.Add(qtecommande);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.refarticle = new SelectList(db.Articles, "refarticle", "libelle", qtecommande.refarticle);
            ViewBag.refcom = new SelectList(db.Commandes, "refcomm", "refcomm", qtecommande.refcom);
            return View(qtecommande);
        }

        // GET: Qtecommandes/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qtecommande qtecommande = await db.Qtecommandes.FindAsync(id);
            if (qtecommande == null)
            {
                return HttpNotFound();
            }
            ViewBag.refarticle = new SelectList(db.Articles, "refarticle", "libelle", qtecommande.refarticle);
            ViewBag.refcom = new SelectList(db.Commandes, "refcomm", "refcomm", qtecommande.refcom);
            return View(qtecommande);
        }

        // POST: Qtecommandes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "refcom,refarticle,qte,totalprix")] Qtecommande qtecommande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qtecommande).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.refarticle = new SelectList(db.Articles, "refarticle", "libelle", qtecommande.refarticle);
            ViewBag.refcom = new SelectList(db.Commandes, "refcomm", "refcomm", qtecommande.refcom);
            return View(qtecommande);
        }

        // GET: Qtecommandes/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Qtecommande qtecommande = await db.Qtecommandes.FindAsync(id);
            if (qtecommande == null)
            {
                return HttpNotFound();
            }
            return View(qtecommande);
        }

        // POST: Qtecommandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Qtecommande qtecommande = await db.Qtecommandes.FindAsync(id);
            db.Qtecommandes.Remove(qtecommande);
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
