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

    public class TrainerEnrollmentsController : Controller
    {
        private TrainingDBEntities db = new TrainingDBEntities();

        // GET: TrainerEnrollments
        public ActionResult Index()
        {
            var trainerEnrollments = db.TrainerEnrollments.Include(t => t.Topic).Include(t => t.Trainer);
            return View(trainerEnrollments.ToList());
        }

        // GET: TrainerEnrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerEnrollment trainerEnrollment = db.TrainerEnrollments.Find(id);
            if (trainerEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(trainerEnrollment);
        }

        // GET: TrainerEnrollments/Create
        public ActionResult Create()
        {
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName");
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName");
            return View();
        }

        // POST: TrainerEnrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerEnrollmentID,TrainerID,TopicID")] TrainerEnrollment trainerEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.TrainerEnrollments.Add(trainerEnrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerEnrollment.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", trainerEnrollment.TrainerID);
            return View(trainerEnrollment);
        }

        // GET: TrainerEnrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerEnrollment trainerEnrollment = db.TrainerEnrollments.Find(id);
            if (trainerEnrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerEnrollment.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", trainerEnrollment.TrainerID);
            return View(trainerEnrollment);
        }

        // POST: TrainerEnrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerEnrollmentID,TrainerID,TopicID")] TrainerEnrollment trainerEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainerEnrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerEnrollment.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerName", trainerEnrollment.TrainerID);
            return View(trainerEnrollment);
        }

        // GET: TrainerEnrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerEnrollment trainerEnrollment = db.TrainerEnrollments.Find(id);
            if (trainerEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(trainerEnrollment);
        }

        // POST: TrainerEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainerEnrollment trainerEnrollment = db.TrainerEnrollments.Find(id);
            db.TrainerEnrollments.Remove(trainerEnrollment);
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
