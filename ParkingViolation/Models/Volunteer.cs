using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParkingViolation.Models
{
	public class Volunteer : Person
	{
		public virtual ICollection<Vehicle> Vehicles { get; set; }
	}
}