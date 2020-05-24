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
    public class RideReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RideReservations
        public ActionResult Index()
        {
            return View(db.RideReservations.ToList());
        }
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

        [Authorize(Roles = "Driver")]
        public ActionResult Request(string driver)
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            var request = db.RideReservations.Where(x => x.DriverName == driver).ToList();
            return View(request);
        }
        [Authorize(Roles = "Passenger")]
        public ActionResult Status(string passenger)
        {
            var status = db.RideReservations.Where(x => x.PassengerName == passenger).ToList();
            return View(status);
        }

        public string DriverDetails(int? id)
        {
            string contact = "";
            var reservation = db.RideReservations.Find(id);
            if(reservation!=null)
            {
                var driver = db.Rides.Where(x => x.UserName == reservation.DriverName).FirstOrDefault();
                if(driver!=null)
                {
                    contact = driver.ContactNumber;
                }
            }
            return contact;
        }
        [Authorize(Roles = "Driver")]
        public ActionResult Accept(int? id, int seats)
        {
            string name = "";
            var result = db.RideReservations.SingleOrDefault(m => m.ReservationID == id);
            if (result != null)
            {
                name = result.DriverName;
                var result3 = db.Rides.SingleOrDefault(m => m.RideID == result.RideID);
                if (result3 != null)
                {
                    if(result3.AvailableSeats !=0)
                    {
                        result.ReservationStatus = "Accepted";
                        db.SaveChanges();
                        var result2 = db.Rides.SingleOrDefault(m => m.RideID == result.RideID);
                        if (result2 != null)
                        {
                            result2.AvailableSeats -= seats;
                            db.SaveChanges();
                        }
                        var passenger = db.Users.Where(x => x.UserName == result.PassengerName).FirstOrDefault();
                        if (passenger != null)
                        {
                            var passEmail = passenger.Email;

                            var message = @String.Format("<div> Your ride reservation request has been accepted !! <br /> " +
                                "Please return to the app and check your reservation status to contact the driver and discuss travel arrangements.. </div>");

                            EmailController email = new EmailController();

                            email.SendEmail(passEmail, "Ride With Me - Reservation Update", message);
                        }
                    }
                    else
                    {
                        TempData["Message"] = "No seats available!!";
                    }
                }
                
            }
            return RedirectToAction("Request",new { Driver = name });
        }
        [Authorize(Roles = "Driver")]
        public ActionResult Decline(int? id, int seats)
        {
            string name = "";
            var result = db.RideReservations.SingleOrDefault(m => m.ReservationID == id);
            if (result != null)
            {
                name = result.DriverName;
                if (result.ReservationStatus == "Accepted")
                {
                    var result2 = db.Rides.SingleOrDefault(m => m.RideID == result.RideID);
                    if (result2 != null)
                    {
                        result2.AvailableSeats += seats;
                        db.SaveChanges();
                    }
                }
                result.ReservationStatus = "Declined";
                db.SaveChanges();
                var passenger = db.Users.Where(x => x.UserName == result.PassengerName).FirstOrDefault();
                if (passenger != null)
                {
                    var passEmail = passenger.Email;

                    var message = @String.Format("<div> Sorry, the driver is unable to accomodate your reservation request at the moment.. <br />" +
                        " Don't be discouraged !! </br> You may return to the app and search for other rides.. </div>");

                    EmailController email = new EmailController();

                    email.SendEmail(passEmail, "Ride With Me - Reservation Update", message);
                }
            }
            return RedirectToAction("Request", new { Driver = name });
        }



        // GET: RideReservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RideReservation rideReservation = db.RideReservations.Find(id);
            if (rideReservation == null)
            {
                return HttpNotFound();
            }
            return View(rideReservation);
        }

        // GET: RideReservations/Create
        public ActionResult Create()
        {
            RideReservation obj = new RideReservation();
            return View(obj);
        }

        // POST: RideReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Passenger")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,RideID,DriverName,PassengerName,SeatsRequired,ReservationStatus")] RideReservation rideReservation)
        {
            if (ModelState.IsValid)
            {
                db.RideReservations.Add(rideReservation);
                db.SaveChanges();
                TempData["Message"] = "Reservation request successfully submitted!!";
                var driver = db.Users.Where(x => x.UserName == rideReservation.DriverName).FirstOrDefault();
                if (driver != null)
                {
                    var drvEmail = driver.Email;

                    var message = @String.Format("<div> You have a new ride reservation request <br />" +
                    "</br> Please return to the application to manage them. </div>");

                    EmailController email = new EmailController();

                    email.SendEmail(drvEmail, "Ride With Me - New Reservation Request", message);
                }

                return RedirectToAction("Index","Home");
            }

            return View(rideReservation);
        }

        // GET: RideReservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RideReservation rideReservation = db.RideReservations.Find(id);
            if (rideReservation == null)
            {
                return HttpNotFound();
            }
            return View(rideReservation);
        }

        // POST: RideReservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,RideID,DriverName,PassengerName,SeatsRequired,ReservationStatus")] RideReservation rideReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rideReservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(rideReservation);
        }

        // GET: RideReservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RideReservation rideReservation = db.RideReservations.Find(id);
            if (rideReservation == null)
            {
                return HttpNotFound();
            }
            return View(rideReservation);
        }

        // POST: RideReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RideReservation rideReservation = db.RideReservations.Find(id);
            db.RideReservations.Remove(rideReservation);
            db.SaveChanges();
            TempData["Message"] = "Reservation successfully deleted!!";
            return RedirectToAction("Index","Home");
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
