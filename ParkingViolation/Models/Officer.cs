using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ParkingViolation.Models
{
	public class Officer : Person
	{
		[Required]
		[Display(Name = "Hire Date")]
		public DateTime HireDate { get; set; }

		public virtual ICollection<Violation> Violations { get; set; }
	}
}