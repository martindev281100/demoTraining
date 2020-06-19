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
    [Authorize (Roles = "Staff, Trainee")]
    public class TraineesController : Controller
    {
        private TrainingDBEntities db = new TrainingDBEntities();

       

        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var trainee = from s in db.Trainees
                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                trainee = trainee.Where(t => t.TraineeName.Contains(searchString)
                                               || t.TraineeEmail.Contains(searchString)|| t.ProgrammingLanguage.Contains(searchString)
                                               || t.Education.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    trainee = trainee.OrderByDescending(s => s.TraineeName);
                    break;
                case "score":
                    trainee = trainee.OrderBy(s => s.ToeicScore);
                    break;
               
                default:
                    trainee = trainee.OrderBy(s => s.TraineeName);
                    break;
            }
            return View(trainee.ToList());
        }
        // GET: Trainees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }
        [Authorize(Roles = "Staff")]
        // GET: Trainees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraineeID,TraineeName,TraineeEmail,DOB,Education,ProgrammingLanguage,ToeicScore,Address")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Trainees.Add(trainee);
                db.SaveChanges();
                AuthenController.CreateAccount(trainee.TraineeEmail, "123456", "Trainee");
                return RedirectToAction("Index");
            }

            return View(trainee);
        }
        [Authorize(Roles = "Staff")]
        // GET: Trainees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraineeID,TraineeName,TraineeEmail,DOB,Education,ProgrammingLanguage,ToeicScore,Address")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainee);
        }
        [Authorize(Roles = "Staff")]

        // GET: Trainees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            db.Trainees.Remove(trainee);
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
        public ActionResult Profile()
        {
            var userName = User.Identity.Name;
            //var student = db.Students.Where(s => s.StudentID.Equals(userName));
            var trainee = (from t in db.Trainees where t.TraineeEmail.Equals(userName) select t)
                .FirstOrDefault();
            return View(trainee);
        }

        public ActionResult MyCourse()
        {
            var userName = User.Identity.Name;
            var enrollments = from e in db.TraineeEnrollments
                where e.Trainee.TraineeEmail.Equals(userName)
                select e;
            return View(enrollments.ToList());
        }
    }
}
