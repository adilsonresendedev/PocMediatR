using PocMediatR.Common.Interfaces;
using PocMediatR.Common.Translations.Resources;
using PocMediatR.Common.Utils;
using Microsoft.AspNetCore.Http;

namespace PocMediatR.Common.Exceptions
{
    public class InvalidParameterValueException(string parameter)
        : Exception(Translation.GetTranslatedMessage(nameof(Messages.IvalidParameterValueException_detail), Translation.GetTranslatedMessage(parameter))),
            IHttpStatusCodeReturnable
    {
        public int StatusCode => StatusCodes.Status400BadRequest;
        public string ErrorDescription => nameof(Messages.IvalidParameterValueException_error);
    }
}
