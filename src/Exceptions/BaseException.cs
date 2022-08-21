using System;
using System.Net;

namespace LS.Avatars.Api.Exceptions;

public abstract class BaseException : ApplicationException
{
    public virtual HttpStatusCode ErrorCode { get; } = HttpStatusCode.BadRequest;

    public abstract ErrorTypes ErrorType { get; }
}