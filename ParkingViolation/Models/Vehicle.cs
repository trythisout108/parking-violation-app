using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingViolation.Models
{
	public class Vehicle
	{
		[Key]
		[Column(Order = 1)]
		[StringLength(2)]
		public string State { get; set; }
		
		[Key]
		[Column(Order = 2)]
		[Display(Name = "Number Plate")]
		[StringLength(6)]
		public string NumberPlate { get; set; }
		
		[ForeignKey("Owner")]
		public int VolunteerID { get; set; }

		public virtual Volunteer Owner { get; set; }

		public virtual ICollection<Violation> Violations { get; set; }
	}
}

