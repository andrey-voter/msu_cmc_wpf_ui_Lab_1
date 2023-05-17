using Lab_1ViewModel;
using FluentAssertions;

namespace Lab_1ViewModelTest
{
  
    public class ViewDataTest
    {
        [Fact]
        public void ConstructorViewDataRawTest()
        {
            ViewData viewData = new ViewData(null);
            viewData.errorReporter.Should().BeNull();
            viewData.a.Should().Be(0);
            viewData.b.Should().Be(10);
            viewData.NodeCnt.Should().Be(10);
            viewData.IsUniform.Should().Be(true);
        }

        [Fact]
        public void ConstructorViewDataSplineTest()
        {
            ViewData viewData = new ViewData(null);
            viewData.SplineNodeCnt.Should().Be(3);
        }

        [Fact]
        public void ConstructorViewDataCommandTest()
        {
            ViewData viewData = new ViewData(null);
            viewData.SaveCommand.Should().NotBeNull();
            viewData.FromControlsCommand.Should().NotBeNull();
            viewData.FromFileCommand.Should().NotBeNull();
        }

    }
}