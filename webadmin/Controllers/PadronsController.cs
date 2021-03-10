using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
            ViewBag.votante = new SelectList(db.TipoVotantes, "id", "tipo_votante");
            return View();
        }

        // POST: Padrons/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre,paterno,materno,telefono,celular,direccion,comentario,rfc,curp,claveElectoral,votante,usuarioRegistro,usuarioUpdate,fechaRegistro,fechaUpdate,email,registroCompleto")] Padron padron)
        {
            if (ModelState.IsValid)
            {
                if(padron.nombre != null && padron.paterno != null && padron.telefono != null && padron.celular != null && padron.direccion != null && padron.rfc != null && padron.curp != null && padron.claveElectoral != null && padron.email != null)
                {
                    padron.registroCompleto = true;
                }
                else
                {
                    padron.registroCompleto = false;
                }
                db.Padrons.Add(padron);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(padron);
        }

        public async System.Threading.Tasks.Task<JsonResult> CreateAsync(HistorialContacto data, HttpPostedFileBase file)
        {
            try
            {
                HistorialContacto historial = new HistorialContacto();

                historial.FkPadron = data.FkPadron;
                historial.comentario = data.comentario;
                historial.fechaupdate = DateTime.Now;
                historial.usuarioRegistro = data.usuarioRegistro;
                
                db.HistorialContactoes.Add(historial);
                await db.SaveChangesAsync();

                return Json(new { accion = true, Msg = "Se ha agregado el seguimiento correctamente" });
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception e)
            {
                return Json(new { accion = false, Msg = e.Message });
            }

        }

        // GET: Padrons/Edit/5
        public ActionResult Edit(int? id)
        {
            PadronHistorial phistorial = new PadronHistorial();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phistorial.padron = db.Padrons.Find(id);
            phistorial.historial = db.HistorialContactoes.Include(s => s.AspNetUser).Where(a => a.FkPadron.Equals(phistorial.padron.Id)).ToList();
            ViewBag.votante = new SelectList(db.TipoVotantes, "id", "tipo_votante",phistorial.padron.votante);
            if (phistorial.padron == null)
            {
                return HttpNotFound();
            }
            return View(phistorial);
        }


        // POST: Padrons/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PadronHistorial phistorial)
        {
            if (ModelState.IsValid)
            {
                Padron padron = new Padron();

                if (padron.nombre != null && padron.paterno != null && padron.telefono != null && padron.celular != null && padron.direccion != null && padron.rfc != null && padron.curp != null && padron.claveElectoral != null && padron.email != null)
                {
                    padron.registroCompleto = true;
                }
                else
                {
                    padron.registroCompleto = false;
                }

                padron.usuarioRegistro = null;
                padron.usuarioUpdate = null;
                padron.fechaRegistro = null;
                padron.fechaUpdate = null;
                db.Entry(phistorial.padron).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phistorial);
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

        public async System.Threading.Tasks.Task<JsonResult> EliminarAsync(Padron data)
        {
            try
            {
                Padron padron = db.Padrons.Find(data.Id);
                db.Padrons.Remove(padron);
                await db.SaveChangesAsync();

                return Json(new { accion = true, Msg = "Se ha eliminado correctamente" });
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception e)
            {
                return Json(new { accion = false, Msg = e.Message });
            }

        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> EliminarHistorialAsync(HistorialContacto data)
        {
            try
            {
                HistorialContacto historial = db.HistorialContactoes.Find(data.Id);
                db.HistorialContactoes.Remove(historial);
                await db.SaveChangesAsync();

                return Json(new { accion = true, Msg = "Se ha eliminado correctamente" });
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception e)
            {
                return Json(new { accion = false, Msg = e.Message });
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
