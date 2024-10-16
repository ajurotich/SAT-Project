using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAT.Data.EF.Models;
using SAT.UI.MVC.Utilities;
using System.Drawing;

namespace SAT.UI.MVC.Controllers {
	public class StudentsController : Controller {
		private readonly SatContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public StudentsController(SatContext context, IWebHostEnvironment iwhb) {
			_context = context;
			_webHostEnvironment = iwhb;

		}

		// GET: Students
		public async Task<IActionResult> Index() {
			var satContext = _context.Students.Include(s => s.Ss);
			return View(await satContext.ToListAsync());
		}

		// GET: Students/Details/5
		public async Task<IActionResult> Details(int? id) {
			if(id == null) {
				return NotFound();
			}

			var student = await _context.Students
				.Include(s => s.Ss)
				.FirstOrDefaultAsync(m => m.StudentId == id);
			if(student == null) {
				return NotFound();
			}

			return View(student);
		}

		// GET: Students/Create
		public IActionResult Create() {
			ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname");
			return View();
		}

		// POST: Students/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,FullName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,Image,Ssid")] Student student) {
			if(ModelState.IsValid) {
				#region File Upload - Create
				if(student.Image != null) {
					//Check the file type
					// retrieve the extension of the uploaded file
					string ext = Path.GetExtension(student.Image.FileName);

					//Make a list of valid extensions to check against
					string[] validExts = { ".jpg", ".jpeg", ".gif", ".png" };

					//check that the file has an extension in our list AND
					//check if the file size will work with our .NET app
					if(validExts.Contains(ext.ToLower()) && student.Image.Length < 4_194_303) {
						//Generate a unique filename
						student.PhotoUrl = Guid.NewGuid() + ext;

						//Save the file to the web server (here, saving to wwwroot/images)
						//To access wwwroot, add a property to the controller for the
						//_webHostEnvironment (see the top of this class for our example)
						//Retrieve the path to wwwroot
						string webRootPath = _webHostEnvironment.WebRootPath;
						//variable for the full image path must include the images folder:
						string fullImagePath = webRootPath + "/img/Clown/";

						//Create a MemoryStream to read the image into the server memory
						using(var memoryStream = new MemoryStream()) {
							//transfer the file from the request into our server's memory
							await student.Image.CopyToAsync(memoryStream);
							//add a using statement for the Image class using System.Drawing
							using(var img = Image.FromStream(memoryStream)) {
								//now, send the image to the ImageUtility for resizing and thumbnail creation
								//items needed for the ImageUtility.ResizeImage()
								//1) (int) maximum image size
								//2) (int) maximum thumbnail image size
								//3) (string) full path where the file will be saved
								//4) (Image) an image
								//5) (string) filename
								int maxImageSize = 500;
								int maxThumbSize = 100;

								ImageUtility.ResizeImage(fullImagePath, student.PhotoUrl, img, maxImageSize, maxThumbSize);

								//if we weren't handling resizing of images, we could plainly save a file like this:
								//myFile.Save("path/to/save", "filename")

							}
						}
					}
				}
				else {
					//If no image was uploaded, assign a default filename
					//We also added noimage.png to our images folder
					student.PhotoUrl = "SadClown.jpg";
				}
				//IMAGE UPLOAD - STEP 12
				//Add noimage.png to our images folder

				#endregion

				_context.Add(student);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
			return View(student);
		}

		// GET: Students/Edit/5
		public async Task<IActionResult> Edit(int? id) {
			if(id == null) {
				return NotFound();
			}

			var student = await _context.Students.FindAsync(id);
			if(student == null) {
				return NotFound();
			}
			ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
			return View(student);
		}

		// POST: Students/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,Image,Ssid")] Student student) {
			if(id != student.StudentId) {
				return NotFound();
			}

			if(ModelState.IsValid) {

				#region file upload - edit

				//save old image name to delete if replacement comes
				string oldImgName = student.PhotoUrl;

				if(student.Image != null) {
					//allowed extensions list
					string[] validExts = { ".jpg", ".jpeg", ".png", ".gif" };

					//get the extension
					string ext = Path.GetExtension(student.Image.FileName);

					//check if file has valid extensions
					// and if size will work with .net app
					if(validExts.Contains(ext.ToLower()) && student.Image.Length < 4_194_303) {

						//make unique file name
						student.PhotoUrl = Guid.NewGuid() + ext;

						//Save the file to the web server (here, saving to wwwroot/images)
						//To access wwwroot, add a property to the controller for the
						//_webHostEnvironment (see the top of this class for our example)
						//Retrieve the path to wwwroot
						string webRootPath = _webHostEnvironment.WebRootPath;

						//full image path must include images folder
						string fullImagePath = webRootPath + "/img/Clown/";

						//delete old image
						if(oldImgName != fullImagePath + "placeholder.png")
							ImageUtility.Delete(fullImagePath, oldImgName);

						//save new image
						using(var memoryStream = new MemoryStream()) {
							await student.Image.CopyToAsync(memoryStream);

							using(var img = Image.FromStream(memoryStream)) {
								int maxImageSize = 500;
								int maxThumbSize = 100;

								ImageUtility.ResizeImage(fullImagePath, student.PhotoUrl, img, maxImageSize, maxThumbSize);
							}

						}

					}

				}

				#endregion

				try {
					_context.Update(student);
					await _context.SaveChangesAsync();
				}
				catch(DbUpdateConcurrencyException) {
					if(!StudentExists(student.StudentId)) 
						return NotFound();
					else throw;
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
			return View(student);
		}

		// GET: Students/Delete/5
		public async Task<IActionResult> Delete(int? id) {
			if(id == null) {
				return NotFound();
			}

			var student = await _context.Students
				.Include(s => s.Ss)
				.FirstOrDefaultAsync(m => m.StudentId == id);
			if(student == null) {
				return NotFound();
			}

			return View(student);
		}

		// POST: Students/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id) {
			var student = await _context.Students.FindAsync(id);
			if(student != null) {
				_context.Students.Remove(student);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool StudentExists(int id) {
			return _context.Students.Any(e => e.StudentId == id);
		}
	}
}
