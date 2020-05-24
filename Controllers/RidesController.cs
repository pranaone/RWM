using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ridewithme.Models;

namespace Ridewithme.Controllers
{
    [Authorize]
    public class RidesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rides
        public ActionResult Index()
        {
            return View(db.Rides.ToList());
        }

        // GET: Rides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return HttpNotFound();
            }
            return View(ride);
        }

        public ActionResult Search(string start, string destination)
        {
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(destination))
            {
                TempData["Message"] = "Please enter valid start and destination locations!!";
                return RedirectToAction("Index", "Home");
            }
            return View(db.Rides.Where(f => f.Type == 0 && f.Origin.Contains(start) || f.Destination.Contains(destination)).ToList());
        }

        public ActionResult MyRides(string username)
        {    
            return View(db.Rides.Where(f => f.Type == 0 && f.UserName == username).ToList());
        }

        public ActionResult Offers()
        {
            return View(db.Rides.Where(x => x.Type == 0).ToList());
        }

        public ActionResult Requests()
        {
            return View(db.Rides.Where(x => x.Type != 0).ToList());
        }

        public ActionResult CreateRideRequest()
        {
            Ride ride = new Ride();
            return View(ride);
        }

        // POST: Rides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRideRequest([Bind(Include = "RideID,Type,Origin,Destination,Schedule,Occurance,VehicleType,Model,Color,AirConditioned,AvailableSeats,Contribution,Message,ContactNumber,IsNonSmoking,IsMaleOnly,IsFemaleOnly,DateAdded,UserName")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                db.Rides.Add(ride);
                db.SaveChanges();
                TempData["Message"] = "Ride Request Successfully Saved!!";
                return RedirectToAction("Index", "Home");
            }

            return View(ride);
        }

        // GET: Rides/Create
        public ActionResult Create()
        {
            Ride ride = new Ride();
            return View(ride);
        }

        // POST: Rides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RideID,Type,Origin,Destination,Schedule,Occurance,VehicleType,Model,Color,AirConditioned,AvailableSeats,Contribution,Message,ContactNumber,IsNonSmoking,IsMaleOnly,IsFemaleOnly,DateAdded,UserName")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                db.Rides.Add(ride);
                db.SaveChanges();
                TempData["Message"] = "Ride Successfully Saved!!";
                return RedirectToAction("Index", "Home");
            }

            return View(ride);
        }

        // GET: Rides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return HttpNotFound();
            }
            return View(ride);
        }

        // POST: Rides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RideID,Type,Origin,Destination,Schedule,Occurance,VehicleType,Model,Color,AirConditioned,AvailableSeats,Contribution,Message,ContactNumber,IsNonSmoking,IsMaleOnly,IsFemaleOnly,DateAdded,UserName")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ride).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Ride Successfully Updated!!";
                return RedirectToAction("Index", "Home");
            }
            return View(ride);
        }

        // GET: Rides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ride ride = db.Rides.Find(id);
            if (ride == null)
            {
                return HttpNotFound();
            }
            return View(ride);
        }

        // POST: Rides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ride ride = db.Rides.Find(id);
            db.Rides.Remove(ride);
            db.SaveChanges();
            TempData["Message"] = "Ride Successfully Deleted!!";
            return RedirectToAction("Index", "Home");
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
