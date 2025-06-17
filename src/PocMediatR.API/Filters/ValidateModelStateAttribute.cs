using Microsoft.AspNetCore.Mvc.Filters;

namespace PocMediatR.API.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ModelState.IsValid)
                return;

            //var invalidParameterValueExceptions = new List<>
        }
    }
}
