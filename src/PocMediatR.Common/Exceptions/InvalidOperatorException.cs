using PocMediatR.Common.Interfaces;
using PocMediatR.Common.Translations.Resources;
using PocMediatR.Common.Utils;
using Microsoft.AspNetCore.Http;

namespace PocMediatR.Common.Exceptions
{
    public class InvalidOperatorException(string @operator) :
        Exception(Translation.GetTranslatedMessage(nameof(Messages.IvalidParameterValueException_detail), @operator)),
        IHttpStatusCodeReturnable
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ErrorDescription => nameof(Messages.IvalidParameterValueException_error);
    }
}
