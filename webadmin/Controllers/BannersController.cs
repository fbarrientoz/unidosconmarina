
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
    public class BannersController : Controller
    {
        private UnidosconmarinaEntities db = new UnidosconmarinaEntities();

        // GET: Banners
        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }

        // GET: Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banners/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,url_foto,estatus,link,usuarioRegistro,usuarioUpdate,fechaRegistro,fechaupdate")] Banner banner, HttpPostedFileBase file)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {

                    if (file != null)
                    {
                        string NombreArchivo = System.IO.Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath("~/Content/images/banners/" + NombreArchivo);
                        file.SaveAs(physicalPath);
                        banner.url_foto = NombreArchivo;

                    }
                    db.Banners.Add(banner);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(banner);
            } 
            else
            {
                return View("~/Views/Account/Login.cshtml");
            }
        }

        // GET: Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Include(a => a.AspNetUser).FirstOrDefault(x => x.Id == id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,url_foto,estatus,link,usuarioRegistro,usuarioUpdate,fechaRegistro,fechaupdate")] Banner banner, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    string NombreArchivo = System.IO.Path.GetFileName(file.FileName);
                    string physicalPath = Server.MapPath("~/Content/images/banners/" + NombreArchivo);
                    file.SaveAs(physicalPath);
                    banner.url_foto = NombreArchivo;
                }
                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> EliminarAsync(Banner data)
        {
            try
            {
                Banner banner = db.Banners.Find(data.Id);
                db.Banners.Remove(banner);
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
