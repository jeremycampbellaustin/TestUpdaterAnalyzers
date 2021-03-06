using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace TestUpdaterAnalyzers.Test
{
    [TestClass]
    public class RepeatFixTests : CodeFixVerifier
    {
        [TestMethod]
        public void RepeatRemovedAfterReturn()
        {
            var test = @"
using Rhino.Mocks;
using Xunit;
using SampleBusinessLogic;

namespace RhinoXUnitFixture
{
    public class RhinoMocksTests
    {
        [Fact]
        public void IgnoreArguments()
        {
            var mock = MockRepository.GenerateMock<IValidator>();
            mock.Expect(x => x.Validate(Arg<Request>.Is.Anything)).Repeat.Twice().Return(true);
            var sut = new BusinessLogic(mock);
            var result = sut.CalculateId(new Request() { Age = 1, Height = 1, Name = ""test"" });
            result = sut.CalculateId(new Request() { Age = 1, Height = 1, Name = ""test"" });
            Assert.Equal(5, result);
        }
    }
}
";


            var expectedSource = @"
using NSubstitute;
using Rhino.Mocks;
using Xunit;
using SampleBusinessLogic;

namespace RhinoXUnitFixture
{
    public class RhinoMocksTests
    {
        [Fact]
        public void IgnoreArguments()
        {
            var mock = Substitute.For<IValidator>();
            mock.Validate(NSubstitute.Arg.Any<Request>()).Returns(true);
            var sut = new BusinessLogic(mock);
            var result = sut.CalculateId(new Request() { Age = 1, Height = 1, Name = ""test"" });
            result = sut.CalculateId(new Request() { Age = 1, Height = 1, Name = ""test"" });
            Assert.Equal(5, result);
        }
    }
}
";
            VerifyCSharpFix(test, expectedSource, allowNewCompilerDiagnostics: false);
        }

        [TestMethod]
        public void RepeatRemovedBeforeReturn()
        {
            var test = @"
using Rhino.Mocks;
using Xunit;
using SampleBusinessLogic;

namespace RhinoXUnitFixture
{
    public class RhinoMocksTests
    {
        [Fact]
        public void IgnoreArguments()
        {
            var mock = MockRepository.GenerateMock<IValidator>();
            mock.Expect(x => x.Validate(Arg<Request>.Is.Anything)).Return(true).Repeat.Twice();
            var sut = new BusinessLogic(mock);
            var result = sut.CalculateId(new Request() { Age = 1, Height = 1, Name = ""test"" });
            result = sut.CalculateId(new Request() { Age = 1, Height = 1, Name = ""test"" });
            Assert.Equal(5, result);
        }
    }
}
";


            var expectedSource = @"
using NSubstitute;
using Rhino.Mocks;
using Xunit;
using SampleBusinessLogic;

namespace RhinoXUnitFixture
{
    public class RhinoMocksTests
    {
        [Fact]
        public void IgnoreArguments()
        {
            var mock = Substitute.For<IValidator>();
            mock.Validate(NSubstitute.Arg.Any<Request>()).Returns(true);
            var sut = new BusinessLogic(mock);
            var result = sut.CalculateId(new Request() { Age = 1, Height = 1, Name = ""test"" });
            result = sut.CalculateId(new Request() { Age = 1, Height = 1, Name = ""test"" });
            Assert.Equal(5, result);
        }
    }
}
";
            VerifyCSharpFix(test, expectedSource, allowNewCompilerDiagnostics: false);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new TestUpdaterAnalyzersCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new TestUpdaterAnalyzersAnalyzer();
        }
    }
}
