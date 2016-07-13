using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ParkingViolation.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ParkingViolation.DAL
{
	public class ParkingViolationContext : DbContext
	{
		public ParkingViolationContext() : base("ParkingViolationContext")
		{

		}

		public DbSet<Person> People { get; set; }
		public DbSet<Volunteer> Volunteers { get; set; }
		public DbSet<Officer> Officers { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		//public DbSet<VehicleSticker> VehicleStickers { get; set; }
		public DbSet<Violation> Violations { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}		
	}
}