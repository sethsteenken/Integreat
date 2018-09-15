using Moq;
using System.Collections.Generic;
using Xunit;

namespace Integreat.Tests.Models
{
    public class ExecutionPlanTests
    {
        [Fact]
        public void Executables_ReturnsSuppliedExecutables()
        {
            var executables = new List<ProcessExecutable>()
            {
                new ProcessExecutable(new Mock<IExecutable>().Object, new ExecutableConfiguration("type", 30, null))
            };

            var executionPlan = new ExecutionPlan(executables, "integrationDir", "executablesDir");

            Assert.Equal(executables, executionPlan.Executables);
        }

        [Theory]
        [InlineData("my_directory")]
        [InlineData("")]
        [InlineData(null)]
        public void DirectoryProperties_ReturnsSuppliedDirectoryValues(string directory)
        {
            var executables = new List<ProcessExecutable>()
            {
                new ProcessExecutable(new Mock<IExecutable>().Object, new ExecutableConfiguration("type", 30, null))
            };

            var executionPlan = new ExecutionPlan(executables, directory, directory);

            Assert.Equal(directory, executionPlan.IntegrationDirectory);
            Assert.Equal(directory, executionPlan.ExecutablesDirectory);
        }
    }
}
