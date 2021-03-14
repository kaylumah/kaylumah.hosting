using System.Reflection;
using FluentAssertions;
using Kaylumah.Ssg.Utilities;
using Xunit;

namespace Test.Unit
{
    public class AssemblyUtilTests
    {
        [Fact]
        public void Test_AssemblyData()
        {
            var sut = new AssemblyUtil();
            var result = sut.RetrieveAssemblyInfo(Assembly.GetExecutingAssembly());
            result.Should().NotBeNull();
            result.Copyright.Should().NotBeNull();
            result.Version.Should().NotBeNull();
            result.Metadata.Count.Should().Be(4);
        }
    }
}