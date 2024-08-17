using System.ComponentModel.DataAnnotations;

namespace FileUploadAttacks.Models
{
    public class FileViewModel
    {
        [Required]
        [AllowedExtensions(new string[] { ".txt", ".pdf" })] // allow only .txt and .pdf files etc
        public IFormFile File { get; set; }
    }
}