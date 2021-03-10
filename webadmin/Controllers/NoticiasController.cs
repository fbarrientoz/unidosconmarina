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
    public class NoticiasController : Controller
    {
        private UnidosconmarinaEntities db = new UnidosconmarinaEntities();

        // GET: Noticias
        public ActionResult Index()
        {
            return View(db.Noticias.Include(a => a.AspNetUser).Include(a => a.AspNetUser1).ToList());
        }

        // GET: Noticias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Noticias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,titulo,descripcion,lugar,fecha,fotoDefault,estatus,usuarioRegistro,usuarioUpdate,fechaRegistro,fechaUpdate")] Noticia noticia, HttpPostedFileBase file)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {

                    if (file != null)
                    {
                        string NombreArchivo = System.IO.Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath("~/Content/images/noticias/" + NombreArchivo);
                        file.SaveAs(physicalPath);
                        noticia.fotoDefault = NombreArchivo;

                    }
                    db.Noticias.Add(noticia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(noticia);
            }
            else
            {
                return View("~/Views/Account/Login.cshtml");
            }
        }


        // GET: Noticias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticias.Include(a => a.AspNetUser).FirstOrDefault(x => x.id == id);
            if (noticia == null)
            {
                return HttpNotFound();
            }
            return View(noticia);
        }

        // POST: Noticias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,titulo,descripcion,lugar,fecha,fotoDefault,estatus,usuarioRegistro,usuarioUpdate,fechaRegistro,fechaUpdate")] Noticia noticia, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    string NombreArchivo = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Content/images/banners/" + NombreArchivo);
                    file.SaveAs(physicalPath);
                    noticia.fotoDefault = NombreArchivo;
                }
                db.Entry(noticia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noticia);
        }

        // GET: Noticias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticias.Find(id);
            if (noticia == null)
            {
                return HttpNotFound();
            }
            return View(noticia);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> EliminarAsync(Noticia data)
        {
            try
            {
                Noticia noticia = db.Noticias.Find(data.id);
                db.Noticias.Remove(noticia);
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
