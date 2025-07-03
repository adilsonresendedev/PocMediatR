using Microsoft.AspNetCore.Mvc.Filters;
using PocMediatR.Common.Exceptions;
using PocMediatR.Common.Interfaces;

namespace PocMediatR.API.Filters
{
    public class ValidatePaginationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var pageableArgument = context.ActionArguments.FirstOrDefault(aa => aa.Value is IPageable);

            if (pageableArgument.Key == default || pageableArgument.Value is null)
                return;

            var pageable = (IPageable)pageableArgument.Value;

            var invalidParameterValueExceptions = new List<InvalidParameterValueException>();

            if (pageable._size < 1 || pageable._size > 100)
                invalidParameterValueExceptions.Add(new InvalidParameterValueException(nameof(IPageable._size)));

            if (pageable._page < 1)
                invalidParameterValueExceptions.Add(new InvalidParameterValueException(nameof(IPageable._page)));

            if (invalidParameterValueExceptions.Count > 0)
                throw new AggregateException([.. invalidParameterValueExceptions]);
        }
    }
}
