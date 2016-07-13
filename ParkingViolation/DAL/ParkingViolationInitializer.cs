using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingViolation.DAL
{
	public class ParkingViolationInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ParkingViolationContext>
	{
		protected override void Seed(ParkingViolationContext context)
		{
			base.Seed(context);
		}
	}
}