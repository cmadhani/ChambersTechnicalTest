using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;



namespace DocumentManagement.Models.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
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
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"Maximum allowed file size is { _maxFileSize } bytes. The current size is {file.Length}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
