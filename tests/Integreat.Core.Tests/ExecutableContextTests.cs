using System;
using Xunit;

namespace Integreat.Core.Tests
{
    public class ExecutableContextTests
    {
        [Fact]
        public void Parameters_ReturnEmpty_WhenNullParametersSupplied()
        {
            var context = new ExecutableContext("integrationDirectory", "executablesDirectory", parameters: null, timeout: 30, logAction: null);

            Assert.NotNull(context.Parameters);
        }

        [Fact]
        public void Ctor_ThrowsException_WhenDirectoriesAreNull()
        {
            var integrationDirException = Record.Exception(() => new ExecutableContext(null, "executablesDirectory", parameters: null, timeout: 30, logAction: null));
            var execDirException = Record.Exception(() => new ExecutableContext("integrationDirectory", null, parameters: null, timeout: 30, logAction: null));

            Assert.NotNull(integrationDirException);
            Assert.IsType<ArgumentNullException>(integrationDirException);
            Assert.NotNull(execDirException);
            Assert.IsType<ArgumentNullException>(execDirException);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-34)]
        public void Timeout_ReturnNumberGreaterThanZero_WhenZeroOrLessIsSupplied(int timeout)
        {
            var context = new ExecutableContext("integrationDirectory", "executablesDirectory", parameters: null, timeout: timeout, logAction: null);

            Assert.True(context.Timeout > 0, $"Timeout ({context.Timeout}) not greater than zero.");
        }

        [Theory]
        [InlineData(30)]
        [InlineData(564)]
        public void Timeout_ReturnsSuppliedTimeout_WhenSuppliedTimeoutIsGreaterThanZero(int timeout)
        {
            var context = new ExecutableContext("integrationDirectory", "executablesDirectory", parameters: null, timeout: timeout, logAction: null);

            Assert.Equal(timeout, context.Timeout);
        }

        [Fact]
        public void Log_ThrowsException_WhenSuppliedLogActionIsNull()
        {
            var context = new ExecutableContext("integrationDirectory", "executablesDirectory", parameters: null, timeout: 30, logAction: null);
            var exception = Record.Exception(() => context.Log("my_message"));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Log_CallsLogAction_WhenSuppliedLogActionIsValid()
        {
            string testingChangeValue = "not_my_message";
            Action<string> logAction = (msg) => 
            {
                testingChangeValue = msg;
                return;
            };

            var context = new ExecutableContext("integrationDirectory", "executablesDirectory", parameters: null, timeout: 30, logAction: logAction);
            context.Log("my_message");

            Assert.Equal("my_message", testingChangeValue);
        }
    }
}
