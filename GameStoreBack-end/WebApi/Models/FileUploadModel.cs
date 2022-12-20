using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class FileUploadModel
    {
        [Required]
        public IFormFile UploadedFile { get; set; }
    }
}
