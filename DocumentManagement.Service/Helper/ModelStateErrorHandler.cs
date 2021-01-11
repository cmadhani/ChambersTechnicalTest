using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DocumentManagement.Service
{
    public class ModelStateErrorHandler : IModelStateErrorHandler
    {
        public string GetValues(ModelStateDictionary modelState)
        {
            if (modelState == null)
                throw new ArgumentNullException("modelState", "ModelStateDictionary Expected");

            if (!modelState.Any())
                return string.Empty;

            var sb = new StringBuilder();

            sb.AppendLine("Model Keys In Error:");

            foreach (var key in modelState.Keys)
            {
                sb.AppendLine(key);
            }

            sb.AppendLine(string.Empty);
            sb.AppendLine("Model Error Messages:");
            foreach (var error in modelState.Values.SelectMany(v => v.Errors))
            {
                sb.AppendLine(error.ErrorMessage);
            }

            return sb.ToString();
        }
    }
}
