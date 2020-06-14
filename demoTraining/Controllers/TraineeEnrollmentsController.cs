using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demoTraining.Models;

namespace demoTraining.Controllers
{
    [Authorize(Roles = "Staff")]
    public class TraineeEnrollmentsController : Controller
    {
        private TrainingDBEntities db = new TrainingDBEntities();

        // GET: TraineeEnrollments
        public ActionResult Index()
        {
            var traineeEnrollments = db.TraineeEnrollments.Include(t => t.Course).Include(t => t.Trainee);
            return View(traineeEnrollments.ToList());
        }

        // GET: TraineeEnrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraineeEnrollment traineeEnrollment = db.TraineeEnrollments.Find(id);
            if (traineeEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(traineeEnrollment);
        }

        // GET: TraineeEnrollments/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName");
            return View();
        }

        // POST: TraineeEnrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraineeEnrollments,TraineeID,CourseID")] TraineeEnrollment traineeEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.TraineeEnrollments.Add(traineeEnrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", traineeEnrollment.CourseID);
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName", traineeEnrollment.TraineeID);
            return View(traineeEnrollment);
        }

        // GET: TraineeEnrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraineeEnrollment traineeEnrollment = db.TraineeEnrollments.Find(id);
            if (traineeEnrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", traineeEnrollment.CourseID);
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName", traineeEnrollment.TraineeID);
            return View(traineeEnrollment);
        }

        // POST: TraineeEnrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraineeEnrollments,TraineeID,CourseID")] TraineeEnrollment traineeEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(traineeEnrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", traineeEnrollment.CourseID);
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName", traineeEnrollment.TraineeID);
            return View(traineeEnrollment);
        }

        // GET: TraineeEnrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraineeEnrollment traineeEnrollment = db.TraineeEnrollments.Find(id);
            if (traineeEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(traineeEnrollment);
        }

        // POST: TraineeEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TraineeEnrollment traineeEnrollment = db.TraineeEnrollments.Find(id);
            db.TraineeEnrollments.Remove(traineeEnrollment);
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
