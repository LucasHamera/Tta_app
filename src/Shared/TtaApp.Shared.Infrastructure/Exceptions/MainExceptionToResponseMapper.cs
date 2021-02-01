using System;
using System.Net;
using Convey.WebApi.Exceptions;
using TtaApp.Shared.Application.Base;
using TtaApp.Shared.Domain.Base;

namespace TtaApp.Shared.Infrastructure.Exceptions
{
    internal class MainExceptionToResponseMapper
        : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(
            Exception exception
        )
        {
            return new(
                MapException(exception),
                HttpStatusCode.BadRequest
            );
        }

        private object MapException(
            Exception exception
        )
        {
            return exception switch
            {
                DomainException ex => new {code = ex.Code, reason = ex.Message},
                AppException ex => new {code = ex.Code, reason = ex.Message},
                _ => new ExceptionResponse(exception, HttpStatusCode.BadRequest)
            };
        }
    }
}
