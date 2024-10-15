using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SAT.Data.EF.Models;

[ModelMetadataType(typeof(StudentMetadata))]
public partial class Student{

	[NotMapped]
	public string FullName => $"{FirstName} {LastName}";

    [NotMapped]
    public IFormFile? Image { get; set; }
}

[ModelMetadataType(typeof(ScheduledClassMetadata))]
public partial class ScheduledClass {

	[NotMapped]
	public string FullCourseName => $"{Course.CourseName} | {Location}";

}

