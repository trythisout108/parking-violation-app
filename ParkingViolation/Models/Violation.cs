using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ParkingViolation.Models
{
	public class Violation
	{
		//[Key]
		public int ViolationID { get; set; }

		[Required]
		[Column(Order = 1)]
		[ForeignKey("Vehicle")]
		public string State { get; set; }

		[Required]
		[Column(Order = 2)]
		[Display(Name = "Number Plate")]
		[ForeignKey("Vehicle")]
		public string NumberPlate { get; set; }

		[Required]
		[Display(Name = "Violation Date & Time")]
		[DataType(DataType.DateTime)]
		public DateTime ViolationDateAndTime { get; set; }

		[ForeignKey("Officer")]
		public int OfficerID { get; set; }

		[Required]
		[StringLength(500)]
		public string Comments { get; set; }

		public virtual Officer Officer { get; set; }

		public virtual Vehicle Vehicle { get; set; }
	}
}
