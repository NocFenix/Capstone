using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final.UI.Models;

namespace Final.UI.Controllers
{
    public class DaysController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: Days
        public ActionResult Index()
        {
            return View(db.Days.ToList().OrderBy(x => x.Date)); //order by date
        }

        // GET: Days/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }

            User opener = (from u in db.Users where u.Id == day.OpeningShift select u).FirstOrDefault();
            ViewBag.OpeningShift = opener.FirstName;
            User morning1 = (from u in db.Users where u.Id == day.MorningShift1 select u).FirstOrDefault();
            ViewBag.MorningShift1 = morning1.FirstName;
            User morning2 = (from u in db.Users where u.Id == day.MorningShift2 select u).FirstOrDefault();
            ViewBag.MorningShift2 = morning2.FirstName;
            User lunch1 = (from u in db.Users where u.Id == day.LunchShift1 select u).FirstOrDefault();
            ViewBag.LunchShift1 = lunch1.FirstName;
            User lunch2 = (from u in db.Users where u.Id == day.LunchShift2 select u).FirstOrDefault();
            ViewBag.LunchShift2 = lunch2.FirstName;
            User after1 = (from u in db.Users where u.Id == day.AfternoonShift1 select u).FirstOrDefault();
            ViewBag.AfternoonShift1 = after1.FirstName;
            User after2 = (from u in db.Users where u.Id == day.AfternoonShift2 select u).FirstOrDefault();
            ViewBag.AfternoonShift2 = after2.FirstName;
            User closer = (from u in db.Users where u.Id == day.ClosingShift select u).FirstOrDefault();
            ViewBag.ClosingShift = closer.FirstName;

