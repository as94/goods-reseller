using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace GoodsReseller.Api.Models
{
    public class FileUpload : IValidatableObject
    {
        private readonly string[] _availableExtensions = { ".png", ".jpg", ".jpeg" };
        
        [Required]
        public string FileName { get; set; }
        
        [Required]
        public IFormFile FileContent { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FileContent.Length == 0)
            {
                yield return new ValidationResult("File size should be more than 0");
            }

            var extension = Path.GetExtension(FileContent.FileName)?.ToLower();
            if (!_availableExtensions.Contains(extension))
            {
                var availableExtensionsString = string.Join(",", _availableExtensions);
                yield return new ValidationResult(
                    $"Bad file extension. Available extensions: {availableExtensionsString}");
            }
        }
    }
}