using Microsoft.AspNetCore.Http;
using PocMediatR.Common.Interfaces;
using PocMediatR.Common.Translations.Resources;
using PocMediatR.Common.Utils;
using PocMediatR.Domain.Entities;

namespace PocMediatR.Application.Exceptions
{
    public class ResourceShouldNotBeNullException<T>() :
        Exception(Translation.GetTranslatedMessage(nameof(Messages.ResourceShouldNotBeNullException_detail),
        Translation.GetTranslatedMessage(nameof(T)))),
        IHttpStatusCodeReturnable 
        where T : BaseEntity
    {
        public int StatusCode => StatusCodes.Status400BadRequest;
        public string ErrorDescription => nameof(Messages.ResourceShouldNotBeNullException_error);
    }
}
