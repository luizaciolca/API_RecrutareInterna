using API_RecrutareInterna.Helpers;
using API_RecrutareInterna.Exceptions;

namespace API_RecrutareInterna.Exceptions
{
    public class ValidationFunctions
    {
        public static void ThrowExceptionWhenDateIsNotValid(DateTime? startTime, DateTime? endTime)
        {
            if (startTime.HasValue && endTime.HasValue && startTime > endTime)
            {
                throw new ModelValidationException(ErrorMessageEnum.Job.StartEndDateError);
            }
        }
    }
} 