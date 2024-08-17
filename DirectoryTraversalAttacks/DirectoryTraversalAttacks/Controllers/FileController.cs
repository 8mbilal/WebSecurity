using DirectoryTraversalAttacks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DirectoryTraversalAttacks.Controllers
{
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VulnerableReadFile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SafeReadFile1()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SafeReadFile2()
        {
            return View();
        }

        // Vulnerable method
        [HttpPost]
        public IActionResult VulnerableReadFile(FileViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filePath = Path.Combine(_basePath, model.FilePath);
                string content = null;
                // Vulnerable code
                if (System.IO.File.Exists(filePath))
                {
                    content = System.IO.File.ReadAllText(filePath);
                    TempData["SuccessMessage"] = $"File <b>'{model.FilePath}'</b> read successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"The requested file <b>'{model.FilePath}'</b> was not found.";
                }
                return View("FileContent", model: content);
            }
            return View(model);
        }

        // Preventing Directory Traversal Attacks

        // Safe method 1
        [HttpPost]
        public IActionResult SafeReadFile1(FileViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filePath = Path.GetFullPath(Path.Combine(_basePath, model.FilePath));
                string content = null;
                // Safe code
                if (filePath.StartsWith(_basePath) && System.IO.File.Exists(filePath))
                {
                    content = System.IO.File.ReadAllText(filePath);
                    TempData["SuccessMessage"] = $"File <b>'{model.FilePath}'</b> read successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"The requested file <b>'{model.FilePath}'</b> was not found.";
                }
                return View("FileContent", model: content);
            }
            return View(model);
        }

        // Safe method 2
        [HttpPost]
        public IActionResult SafeReadFile2(FileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // sanitize file path
                if (model.FilePath.Contains("../")) 
                {
                    // if file path is ../../Program.cs, it will be sanitized to Program.cs
                    model.FilePath = model.FilePath.Replace("../", "");
                }

                string filePath = Path.GetFullPath(Path.Combine(_basePath, model.FilePath));
                string content = null;
                
                // Safe code
                if (filePath.StartsWith(_basePath) && System.IO.File.Exists(filePath))
                {
                    content = System.IO.File.ReadAllText(filePath);
                    TempData["SuccessMessage"] = $"File <b>'{model.FilePath}'</b> read successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"The requested file <b>'{model.FilePath}'</b> was not found.";
                }
                return View("FileContent", model: content);
            }
            return View(model);
        }
    }
}