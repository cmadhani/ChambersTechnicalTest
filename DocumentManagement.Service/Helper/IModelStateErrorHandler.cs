using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DocumentManagement.Service
{
    public interface IModelStateErrorHandler
    {
        string GetValues(ModelStateDictionary modelState);
    }
}
