using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurats.Domain.Exceptions;
using Xunit;

namespace Restaurats.API.Middlewares.Tests;

public class ErrorHandlingMiddlewareTests
{
    [Fact()]
    public async Task InvokeAsync_WhenNoException_ShouldNotChangeStatusCode()
    {
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);

        var context = new DefaultHttpContext();
        var next = new RequestDelegate((innerContext) => Task.CompletedTask);
        var statusCode = context.Response.StatusCode;
        var expectedStatusCode = 200;
        await middleware.InvokeAsync(context, next);

        expectedStatusCode.Should().Be((int)statusCode);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNotFoundException_ShouldReturnStatusCode404()
    {
        var context = new DefaultHttpContext();
        var exception = new NotFoundException(It.IsAny<string>(), It.IsAny<string>());
        RequestDelegate next = (ctx) => throw exception;
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var _middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        // Act
        await _middleware.InvokeAsync(context, next);

        // Assert
        ((int)HttpStatusCode.NotFound).Should().Be((int)(context.Response.StatusCode));

    }

    [Fact()]
    public async Task InvokeAsync_WhenForbiddenException_ShouldReturnStatusCode403()
    {
        var context = new DefaultHttpContext();
        var exception = new ForbiddenException();
        RequestDelegate next = (ctx) => throw exception;
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var _middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        // Act
        await _middleware.InvokeAsync(context, next);

        // Assert
        ((int)HttpStatusCode.Forbidden).Should().Be((int)(context.Response.StatusCode));
    }

    [Fact()]
    public async Task InvokeAsync_WhenGeneralException_ShouldReturnStatusCode500()
    {
        var context = new DefaultHttpContext();
        var exception = new Exception();
        RequestDelegate next = (ctx) => throw exception;
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var _middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        // Act
        await _middleware.InvokeAsync(context, next);

        // Assert
        ((int)HttpStatusCode.InternalServerError).Should().Be((int)(context.Response.StatusCode));
    }
}