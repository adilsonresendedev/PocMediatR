using Microsoft.AspNetCore.Http;
using PocMediatR.Common.Interfaces;
using PocMediatR.Common.Translations.Resources;
using PocMediatR.Common.Utils;

namespace PocMediatR.Common.Exceptions
{
    public class DomainException(string parameter)
        : Exception(Translation.GetTranslatedMessage(Messages.DomainException_detail)),
            IHttpStatusCodeReturnable
    {
        public int StatusCode => StatusCodes.Status422UnprocessableEntity;
        public string ErrorDescription =>  Translation.GetTranslatedMessage(parameter);
    }
}
