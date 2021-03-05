using System;
using HotChocolate;

namespace RibbleChatServer.GraphQL
{
    public class ErrorException : Exception
    {
        public ErrorException(string? message) : base(message)
        {
        }
    }

    /// Bad Request
    public class RequestException : Exception
    {
        public RequestException(string? message) : base(message)
        {
        }
    }

    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error) =>
            error.WithMessage(error?.Exception?.Message ?? "Unknown Error Occurred");
    }
}