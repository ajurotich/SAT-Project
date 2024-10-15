using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SAT.Data.EF.Models {
	public class CourseMetadata {

		public int CourseId { get; set; }

		[StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
		[DisplayName("Course Name")]
		public string CourseName { get; set; }

		[StringLength(5000, ErrorMessage = "Must not exceed 5000 characters")]
		[Display(Name = "Description")]
		public string CourseDescription { get; set; }

		public int CreditHours { get; set; }

		[StringLength(250, ErrorMessage = "Must not exceed 250 characters")]
		[Display(Name = "Curriculum")]
		public string? Curriculum { get; set; }

		[StringLength(500, ErrorMessage = "Must not exceed 500 characters")]
		[Display(Name = "Notes")]
		public string? Notes { get; set; }

		public bool IsActive { get; set; }
	}

	public class EnrollmentMetadata {
		public int EnrollmentId { get; set; }
		public int StudentId { get; set; }
		public int ScheduledClassId { get; set; }
		public DateOnly? EnrollmentDate { get; set; }
	}

	public class ScheduledClassMetadata {

		public int ScheduledClassId { get; set; }

		public int CourseId { get; set; }

		public DateOnly? StartDate { get; set; }

		public DateOnly? EndDate { get; set; }

		[DisplayName("Course")]
		public string FullCourseName { get; set; }

		[StringLength(40, ErrorMessage = "Must not exceed 40 characters")]
		[DisplayName("Instructor")]
		public string InstructorName { get; set; }

		[StringLength(20, ErrorMessage = "Must not exceed 20 characters")]
		[Display(Name = "Location")]
		public string Location { get; set; }

		[Display(Name="Status")]
		public int SCSID { get; set; }
	}

	public class ScheduledClassStatusMetadata {
		public int SCSID { get; set; }

		[StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
		[DisplayName("Status")]
		public string Scname { get; set; }
	}

	public class StudentMetadata {

		public int StudentId { get; set; }

		[StringLength(20, ErrorMessage = "Must not exceed 20 characters")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[DisplayName("Name")]
		public string FullName { get; set; }

		[StringLength(200, ErrorMessage = "Must not exceed 200 characters")]
		[Display(Name = "Major")]
		public string? Major { get; set; }

		[StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
		[Display(Name = "Adress")]
		public string? Address { get; set; }

		[StringLength(25, ErrorMessage = "Must not exceed 25 characters")]
		[Display(Name = "City")]
		public string? City { get; set; }

		[StringLength(2, ErrorMessage = "Must not exceed 2 characters")]
		[Display(Name = "State")]
		public string? State { get; set; }

		[StringLength(10, ErrorMessage = "Must not exceed 10 characters")]
		[Display(Name = "Zip Code")]
		public string? ZipCode { get; set; }

		[StringLength(13, ErrorMessage = "Must not exceed 13 characters")]
		[Display(Name = "Phone Number")]
		public string? Phone { get; set; }

		[StringLength(60, ErrorMessage = "Must not exceed 60 characters")]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[StringLength(100, ErrorMessage = "Must not exceed 100 characters")]
		[Display(Name = "Photo Url")]
		public string? PhotoUrl { get; set; }

		public int? SSID { get; set; }
	}

	public class StudentStatusMetadata {
		public int SSID { get; set; }
		[StringLength(30, ErrorMessage = "Must not exceed 30 characters")]
		[Display(Name = "SSName")]
		public string SSName { get; set; }
		[StringLength(250, ErrorMessage = "Must not exceed 250 characters")]
		[Display(Name = "SSDescription")]
		public string? SSDescription { get; set; }
	}
}
