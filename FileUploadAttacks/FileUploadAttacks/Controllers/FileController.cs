using FileUploadAttacks.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadAttacks.Controllers
{
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Vulnerable code

        [HttpPost]
        public IActionResult VulnerableUpload(FileViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    var fileName = model.File.FileName;
                    var filePath = Path.Combine(_uploadPath, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.File.CopyTo(fileStream);
                    }
                    TempData["Message"] = $"<p>'{fileName}'</p> uploaded successfully";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", model);
        }


        // Safe code

        //[Authorize]
        [RequestSizeLimit(10*1024)] // limit the file size to 10KB
        [HttpPost]
        public IActionResult Index(FileViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    // check the memi type of the file
                    var mimeType = model.File.ContentType;
                    if (!mimeType.Equals("text/plain") && !mimeType.Equals("application/pdf"))
                    {
                        TempData["ErrorMessage"] = $"This file type is not allowed!";
                        return View(model);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filePath = Path.Combine(_uploadPath, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.File.CopyTo(fileStream);
                    }
                    TempData["SuccessMessage"] = $"<b>'{model.File.FileName}'</b> uploaded successfully";
                    return RedirectToAction("Index");
                }
            }
            TempData["ErrorMessage"] = $"{ModelState.Values.First().Errors.First().ErrorMessage}";
            return View(model);
        }
    }
}