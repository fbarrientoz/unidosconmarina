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
    public class PadronsController : Controller
    {
        private UnidosconmarinaEntities db = new UnidosconmarinaEntities();

        // GET: Padrons
        public ActionResult Index()
        {
            return View(db.Padrons.ToList());
        }

        // GET: Padrons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padron padron = db.Padrons.Find(id);
            if (padron == null)
            {
                return HttpNotFound();
            }
            return View(padron);
        }

        // GET: Padrons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Padrons/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre,paterno,materno,telefono,celular,direccion,comentario,rfc,curp,claveElectoral,votante,usuarioRegistro,usuarioUpdate,fechaRegistro,fechaUpdate")] Padron padron)
        {
            if (ModelState.IsValid)
            {
                db.Padrons.Add(padron);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(padron);
        }

        // GET: Padrons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padron padron = db.Padrons.Find(id);
            if (padron == null)
            {
                return HttpNotFound();
            }
            return View(padron);
        }

        // POST: Padrons/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre,paterno,materno,telefono,celular,direccion,comentario,rfc,curp,claveElectoral,votante,usuarioRegistro,usuarioUpdate,fechaRegistro,fechaUpdate")] Padron padron)
        {
            if (ModelState.IsValid)
            {
                db.Entry(padron).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(padron);
        }

        // GET: Padrons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padron padron = db.Padrons.Find(id);
            if (padron == null)
            {
                return HttpNotFound();
            }
            return View(padron);
        }

        // POST: Padrons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Padron padron = db.Padrons.Find(id);
            db.Padrons.Remove(padron);
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
