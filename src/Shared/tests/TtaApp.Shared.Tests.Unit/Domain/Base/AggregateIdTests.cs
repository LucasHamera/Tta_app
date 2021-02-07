using System;
using FluentAssertions;
using TtaApp.Shared.Domain.Base;
using TtaApp.Shared.Domain.Exceptions;
using Xunit;

namespace TtaApp.Shared.Tests.Unit.Domain.Base
{
    public class AggregateIdTests
    {
        [Fact]
        public void GenerateNewAggregateShouldBeGenerated()
        {
            // Act
            var aggregateId = new AggregateId();

            // Assert
            aggregateId.Value.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void GenerateFromValidGuidAggregateShouldBeGenerated()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var aggregateId = new AggregateId(id);

            // Assert
            aggregateId.Value.Should().Be(id);
        }

        [Fact]
        public void GenerateEmptyAggregateIdShouldThrowInvalidAggregateIdException()
        {
            // Arrange
            var id = Guid.Empty;

            // Act
            var exception = Record.Exception(
                () => new AggregateId(id)
            );

            // Assert
            exception.Should().NotBeNull();
            var invalidAggregateId = exception as InvalidAggregateIdException;
            invalidAggregateId.Should().NotBeNull();
            invalidAggregateId!.Code.Should().Be("invalid_aggregate_id");
            invalidAggregateId!.Message.Should().Be("Invalid aggregate id.");
            invalidAggregateId!.Id.Should().Be(id);
        }

        [Fact]
        public void EqualsSameReferenceShouldResultTrue()
        {
            // Arrange
            var aggregateId = new AggregateId();

            // Act
            var equals = aggregateId.Equals(aggregateId);

            // Assert
            equals.Should().BeTrue();
        }

        [Fact]
        public void EqualsDifferentReferenceShouldResultTrue()
        {
            // Arrange
            var first = new AggregateId();
            var second = new AggregateId(first.Value);

            // Act
            var equals = first.Equals(second);

            // Assert
            equals.Should().BeTrue();
        }

        [Fact]
        public void EqualsNullShouldResultFalse()
        {
            // Arrange
            var aggregateId = new AggregateId();

            // Act
            var equals = aggregateId.Equals(null);

            // Assert
            equals.Should().BeFalse();
        }
    }
}
