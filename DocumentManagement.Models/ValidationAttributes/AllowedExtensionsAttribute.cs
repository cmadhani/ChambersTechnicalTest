using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace DocumentManagement.Models.ValidationAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentNullException("provide valid extensions", "extensions");
            }
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                throw new ArgumentException("Invalid argument type, expected type : IFormFile", "value");
            }
            else
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"This extension ({extension}) is not permitted");
                }
            }

            return ValidationResult.Success;
        }
    }
}