            return View(day);
        }

        // GET: Days/Create
        public ActionResult Create()
        {
            List<User> Openers = (from u in db.Users
                                              join us in db.UserShifts
                                              on u.Id equals us.UserId
                                              where us.ShiftId == 1 && u.Active == true
                                              select u).ToList();
            var selectList = new SelectList(Openers, "Id", "FirstName", "Id");
            ViewBag.OpeningSelection = selectList;
            selectList = null;

            List<User> Mornings = (from u in db.Users
                                              join us in db.UserShifts
                                              on u.Id equals us.UserId
                                              where us.ShiftId == 2 && u.Active == true
                                              select u).ToList();
            selectList = new SelectList(Mornings, "Id", "FirstName", "Id");
            ViewBag.MorningSelection = selectList;
            selectList = null;

            List<User> Lunches = (from u in db.Users
                                              join us in db.UserShifts
                                              on u.Id equals us.UserId
                                              where us.ShiftId == 3 && u.Active == true
                                              select u).ToList();
            selectList = new SelectList(Lunches, "Id", "FirstName", "Id");
            ViewBag.LunchSelection = selectList;
            selectList = null;

            List<User> Afternoons = (from u in db.Users
                                              join us in db.UserShifts
                                              on u.Id equals us.UserId
                                              where us.ShiftId == 4 && u.Active == true
                                              select u).ToList();
            Afternoons.Select(m => new SelectListItem { Text = m.FirstName + m.LastName, Value = m.Id.ToString() });
            selectList = new SelectList(Afternoons, "Id", "FirstName", "Id");
            ViewBag.AfternoonSelection = selectList;
            selectList = null;

            List<User> Closers = (from u in db.Users
                                                 join us in db.UserShifts
                                                 on u.Id equals us.UserId
                                                 where us.ShiftId == 5 && u.Active == true
                                                 select u).ToList();
            selectList = new SelectList(Closers, "Id", "FirstName", "Id");
            ViewBag.CloserSelection = selectList;
            selectList = null;

            DayShiftViewModel dvm = new DayShiftViewModel();
            return View(dvm);
        }

        // POST: Days/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Day,Shifts,Users")] DayShiftViewModel dvm)
        {
            List<User> Openers = (from u in db.Users
                                  join us in db.UserShifts
                                  on u.Id equals us.UserId
                                  where us.ShiftId == 1 && u.Active == true
                                  select u).ToList();
            var selectList = new SelectList(Openers, "Id", "FirstName", "Id");
            ViewBag.OpeningSelection = selectList;
            selectList = null;

            List<User> Mornings = (from u in db.Users
                                   join us in db.UserShifts
                                   on u.Id equals us.UserId
                                   where us.ShiftId == 2 && u.Active == true
                                   select u).ToList();
            selectList = new SelectList(Mornings, "Id", "FirstName", "Id");
            ViewBag.MorningSelection = selectList;
            selectList = null;

            List<User> Lunches = (from u in db.Users
                                  join us in db.UserShifts
                                  on u.Id equals us.UserId
                                  where us.ShiftId == 3 && u.Active == true
                                  select u).ToList();
            selectList = new SelectList(Lunches, "Id", "FirstName", "Id");
            ViewBag.LunchSelection = selectList;
            selectList = null;

            List<User> Afternoons = (from u in db.Users
                                     join us in db.UserShifts
                                     on u.Id equals us.UserId
                                     where us.ShiftId == 4 && u.Active == true
                                     select u).ToList();
            Afternoons.Select(m => new SelectListItem { Text = m.FirstName + m.LastName, Value = m.Id.ToString() });
            selectList = new SelectList(Afternoons, "Id", "FirstName", "Id");
            ViewBag.AfternoonSelection = selectList;
            selectList = null;

            List<User> Closers = (from u in db.Users
                                  join us in db.UserShifts
                                  on u.Id equals us.UserId
                                  where us.ShiftId == 5 && u.Active == true
                                  select u).ToList();
            selectList = new SelectList(Closers, "Id", "FirstName", "Id");
            ViewBag.CloserSelection = selectList;
            selectList = null;

            if (dvm.Day.OpeningShift == dvm.Day.MorningShift1 || dvm.Day.OpeningShift == dvm.Day.MorningShift2 ||
                dvm.Day.OpeningShift == dvm.Day.LunchShift1 || dvm.Day.OpeningShift == dvm.Day.LunchShift2 ||
                dvm.Day.OpeningShift == dvm.Day.AfternoonShift1 || dvm.Day.OpeningShift == dvm.Day.AfternoonShift2 ||
                dvm.Day.OpeningShift == dvm.Day.ClosingShift ||
                //MorningShift1
                dvm.Day.MorningShift1 == dvm.Day.MorningShift2 ||
                dvm.Day.MorningShift1 == dvm.Day.LunchShift1 || dvm.Day.MorningShift1 == dvm.Day.LunchShift2 ||
                dvm.Day.MorningShift1 == dvm.Day.AfternoonShift1 || dvm.Day.MorningShift1 == dvm.Day.AfternoonShift2 ||
                dvm.Day.MorningShift1 == dvm.Day.ClosingShift ||
                //MorningShift2
                dvm.Day.MorningShift2 == dvm.Day.LunchShift1 || dvm.Day.MorningShift2 == dvm.Day.LunchShift2 ||
                dvm.Day.MorningShift2 == dvm.Day.AfternoonShift1 || dvm.Day.MorningShift2 == dvm.Day.AfternoonShift2 ||
                dvm.Day.MorningShift2 == dvm.Day.ClosingShift ||
                //LunchShift1
                dvm.Day.LunchShift1 == dvm.Day.LunchShift2 ||
                dvm.Day.LunchShift1 == dvm.Day.AfternoonShift1 || dvm.Day.LunchShift1 == dvm.Day.AfternoonShift2 ||
                dvm.Day.LunchShift1 == dvm.Day.ClosingShift ||
                //LunchShift2
                dvm.Day.LunchShift2 == dvm.Day.AfternoonShift1 || dvm.Day.LunchShift2 == dvm.Day.AfternoonShift2 ||
                dvm.Day.LunchShift2 == dvm.Day.ClosingShift ||
                //AfternoonShift1
                dvm.Day.AfternoonShift1 == dvm.Day.AfternoonShift2 || dvm.Day.AfternoonShift1 == dvm.Day.ClosingShift ||
                //AfternoonShift2
                dvm.Day.AfternoonShift2 == dvm.Day.ClosingShift)
            {
                ViewBag.Duplicate = "Cannot schedule a worker for more than one shift.";

                return View(dvm);
            }

            List<Day> dates = db.Days.ToList();
            foreach (Day d in dates)
            {
                if (d.Date == dvm.Day.Date)
                {
                    ViewBag.Duplicate = "Another schedule with this date already exists!";
                    return View(dvm);
                }
            }

            if (ModelState.IsValid)
            {
                db.Days.Add(dvm.Day);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dvm);
        }

        // GET: Days/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }

            List<User> Openers = (from u in db.Users
                                  join us in db.UserShifts
                                  on u.Id equals us.UserId
                                  where us.ShiftId == 1 && u.Active == true
                                  select u).ToList();
            var selectList = new SelectList(Openers, "Id", "FirstName", "Id");
            ViewBag.OpeningSelection = selectList;
            selectList = null;

            List<User> Mornings = (from u in db.Users
                                   join us in db.UserShifts
                                   on u.Id equals us.UserId
                                   where us.ShiftId == 2 && u.Active == true
                                   select u).ToList();
            selectList = new SelectList(Mornings, "Id", "FirstName", "Id");
            ViewBag.MorningSelection = selectList;
            selectList = null;

            List<User> Lunches = (from u in db.Users
                                  join us in db.UserShifts
                                  on u.Id equals us.UserId
                                  where us.ShiftId == 3 && u.Active == true
                                  select u).ToList();
            selectList = new SelectList(Lunches, "Id", "FirstName", "Id");
            ViewBag.LunchSelection = selectList;
            selectList = null;

            List<User> Afternoons = (from u in db.Users
                                     join us in db.UserShifts
                                     on u.Id equals us.UserId
                                     where us.ShiftId == 4 && u.Active == true
                                     select u).ToList();
            Afternoons.Select(m => new SelectListItem { Text = m.FirstName + m.LastName, Value = m.Id.ToString() });
            selectList = new SelectList(Afternoons, "Id", "FirstName", "Id");
            ViewBag.AfternoonSelection = selectList;
            selectList = null;

            List<User> Closers = (from u in db.Users
                                  join us in db.UserShifts
                                  on u.Id equals us.UserId
                                  where us.ShiftId == 5 && u.Active == true
                                  select u).ToList();
            selectList = new SelectList(Closers, "Id", "FirstName", "Id");
            ViewBag.CloserSelection = selectList;
            selectList = null;

            DayShiftViewModel dvm = new DayShiftViewModel()
            {
                Day = day
            };

            return View(dvm);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Day,Shifts,Users")] DayShiftViewModel dvm)
        {
            if (dvm.Day.OpeningShift == dvm.Day.MorningShift1 || dvm.Day.OpeningShift == dvm.Day.MorningShift2 ||
               dvm.Day.OpeningShift == dvm.Day.LunchShift1 || dvm.Day.OpeningShift == dvm.Day.LunchShift2 ||
               dvm.Day.OpeningShift == dvm.Day.AfternoonShift1 || dvm.Day.OpeningShift == dvm.Day.AfternoonShift2 ||
               dvm.Day.OpeningShift == dvm.Day.ClosingShift ||
               //MorningShift1
               dvm.Day.MorningShift1 == dvm.Day.MorningShift2 ||
               dvm.Day.MorningShift1 == dvm.Day.LunchShift1 || dvm.Day.MorningShift1 == dvm.Day.LunchShift2 ||
               dvm.Day.MorningShift1 == dvm.Day.AfternoonShift1 || dvm.Day.MorningShift1 == dvm.Day.AfternoonShift2 ||
               dvm.Day.MorningShift1 == dvm.Day.ClosingShift ||
               //MorningShift2
               dvm.Day.MorningShift2 == dvm.Day.LunchShift1 || dvm.Day.MorningShift2 == dvm.Day.LunchShift2 ||
               dvm.Day.MorningShift2 == dvm.Day.AfternoonShift1 || dvm.Day.MorningShift2 == dvm.Day.AfternoonShift2 ||
               dvm.Day.MorningShift2 == dvm.Day.ClosingShift ||
               //LunchShift1
               dvm.Day.LunchShift1 == dvm.Day.LunchShift2 ||
               dvm.Day.LunchShift1 == dvm.Day.AfternoonShift1 || dvm.Day.LunchShift1 == dvm.Day.AfternoonShift2 ||
               dvm.Day.LunchShift1 == dvm.Day.ClosingShift ||
               //LunchShift2
               dvm.Day.LunchShift2 == dvm.Day.AfternoonShift1 || dvm.Day.LunchShift2 == dvm.Day.AfternoonShift2 ||
               dvm.Day.LunchShift2 == dvm.Day.ClosingShift ||
               //AfternoonShift1
               dvm.Day.AfternoonShift1 == dvm.Day.AfternoonShift2 || dvm.Day.AfternoonShift1 == dvm.Day.ClosingShift ||
               //AfternoonShift2
               dvm.Day.AfternoonShift2 == dvm.Day.ClosingShift)
            {
                ViewBag.Duplicate = "Cannot schedule a worker for more than one shift.";

                List<User> Openers = (from u in db.Users
                                      join us in db.UserShifts
                                      on u.Id equals us.UserId
                                      where us.ShiftId == 1 && u.Active == true
                                      select u).ToList();
                var selectList = new SelectList(Openers, "Id", "FirstName", "Id");
                ViewBag.OpeningSelection = selectList;
                selectList = null;

                List<User> Mornings = (from u in db.Users
                                       join us in db.UserShifts
                                       on u.Id equals us.UserId
                                       where us.ShiftId == 2 && u.Active == true
                                       select u).ToList();
                selectList = new SelectList(Mornings, "Id", "FirstName", "Id");
                ViewBag.MorningSelection = selectList;
                selectList = null;

                List<User> Lunches = (from u in db.Users
                                      join us in db.UserShifts
                                      on u.Id equals us.UserId
                                      where us.ShiftId == 3 && u.Active == true
                                      select u).ToList();
                selectList = new SelectList(Lunches, "Id", "FirstName", "Id");
                ViewBag.LunchSelection = selectList;
                selectList = null;

                List<User> Afternoons = (from u in db.Users
                                         join us in db.UserShifts
                                         on u.Id equals us.UserId
                                         where us.ShiftId == 4 && u.Active == true
                                         select u).ToList();
                Afternoons.Select(m => new SelectListItem { Text = m.FirstName + m.LastName, Value = m.Id.ToString() });
                selectList = new SelectList(Afternoons, "Id", "FirstName", "Id");
                ViewBag.AfternoonSelection = selectList;
                selectList = null;

                List<User> Closers = (from u in db.Users
                                      join us in db.UserShifts
                                      on u.Id equals us.UserId
                                      where us.ShiftId == 5 && u.Active == true
                                      select u).ToList();
                selectList = new SelectList(Closers, "Id", "FirstName", "Id");
                ViewBag.CloserSelection = selectList;
                selectList = null;

                return View(dvm);
            }

            if (ModelState.IsValid)
            {
                db.Entry(dvm.Day).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dvm);
        }

        // GET: Days/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Day day = db.Days.Find(id);
            if (day == null)
            {
                return HttpNotFound();
            }
            return View(day);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Day day = db.Days.Find(id);
            db.Days.Remove(day);
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
