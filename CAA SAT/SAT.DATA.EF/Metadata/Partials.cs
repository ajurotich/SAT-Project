using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;


namespace SAT.Data.EF.Models;

[ModelMetadataType(typeof(StudentMetadata))]
public partial class Student{

	[NotMapped]
	public string FullName => $"{FirstName} {LastName}";

}

[ModelMetadataType(typeof(ScheduledClassMetadata))]
public partial class ScheduledClass {

	[NotMapped]
	public string FullCourseName => $"{Course.CourseName} | {Location}";

}
