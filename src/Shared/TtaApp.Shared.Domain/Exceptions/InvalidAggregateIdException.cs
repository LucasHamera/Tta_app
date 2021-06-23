using System;
using TtaApp.Shared.Domain.Base;

namespace TtaApp.Shared.Domain.Exceptions
{
    public class InvalidAggregateIdException : DomainException
    {
        public InvalidAggregateIdException(
            Guid id
        ) : base($"Invalid aggregate id.")
        {
            Id = id;
        }

        public override string Code 
            => "invalid_aggregate_id";

        public Guid Id
        {
            get;
        }
    }
}