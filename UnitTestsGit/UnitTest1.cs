using StringCalculator;

namespace StringCalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Calculate_WithEmptyString_ReturnsZero()
        {
            StringCalculator.StringCalculator strCalc = new StringCalculator.StringCalculator();

            int expected = 0;
            int result = strCalc.Calculate(string.Empty);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Calculate_WithSingleNumber_ReturnsValue()
        {
            StringCalculator.StringCalculator strCalc = new();

            int[] inputs = { 1, 3, 27, 13, 3, 8 };

            for (int i = 0; i < inputs.Length; i++)
            {
                int expected = inputs[i];
                int result = strCalc.Calculate(inputs[i].ToString());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Calculate_TwoNumbersComma_ReturnsSum()
        {
            StringCalculator.StringCalculator strCalc = new StringCalculator.StringCalculator();

            string[] inputs = { "22,12", "1,3", "0,  0", "100,1" };
            int[] expectations = { 34, 4, 0, 101 };

            for (int i = 0; i < expectations.Length; i++)
            {
                int expected = expectations[i];
                int result = strCalc.Calculate(inputs[i]);
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Calculate_TwoNumbersNewline_ReturnsSum()
        {
            StringCalculator.StringCalculator stringCalc = new StringCalculator.StringCalculator();

            string[] inputs = { "130\n12", "18\n20", "0\n100", "1\n1" };
            int[] expectations = { 142, 38, 100, 2 };

            for (int i = 0; i < expectations.Length; i++)
            {
                int expected = expectations[i];
                int result = stringCalc.Calculate(inputs[i]);
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Calculate_ThreeNumbersNLandComma_ReturnsSum()
        {
            StringCalculator.StringCalculator calc = new();

            string[] inputs = { "2\n1\n4", "1\n3,9", "1,4\n16", "1,5,25" };
            int[] expectations = { 7, 13, 21, 31 };

            for (int i = 0; i < expectations.Length; i++)
            {
                int expected = expectations[i];
                int result = calc.Calculate(inputs[i]);
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Calculate_NegativeNumber_ThrowsException()
        {
            StringCalculator.StringCalculator calc = new();

            string[] inputs = { "-2", "-4", "1,-12", "-2,1", "7\n-3", "-3\n7", "-1,1,1", "1,-1,1", "1,1,-1", "1,1\n-1", "1\n-1\n1" };
            for(int i = 0;i < inputs.Length; i++)
            {
                try
                {
                    calc.Calculate(inputs[i]);
                }
                catch (Exception ex) 
                {
                    StringAssert.Contains(ex.Message, StringCalculator.StringCalculator.NegativeNumbersExceptionMessage);
                    return;
                }
            }
            Assert.Fail();
        }

        [TestMethod]
        public void Calculate_BigNumbers_Ignored()
        {
            StringCalculator.StringCalculator calc = new();
            string[] inputs = { "1001", "20000", "500, 1000", "500, 1001", "10000\n10000\n1", "10\n20\n2000" };
            int[] expectations = { 0, 0, 1500, 500, 1, 30 };

            for (int i = 0; i < inputs.Length; i++)
            {
                int expected = expectations[i];
                int result = calc.Calculate(inputs[i]);
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Calculate_CustomSingleCharDelimiters()
        {
            StringCalculator.StringCalculator calc = new();
            string[] inputs = { "//#34,12", "//#34#12", "//#34\n12#1", "//#34#12#1" };
            int[] expectations = { 46, 46, 47, 47 };

            for (int i = 0; i < inputs.Length; i++)
            {
                int expected = expectations[i];
                int result = calc.Calculate(inputs[i]);
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Calculate_CustomMultipleCharDelimiters()
        {
            StringCalculator.StringCalculator calc = new();
            string[] inputs = { "//[abrakadabra]1abrakadabra2", "//[:)]7\n8:)9", "//[:(]1,2:(3"};
            int[] expectations = { 3, 24, 6};

            for (int i = 0; i < inputs.Length; i++)
            {
                int expected = expectations[i];
                int result = calc.Calculate(inputs[i]);
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void Calculate_MultipleCustomDelimiters()
        {
            StringCalculator.StringCalculator calc = new();
            string[] inputs = { "//[.][..][...]1.2...3", "//[.][...]1...2...3", "//[..]1,2..3", "//[..]1..2\n3" };
            int[] expectations = { 6, 6, 6, 6 };

            for (int i = 0; i < inputs.Length; i++)
            {
                int expected = expectations[i];
                int result = calc.Calculate(inputs[i]);
                Assert.AreEqual(expected, result);
            }
        }
    }
}