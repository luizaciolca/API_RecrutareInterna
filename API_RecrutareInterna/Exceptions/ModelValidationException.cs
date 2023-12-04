using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace API_RecrutareInterna.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException(string message) : base(message) { }
    }
}
