using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UPM.Entities;
using webadmin.Models;

namespace webadmin.Controllers
{
    public class HistorialContactoesController : Controller
    {
        private UnidosconmarinaEntities db = new UnidosconmarinaEntities();

        // GET: HistorialContactoes
        public ActionResult Index()
        {
            return View(db.HistorialContactoes.ToList());
        }

        // GET: HistorialContactoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialContacto historialContacto = db.HistorialContactoes.Find(id);
            if (historialContacto == null)
            {
                return HttpNotFound();
            }
            return View(historialContacto);
        }

        // GET: HistorialContactoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistorialContactoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,comentario,FkPadron,usuarioRegistro,fechaupdate")] HistorialContacto historialContacto)
        {
            if (ModelState.IsValid)
            {
                db.HistorialContactoes.Add(historialContacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(historialContacto);
        }

        // GET: HistorialContactoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialContacto historialContacto = db.HistorialContactoes.Find(id);
            if (historialContacto == null)
            {
                return HttpNotFound();
            }
            return View(historialContacto);
        }

        // POST: HistorialContactoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,comentario,FkPadron,usuarioRegistro,fechaupdate")] HistorialContacto historialContacto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historialContacto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(historialContacto);
        }

        // GET: HistorialContactoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialContacto historialContacto = db.HistorialContactoes.Find(id);
            if (historialContacto == null)
            {
                return HttpNotFound();
            }
            return View(historialContacto);
        }

        // POST: HistorialContactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistorialContacto historialContacto = db.HistorialContactoes.Find(id);
            db.HistorialContactoes.Remove(historialContacto);
            db.SaveChanges();
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
