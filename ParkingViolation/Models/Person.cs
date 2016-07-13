using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ParkingViolation.Models
{
	public abstract class Person
	{
		public int PersonID { get; set; }

		[Required(ErrorMessage = "First name is required.")]
		[Display(Name = "First Name")]
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
		public string FirstMidName { get; set; }

		[Required(ErrorMessage = "Last name is required.")]
		[Display(Name = "Last Name")]
		[StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Phone number is required.")]
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Phone Number")]
		[Index("PhoneNumberIndex", IsUnique = true)]
		[StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits.")]
		[Remote("ValidatePhoneNumber", "Volunteer", ErrorMessage = "Phone number is already associated with another urser.", AdditionalFields = "initialPhoneNumber")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Email address is required.")]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Email Address")]
		[StringLength(50, ErrorMessage = "Email address cannot be longer than 50 characters.")]
		public string EmailAddress { get; set; }

		public string FullName
		{
			get
			{
				return string.Format("{0} {1}", FirstMidName, LastName);
			}
		}
	}
}