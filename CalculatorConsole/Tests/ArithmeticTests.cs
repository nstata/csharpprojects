using NUnit.Framework;
using System;

namespace CalculatorConsole.Tests
{
    [TestFixture]

    public class ArithmeticTests
    {
        [TestCase(12, 0, 12)]
        [TestCase(12, -2, 14)]
        [TestCase(12, 4.3, 7.7)]
        [TestCase(12, 10.001, 1.999)]
        [TestCase(12, null, 12)]
        [TestCase(12, 9.999999999, 2.000000001)]
        public void TestAdd(double expected, double d1, double d2)
        {
            //Console.WriteLine($"d1: {d1}, d2: {d2}");
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Add(d1, d2);
            Assert.AreEqual(expected, result);

        }

        [TestCase(13, 0, 12)]
        [TestCase(13, -2, 14)]
        [TestCase(13, 4.3, 7.7)]
        [TestCase(13, 10.001, 1.999)]
        [TestCase(13, null, 12)]
        public void TestAddNegativeScenarios(double expected, double d1, double d2)
        {
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Add(d1, d2);
            Assert.AreNotEqual(expected, result);

        }
        [TestCase(12, 12, 0)]
        [TestCase(12, 14, 2)]
        [TestCase(12, 14.3, 2.3)]
        [TestCase(12, 14.001, 2.001)]
        [TestCase(12, 12, null)]
        [TestCase(12, 14.999999999, 2.999999999)]
        public void TestSub(double expected, double d1, double d2)
        {
            //Console.WriteLine($"d1: {d1}, d2: {d2}");
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Sub(d1, d2);
            Assert.AreEqual(expected, result);

        }
        [TestCase(11, 12, 0)]
        [TestCase(12, 14, -2)]
        [TestCase(13, 14.3, 2.3)]
        [TestCase(13, 14.001, 2.001)]
        [TestCase(11, 12, null)]
        public void TestSubNegative(double expected, double d1, double d2)
        {
            //Console.WriteLine($"d1: {d1}, d2: {d2}");
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Sub(d1, d2);
            Assert.AreNotEqual(expected, result);

        }
        [TestCase(12, 3, 4)]
        [TestCase(12, -3, -4)]
        [TestCase(12, 5.997, 2.001)]
        [TestCase(12, 4.000000001333, 2.999999999)]
        public void TestMult(double expected, double d1, double d2)
        {
            //Console.WriteLine($"d1: {d1}, d2: {d2}");
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Mult(d1, d2);
            Assert.AreEqual(expected, result, .01);

        }
        
        [TestCase(12, 14, -2)]
        [TestCase(13, 1, 2.3)]
        [TestCase(13, 14.001, 2.001)]
        public void TestMultNegative(double expected, double d1, double d2)
        {
            //Console.WriteLine($"d1: {d1}, d2: {d2}");
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Mult(d1, d2);
            Assert.AreNotEqual(expected, result);

        }
        [TestCase(12, 12, 0)]
        [TestCase(12, 12, null)]
        public void TestMultWhenANumberIsZeroOrNull(double expected, double d1, double d2)
        {
            //Console.WriteLine($"d1: {d1}, d2: {d2}");
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Mult(d1, d2);
            Assert.That(result, Is.Zero);

        }


        [TestCase(12, 36, 3)]
        [TestCase(12, -48, -4)]
        [TestCase(12, 12.0001, .9999)]
        [TestCase(12, 24.000000001, 1.99999999999)]
        public void TestDiv(double expected, double d1, double d2)
        {
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Div(d1, d2);
            Assert.AreEqual(expected, result, 0.01);

        }

        [TestCase(12, 14, -2)]
        [TestCase(13, 26, 2.3)]
        [TestCase(13, 12.001, .999999)]
        public void TestDivNegative(double expected, double d1, double d2)
        {
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Div(d1, d2);
            Assert.AreNotEqual(expected, result);

        }


        [TestCase(11, 12, 0)]
        public void TestDivWhenDenominatorIsZero(double expected, double d1, double d2)
        {
            
            Arithmetic arithmetic = new Arithmetic();
            Assert.Throws<DivideByZeroException>(() => arithmetic.Div(d1, d2));
        }
    }
}
