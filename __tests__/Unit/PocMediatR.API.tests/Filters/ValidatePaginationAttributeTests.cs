using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using PocMediatR.API.Filters;
using PocMediatR.Common.Exceptions;
using PocMediatR.Common.Interfaces;
using System.Runtime.InteropServices;

namespace PocMediatR.API.Tests.Filters
{
    public class ValidatePaginationAttributeTests
    {
        private readonly ValidaPaginationAttribute filter = new();
        private ActionExecutingContext context;

        private class TestPageable : IPageable
        {
            public int _page { get; set; }
            public int _size { get; set; }
        }

        [Fact]
        public void Should_not_trow_when_pageable_is_valid()
        {
            var pageable = new TestPageable { _size = 10, _page = 1 };

            CreateContext(pageable);

            ExecuteFilter()
                .ShouldNotThrow();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        public void Should_throw(int size, int page)
        {
            var pageable = new TestPageable { _size = size, _page = page };

            CreateContext(pageable);

            var exception = Should.Throw<AggregateException>(() => filter.OnActionExecuting(context!));

            exception
                .InnerExceptions
                .OfType<InvalidParameterValueException>();
        }

        [Fact]
        public void Should_throw_when_page_is_invalid()
        {

        }
        private void CreateContext(object? pageable)
        {
            var actionContext = GetActionContext();

            var actionArguments = new Dictionary<string, object>
            {
                { "pageable", pageable!}
            };

            context = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                actionArguments,
                controller: new { });
        }

        private void CreateContextWithoutPageable()
        {
            var actionContext = GetActionContext();

            context = new ActionExecutingContext(
               actionContext,
               new List<IFilterMetadata>(),
               new Dictionary<string, object?>(),
               controller: new { });
        }

        private ActionContext GetActionContext()
        {
            var httpContext = Substitute.For<HttpContext>();
            var routeData = Substitute.For<RouteData>();
            var actionDescriptor = Substitute.For<ActionDescriptor>();
            var modelState = new ModelStateDictionary();

            return new ActionContext(httpContext, routeData, actionDescriptor, modelState);
        }

        private Action ExecuteFilter()
        {
            return () => filter.OnActionExecuting(context!);
        }
    }
}
