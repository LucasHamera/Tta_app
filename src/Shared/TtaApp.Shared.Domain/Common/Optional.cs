using System;
using System.Collections.Generic;

namespace TtaApp.Shared.Domain.Common
{
    public readonly struct Optional<T> : IEquatable<Optional<T>>
    {
        private readonly T _value;

        private Optional(T value, bool hasValue)
        {
            _value = value;
            HasValue = hasValue;
        }

        public bool HasValue
        {
            get;
        }

        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException("No value present.");
                return _value;
            }
        }

        public static Optional<T> Empty 
            => new(default, false);

        public static Optional<T> Of(
            T value
        )
        {
            return new(value, value is not null);
        }

        public bool Equals(
            Optional<T> other
        )
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value) 
                   && HasValue == other.HasValue;
        }

        public override bool Equals(
            object obj
        )
        {
            return obj is Optional<T> other 
                   && Equals(other);
        }

        public static implicit operator Optional<T>(T value) 
            => Of(value);
    }
}
