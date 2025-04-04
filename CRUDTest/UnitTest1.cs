using Microsoft.VisualBasic;

namespace CRUDTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            MathService mathService = new MathService();
            int a = 5, b = 10;
            int expected = 15;
            int actual = mathService.add(a, b);
            Assert.Equal(expected, actual);
        }
    }
}