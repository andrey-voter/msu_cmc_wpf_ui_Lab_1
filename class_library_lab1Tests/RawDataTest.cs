using FluentAssertions;
using ñlass_library_lab1;

namespace class_library_lab1Tests
{
    public class RawDataTest
    {
        [Fact]
        public void ConstructorRawDataTest()
        {

            FRaw func = RawData.LinearFunc;
            RawData rawData = new RawData(1, 2, 3, true, func);
            rawData.a.Should().Be(1);
            rawData.b.Should().Be(2);
            rawData.NodeCount.Should().Be(3);
            rawData.uniform.Should().Be(true);
            rawData.func.Should().Be(func);

        }

        [Fact]
        public void ConstructorRawDataExceptionTest()
        {

            var code = new Action(() =>
            {
                FRaw func = RawData.RandomFunc;
                RawData rawData = new RawData(1, 2, -200, true, func);
            });

            code.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("Empty data in RawData constructor");

        }

        [Fact]
        public void RawDataGridTest()
        {

            FRaw func = RawData.CubeFunc;
            RawData rawData = new RawData(0, 2, 3, true, func);
            rawData.nodes[0].Should().Be(0);
            rawData.nodes[1].Should().Be(1);
            rawData.nodes[2].Should().Be(2);

        }


    }
}