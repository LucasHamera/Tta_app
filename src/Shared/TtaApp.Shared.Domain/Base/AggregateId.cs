using System;
using TtaApp.Shared.Domain.Exceptions;

namespace TtaApp.Shared.Domain.Base
{
    public class AggregateId : IEquatable<AggregateId>
    {
        public AggregateId() 
            : this(Guid.NewGuid())
        {
        }
        public AggregateId(
            Guid value
        )
        {
            if (value == Guid.Empty)
                throw new InvalidAggregateIdException(value);

            Value = value;
        }

        public Guid Value
        {
            get;
        }

        public bool Equals(
            AggregateId other
        )
        {
            if (other is null)
                return false;
            return ReferenceEquals(this, other) || Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (this == obj)
                return true;
            return obj.GetType() == GetType() && Equals((AggregateId) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator Guid(AggregateId id)
            => id.Value;

        public static implicit operator AggregateId(Guid id)
            => new(id);
    }
}