using FluentAssertions;
using сlass_library_lab1;


namespace class_library_lab1Tests
{
    public class SplineDataTest
    {

        [Fact]
        public void ConstructorSplineDataTest()
        {

            FRaw func = RawData.LinearFunc;
            RawData rawData = new RawData(1, 2, 3, true, func);
            SplineData splineData = new SplineData(rawData, -2.1, 3.66, 15);
            splineData.rawData.Should().NotBeNull();
            splineData.Spline2Left.Should().Be(-2.1);
            splineData.Spline2Right.Should().Be(3.66);
            splineData.NodeCnt.Should().Be(15);

        }

        [Fact]
        public void ConstructorSplineDataExtremeTest()
        {

            FRaw func = RawData.RandomFunc;
            RawData rawData = new RawData(1, 2000, 367, false, func);
            SplineData splineData = new SplineData(rawData, -204800, 32222.9989, 8166);
            splineData.rawData.Should().NotBeNull();
            splineData.Spline2Left.Should().Be(-204800);
            splineData.Spline2Right.Should().Be(32222.9989);
            splineData.NodeCnt.Should().Be(8166);

        }

        [Fact]
        public void InterpolationSplineDataTest()
        {

            FRaw func = RawData.CubeFunc;
            RawData rawData = new RawData(1, 2, 3, true, func);
            SplineData splineData = new SplineData(rawData, 1, 2, 3);
            splineData.CalculateInerpolation();
            splineData.IntegralValue.Should().Be(13.976562499999998);

        }
    }
}
