namespace ParkingViolation.Migrations
{
	using ParkingViolation.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<ParkingViolation.DAL.ParkingViolationContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(ParkingViolation.DAL.ParkingViolationContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			var volunteers = new List<Volunteer>
			{
				new Volunteer { FirstMidName = "Kalpesh", LastName = "Patel", PhoneNumber = "1000000001", EmailAddress = "kalpeshpatel-test@gmail.com"},
				new Volunteer { FirstMidName = "Aarzoo", LastName = "Shah", PhoneNumber = "2000000001", EmailAddress = "aarzooshah-test@gmail.com"},
				new Volunteer { FirstMidName = "John", LastName = "J", PhoneNumber = "3000000001", EmailAddress = "johnj-test@gmail.com"},
				new Volunteer { FirstMidName = "John", LastName = "Lock", PhoneNumber = "4000000001", EmailAddress = "johnlock-test@gmail.com"},
				new Volunteer { FirstMidName = "Michael", LastName = "Shah", PhoneNumber = "5000000001", EmailAddress = "michaelshah-test@gmail.com"},
				new Volunteer { FirstMidName = "Rajiv", LastName = "Gandhi", PhoneNumber = "6000000001", EmailAddress = "rajivgandhi-test@gmail.com"},
				new Volunteer { FirstMidName = "John", LastName = "Piazza", PhoneNumber = "7000000001", EmailAddress = "johnpiazza-test@gmail.com"},
				new Volunteer { FirstMidName = "Brian", LastName = "Smith", PhoneNumber = "8000000001", EmailAddress = "briansmith-test@gmail.com"},
				new Volunteer { FirstMidName = "Frank", LastName = "Kampalia", PhoneNumber = "1100000001", EmailAddress = "frankkampalia-test@gmail.com"},
				new Volunteer { FirstMidName = "Amy", LastName = "Buttler", PhoneNumber = "1200000001", EmailAddress = "amybuttler-test@gmail.com"}
			};
			volunteers.ForEach(v => context.Volunteers.AddOrUpdate(x => x.PhoneNumber, v));
			context.SaveChanges();

			var officers = new List<Officer>
			{
				new Officer { FirstMidName = "Manish", LastName = "Patel", PhoneNumber = "9000000001", EmailAddress = "manish-officer@gmail.com", HireDate = DateTime.Parse("2016-01-15")},
				new Officer {FirstMidName = "Nilesh", LastName = "Shah", PhoneNumber = "9000000002", EmailAddress = "nilesh-officer@gmail.com" , HireDate = DateTime.Parse("2016 02 05")}
			};
			officers.ForEach(o => context.Officers.AddOrUpdate(x => x.PhoneNumber, o));
			context.SaveChanges();

			var vehicles = new List<Vehicle>
			{
				new Vehicle { State = "PA", NumberPlate = "100001", VolunteerID = volunteers.Single(v => v.PhoneNumber == "1000000001").PersonID },
				new Vehicle { State = "NY", NumberPlate = "100002", VolunteerID = volunteers.Single(v => v.PhoneNumber == "1000000001").PersonID },
				new Vehicle { State = "NJ", NumberPlate = "100003", VolunteerID = volunteers.Single(v => v.PhoneNumber == "1000000001").PersonID },
				new Vehicle { State = "NJ", NumberPlate = "200001", VolunteerID = volunteers.Single(v => v.PhoneNumber == "2000000001").PersonID },
				new Vehicle { State = "VA", NumberPlate = "300001", VolunteerID = volunteers.Single(v => v.PhoneNumber == "3000000001").PersonID },
				new Vehicle { State = "NJ", NumberPlate = "300002", VolunteerID = volunteers.Single(v => v.PhoneNumber == "3000000001").PersonID }
			};
			vehicles.ForEach(v => context.Vehicles.AddOrUpdate(x => new { x.State, x.NumberPlate }, v));
			context.SaveChanges();

			var violations = new List<Violation>
			{
				new Violation { State = "PA", NumberPlate = "100001", ViolationDateAndTime = DateTime.Parse("2016-05-25 11:03"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000001").PersonID, Comments = "Can't park here."},
				new Violation { State = "PA", NumberPlate = "100001", ViolationDateAndTime = DateTime.Parse("2016-05-27 15:45"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000001").PersonID, Comments = "Can't park here...again"},
				new Violation { State = "PA", NumberPlate = "100001", ViolationDateAndTime = DateTime.Parse("2016-05-28 11:39"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000001").PersonID, Comments = "Violated parking rules."},
				new Violation { State = "NY", NumberPlate = "100002", ViolationDateAndTime = DateTime.Parse("2016-05-29 13:47"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000002").PersonID, Comments = "Was parked in Lot 1."},

				new Violation { State = "NJ", NumberPlate = "200001", ViolationDateAndTime = DateTime.Parse("2016-04-25 19:08"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000001").PersonID, Comments = "Violated parking rules again."},
				new Violation { State = "NJ", NumberPlate = "200001", ViolationDateAndTime = DateTime.Parse("2016-04-29 15:06"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000002").PersonID, Comments = "Parked in visitor parking."},
				new Violation { State = "NJ", NumberPlate = "300002", ViolationDateAndTime = DateTime.Parse("2016-03-05 17:01"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000002").PersonID, Comments = "Violation violation violation ..."},
				new Violation { State = "VA", NumberPlate = "300001", ViolationDateAndTime = DateTime.Parse("2016-05-04 09:56"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000002").PersonID, Comments = "Please don't park here."},
				new Violation { State = "NY", NumberPlate = "100002", ViolationDateAndTime = DateTime.Parse("2016-03-26 14:25"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000002").PersonID, Comments = "Was parked in Lot 1 again."},
				new Violation { State = "NJ", NumberPlate = "300002", ViolationDateAndTime = DateTime.Parse("2016-04-05 16:47"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000001").PersonID, Comments = "Violation violation violation ..."},
				new Violation { State = "NJ", NumberPlate = "300002", ViolationDateAndTime = DateTime.Parse("2016-04-14 14:06"), OfficerID = officers.Single(o => o.PhoneNumber == "9000000001").PersonID, Comments = "Violation violation violation and violation..."},
			};
			violations.ForEach(v => context.Violations.AddOrUpdate(x => new { x.State, x.NumberPlate, x.ViolationDateAndTime }, v));
			context.SaveChanges();
		}
	}
}
