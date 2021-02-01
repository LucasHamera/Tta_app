using System;

namespace TtaApp.Shared.Domain.Base
{
    public abstract class DomainException : Exception
    {
        protected DomainException(
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
