using DocumentManagement.Models.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DocumentManagement.Models
{
    public class PdfDocument
    {
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".pdf" })]
        [Required(ErrorMessage = "Please provide PDF file."), DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
