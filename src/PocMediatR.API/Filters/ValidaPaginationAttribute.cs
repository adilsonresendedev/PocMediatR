using Microsoft.AspNetCore.Mvc.Filters;
using PocMediatR.Common.Exceptions;

namespace PocMediatR.API.Filters
{
    public class ValidaPaginationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;
            var invalidParameterValueExceptions = new List<InvalidParameterValueException>();

            foreach (var modelStateEntry in context.ModelState.Where(ms => ms.Value!.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
            {
                var exception = new InvalidParameterValueException(modelStateEntry.Key);
                invalidParameterValueExceptions.Add(exception);
            }

            if (invalidParameterValueExceptions.Count != 0)
                throw new AggregateException([.. invalidParameterValueExceptions]);
        }
    }
}
