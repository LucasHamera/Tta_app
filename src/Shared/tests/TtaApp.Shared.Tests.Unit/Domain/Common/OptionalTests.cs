#nullable enable
using System;
using FluentAssertions;
using TtaApp.Shared.Domain.Common;
using Xunit;

namespace TtaApp.Shared.Tests.Unit.Domain.Common
{
    public class OptionalTests
    {
        [Fact]
        public void CreatedWithValueTypeShouldBeCreated()
        {
            // Arrange
            var value = 0;

            // Act
            var optional = Optional<int>.Of(value);

            // Assert
            optional.HasValue.Should().BeTrue();
            optional.Value.Should().Be(value);
        }

        [Fact]
        public void CreatedWithReferenceTypeShouldBeCreated()
        {
            // Arrange
            object value = new object();

            // Act
            var optional = Optional<object>.Of(value);

            // Assert
            optional.HasValue.Should().BeTrue();
            optional.Value.Should().Be(value);
        }

        [Fact]
        public void CreatedWithNullShouldBeCreatedWithFalseHasValue()
        {
            // Arrange
            object? value = null;

            // Act
            var optional = Optional<object>.Of(value);

            // Assert
            optional.HasValue.Should().BeFalse();
        }

        [Fact]
        public void GetNoValueShouldThrowInvalidOperationException()
        {
            // Arrange
            object? value = null;
            var optional = Optional<object>.Of(value);

            // Act
            var exception =  Record.Exception(
                () => optional.Value
            );

            // Assert
            exception.Should().NotBeNull();
            var invalidOperation = exception as InvalidOperationException;
            invalidOperation.Should().NotBeNull();
            invalidOperation!.Message.Should().Be("No value present.");
        }

        [Fact]
        public void CreateEmptyShouldCreateWithNoValue()
        {
            // Arrange
            var optional = Optional<object>.Empty;

            // Act
            var hasValue = optional.HasValue;

            // Assert
            hasValue.Should().BeFalse();
        }

        [Fact]
        public void CreateImplicitNullShouldCreateWithNoValue()
        {
            // Arrange
            Optional<object> optional = null!;

            // Act
            var hasValue = optional.HasValue;

            // Assert
            hasValue.Should().BeFalse();
        }

        [Fact]
        public void CreateImplicitValueShouldCreateWithHasValue()
        {
            // Arrange
            Optional<object> optional = new object();

            // Act
            var hasValue = optional.HasValue;

            // Assert
            hasValue.Should().BeTrue();
        }
    }
}
