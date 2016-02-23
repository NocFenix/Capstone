using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final.UI.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Final.UI.Controllers
{
    public class UsersController : Controller
    {
        private CapstoneEntities db = new CapstoneEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.OrderByDescending(u => u.Active).ThenBy(u => u.LastName).ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var viewModel = new UserShiftViewModel
            {
                AllShifts = db.Shifts.ToList()
            };

            return View(viewModel);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AllShifts,AssignedShifts,User")] UserShiftViewModel uvm)
        {
            User user = uvm.User; //the user being created

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();

                int nextUserId = db.Users.ToList().LastOrDefault().Id; //get newly created user id

                //create UserShifts for the new user
                foreach (Shift s in uvm.AllShifts)
                {
                    if (s.Include)
                    {
                        UserShift us = new UserShift
                        {
                            ShiftId = s.Id,
                            UserId = nextUserId,
                            Active = true
                        };
                        db.UserShifts.Add(us);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var us = user.UserShifts;
            List<Shift> shifts = new List<Shift>();

            foreach (UserShift s in us)
            {
                if (s.Active)
                    shifts.Add((from shift in db.Shifts where shift.Id == s.ShiftId == true select shift).SingleOrDefault());
            }

            var viewModel = new UserShiftViewModel
            {
                User = user,
                AssignedShifts = shifts,
                AllShifts = db.Shifts.ToList()
            };
            return View(viewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AllShifts,AssignedShifts,User")] UserShiftViewModel uvm)
        {
            #region declarations/assignments
            User user = uvm.User; //the user being editted

            List<UserShift> userShifts = db.UserShifts.ToList(); //all UserRoles
            List<UserShift> currUserShifts = new List<UserShift>(); //for the current user's UserShifts
            List<int> newShift = new List<int>(); //for new shifts that were added

            //get the current user's UserShifts
            foreach (UserShift us in userShifts)
            {
                if (us.UserId == user.Id)
                    currUserShifts.Add(us);
            }
            #endregion

            if (ModelState.IsValid)
            {
                #region loop thru roles to see what was added/removed
                //loop thru all of the shifts
                for (int i = 0; i < uvm.AllShifts.Count; i++)
                {
                    if (uvm.AllShifts[i].Include) //it is a checked box, so we need to add the shift
                    {
                        int counter = 0;
                        //loop thru shifts associated to the user
                        for (int j = 0; j < currUserShifts.Count; j++)
                        {
                            if (uvm.AllShifts[i].Id == currUserShifts[j].ShiftId) //if the checked shift is already associated
                            {
                                if (!currUserShifts[j].Active) //if the shift is NOT currently active
                                {
                                    currUserShifts[i].Active = true; //activate shifts
                                    db.Entry(currUserShifts[i]).State = EntityState.Modified;
                                }
                            }
                            else
                            {
                                counter++;
                            }
                        }
                        if (counter == currUserShifts.Count)
                            newShift.Add(uvm.AllShifts[i].Id);
                    }
                    else //the box is not checked, see if we need to remove the shifts
                    {
                        for (int j = 0; j < currUserShifts.Count; j++)
                        {
                            if (uvm.AllShifts[i].Id == currUserShifts[j].ShiftId)
                            {
                                currUserShifts[i].Active = false;
                                db.Entry(currUserShifts[i]).State = EntityState.Modified;
                            }
                        }
                    }
                }
                #endregion

                #region activate/add shifts accordingly
                for (int i = 0; i < newShift.Count; i++)
                {
                    UserShift us = new UserShift
                    {
                        ShiftId = newShift[i],
                        UserId = user.Id,
                        Active = true
                    };
                    db.UserShifts.Add(us);
                }
                #endregion

                db.Entry(user).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                validationError.PropertyName,
                                validationError.ErrorMessage);
                        }
                    }

                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
