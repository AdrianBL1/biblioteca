using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using capaDatos;

namespace biblioteca.Controllers
{
    public class PrestamosController : Controller
    {
        private bibliotecaITSM_dbEntities db = new bibliotecaITSM_dbEntities();

        // GET: Prestamos
        public ActionResult Index()
        {
            var prestamos = db.Prestamos.Include(p => p.Libros).Include(p => p.Usuarios);
            return View(prestamos.ToList());
        }

        // GET: Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            ViewBag.id_libro = new SelectList(db.Libros, "id_libro", "titulo");
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre");
            return View();
        }

        // POST: Prestamos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_prestamo,id_usuario,id_libro,fecha_prestamo,fecha_entrega,comentarios")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                db.Prestamos.Add(prestamos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_libro = new SelectList(db.Libros, "id_libro", "titulo", prestamos.id_libro);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", prestamos.id_usuario);
            return View(prestamos);
        }

        // GET: Prestamos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_libro = new SelectList(db.Libros, "id_libro", "titulo", prestamos.id_libro);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", prestamos.id_usuario);
            return View(prestamos);
        }

        // POST: Prestamos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_prestamo,id_usuario,id_libro,fecha_prestamo,fecha_entrega,comentarios")] Prestamos prestamos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_libro = new SelectList(db.Libros, "id_libro", "titulo", prestamos.id_libro);
            ViewBag.id_usuario = new SelectList(db.Usuarios, "id_usuario", "nombre", prestamos.id_usuario);
            return View(prestamos);
        }

        // GET: Prestamos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamos prestamos = db.Prestamos.Find(id);
            if (prestamos == null)
            {
                return HttpNotFound();
            }
            return View(prestamos);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamos prestamos = db.Prestamos.Find(id);
            db.Prestamos.Remove(prestamos);
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
