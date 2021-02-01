using System;
using Convey.WebApi.Exceptions;

namespace TtaApp.Shared.Application.Exceptions
{
    public interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(
            Exception exception
        );
    }
}
