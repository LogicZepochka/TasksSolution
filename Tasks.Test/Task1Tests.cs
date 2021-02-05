using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tasks;

namespace Tasks.Tests
{
    [TestClass]
    public class Task1Tests
    {
        [TestMethod]
        [TestCategory("Проверка правильности решения")]
        public void CheckCorrectCalc()
        {
            Assert.AreEqual(4, Math.Floor(Program.CalculateExpression("2+2")));
            Assert.AreEqual(6, Math.Floor(Program.CalculateExpression("2+2*2")));
            Assert.AreEqual(8, Math.Floor(Program.CalculateExpression("(2+2)*2")));
            Assert.AreEqual(16, Math.Floor(Program.CalculateExpression("((2+2)*2)*2")));
            Assert.AreEqual(32, Math.Floor(Program.CalculateExpression("33 + (21 * 3 / (781 + 12 - (5 * 1025)) / 17)")));
        }

        [TestMethod]
        [TestCategory("Проверка исключений")]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckDoubleOperator()
        {
            Program.CalculateExpression("2++2");
        }

        [TestMethod]
        [TestCategory("Проверка исключений")]
        public void CheckDoubleBracketsNotThrowException()
        {
            try
            {
                Program.CalculateExpression("2*((2+1)/1)");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [TestCategory("Проверка исключений")]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckDivideToZero()
        {
            Program.CalculateExpression("2/0");
        }

        [TestMethod]
        [TestCategory("Проверка исключений")]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckWrongExpression()
        {
            Program.CalculateExpression("2+2*2withLetters");
        }
    }
}
