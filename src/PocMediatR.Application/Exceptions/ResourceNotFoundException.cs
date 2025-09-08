using Microsoft.AspNetCore.Http;
using PocMediatR.Common.Interfaces;
using PocMediatR.Common.Translations.Resources;
using PocMediatR.Common.Utils;
using PocMediatR.Domain.Entities;
namespace PocMediatR.Application.Exceptions
{
    public class ResourceNotFoundException<T>() :
        Exception(Translation.GetTranslatedMessage(nameof(Messages.ResourceNotFoundException_detail),
        Translation.GetTranslatedMessage(nameof(T)))),
        IHttpStatusCodeReturnable
        where T : BaseEntity
    {
        public int StatusCode => StatusCodes.Status404NotFound;
        public string ErrorDescription => nameof(Messages.ResourceNotFoundException_error);
    }
}