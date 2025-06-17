using PocMediatR.Common.Interfaces;
using PocMediatR.Common.Translations.Resources;
using PocMediatR.Common.Utils;
using Microsoft.AspNetCore.Http;

namespace PocMediatR.Common.Exceptions
{
    class ResourceNotFoundException<TEntity> : Exception, IHttpStatusCodeReturnable
    {
        public ResourceNotFoundException() : base(Translation.GetTranslatedMessage(nameof(Messages.ResourceNotFoundException_detail), typeof(TEntity).Name))
        {

        }
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ErrorDescription => nameof(Messages.ResourceNotFoundException_error);
    }
}
