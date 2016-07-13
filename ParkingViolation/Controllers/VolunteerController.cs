using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParkingViolation.DAL;
using ParkingViolation.Models;

namespace ParkingViolation.Controllers
{
	public class VolunteerController : Controller
	{
		private ParkingViolationContext db = new ParkingViolationContext();

		// GET: Volunteer
		public ActionResult Index(string sortOrder)
		{
			ViewBag.FirstNameSortOrder = (String.IsNullOrEmpty(sortOrder) ? "FirstName_Desc" : String.Empty);
			ViewBag.LastNameSortOrder = (sortOrder == "LastName_Asc" ? "LastName_Desc" : "LastName_Asc");

			var volunteers = from v in db.Volunteers
							 select v;


			switch (sortOrder)
			{
				case "FirstName_Desc":
					volunteers = volunteers.OrderByDescending(v => v.FirstMidName);
					break;
				case "LastName_Asc":
					volunteers = volunteers.OrderBy(v => v.LastName);
					break;
				case "LastName_Desc":
					volunteers = volunteers.OrderByDescending(v => v.LastName);
					break;
				default:
					volunteers = volunteers.OrderBy(v => v.FirstMidName);
					break;
			}

			//return View(db.Volunteers.ToList());
			return View(volunteers.ToList());
		}

		// GET: Volunteer/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var volunteer = db.Volunteers.Find(id);
			if (volunteer == null)
			{
				return HttpNotFound();
			}
			return View(volunteer);
		}

		// GET: Volunteer/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Volunteer/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "FirstMidName,LastName,PhoneNumber,EmailAddress")] Volunteer volunteer)
		{
			try
			{
				if (PhoneNumberAlreadyInUse(volunteer.PhoneNumber))
				{
					ModelState.AddModelError("PhoneNumber", "Phone number is already associated with another urser.");
				}

				if (ModelState.IsValid)
				{
					db.People.Add(volunteer);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (DataException)
			{
				ModelState.AddModelError("", "Unable to save changes. Try again.");
			}

			return View(volunteer);
		}

		// GET: Volunteer/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var volunteer = db.Volunteers.Find(id);
			if (volunteer == null)
			{
				return HttpNotFound();
			}
			return View(volunteer);
		}

		// POST: Volunteer/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ActionName("Edit")]
		[ValidateAntiForgeryToken]
		//public ActionResult Edit([Bind(Include = "FirstMidName,LastName,PhoneNumber,EmailAddress")] Volunteer volunteer)
		public ActionResult EditPost(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var volunteerToUpdate = db.Volunteers.Find(id);
			var initialPhoneNumber = volunteerToUpdate.PhoneNumber;

			if (TryUpdateModel(volunteerToUpdate, "", new string[] { "FirstMidName", "LastName", "PhoneNumber", "EmailAddress" }))
			{
				try
				{
					if ((volunteerToUpdate.PhoneNumber != initialPhoneNumber) && (PhoneNumberAlreadyInUse(volunteerToUpdate.PhoneNumber)))
					{
						ModelState.AddModelError("PhoneNumber", "Phone number is already associated with another urser.");
					}

					if (ModelState.IsValid)
					{
						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}
				catch (DataException)
				{
					ModelState.AddModelError("", "Unable to save changes. Try again.");
				}
			}

			return View(volunteerToUpdate);
		}

		// GET: Volunteer/Delete/5
		public ActionResult Delete(int? id, bool? saveChangesError = false)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var volunteer = db.Volunteers.Find(id);

			if (volunteer == null)
			{
				return HttpNotFound();
			}

			if (saveChangesError.GetValueOrDefault())
			{
				ViewBag.ErrorMessage = "Delete failed. Try again.";
			}

			return View(volunteer);
		}

		// POST: Volunteer/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			try
			{
				//var volunteer = db.Volunteers.Find(id);
				//db.Volunteers.Remove(volunteer);
				var volunteerToDelete = new Volunteer { PersonID = id };
				db.Entry(volunteerToDelete).State = EntityState.Deleted;
				db.SaveChanges();
			}
			catch (DataException)
			{
				return RedirectToAction("Delete", new { id = id, saveChangesError = true });
			}
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

		public ActionResult ValidatePhoneNumber(string PhoneNumber, string initialPhoneNumber)
		{
			if (PhoneNumber == initialPhoneNumber)
			{
				return Json(true, JsonRequestBehavior.AllowGet);
			}

			return Json(!db.Volunteers.Any(v => v.PhoneNumber == PhoneNumber), JsonRequestBehavior.AllowGet);
		}

		public bool PhoneNumberAlreadyInUse(string PhoneNumber)
		{
			return db.Volunteers.Any(v => v.PhoneNumber == PhoneNumber);
		}
	}
}