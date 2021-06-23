using System;

namespace TtaApp.Shared.Application.Base
{
    public abstract class AppException : Exception
    {
        protected AppException(
            string message
        ) : base(message)
        {
        }

        public abstract string Code
        {
            get;
        }
    }
}
