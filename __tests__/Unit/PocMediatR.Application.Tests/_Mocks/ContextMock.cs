using MockQueryable.NSubstitute;
using NSubstitute.Extensions;
using PocMediatR.Domain.Context;
using PocMediatR.Domain.Entities;
using FizzWare.NBuilder;

namespace PocMediatR.Application.Tests._Mocks
{
    public static class ContextMock
    {
        public static IPocMediatRContext Mock<TEntity>(this IPocMediatRContext context, params TEntity[] entities) where TEntity : BaseEntity
        {
            var mockedEntity = entities.AsQueryable().BuildMockDbSet();
            context.ReturnsForAll(mockedEntity);
            return context;
        }

        public static IPocMediatRContext Mock<TEntity>(this IPocMediatRContext context, int quantity = 10) where TEntity : BaseEntity
        {
            if (quantity <= 0)
                return context.EmptyMock<TEntity>();

            var data = Builder<TEntity>
                .CreateListOfSize(quantity)
                .Build()
                .ToArray();

            return context.Mock(data);
        }

        public static IPocMediatRContext EmptyMock<TEntity>(this IPocMediatRContext context) where TEntity : BaseEntity
        {
            return context.Mock(Array.Empty<TEntity>());
        }
    }
}
