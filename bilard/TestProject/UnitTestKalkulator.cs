using bilard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class UnitTestKalkulator

    {
        [TestMethod]
        public void Poprawna_Suma() 
        {
            var kalkulator = new Kalkulator();

            var result = kalkulator.suma(7, 3);

            Assert.AreEqual(10, result);   
        }

        [TestMethod]
        public void Negatywna_Suma() {
            var kalkulator = new Kalkulator();

            var result = kalkulator.suma(7, 3);

            Assert.AreNotEqual(12, result);
        }

        [TestMethod]
        public void Poprawne_Odejmowanie()
        {
            var kalkulator = new Kalkulator();

            var result = kalkulator.odejmowanie(7, 3);

            Assert.AreEqual(4, result);
        }


        [TestMethod]
        public void Niepoprawne_Odejmowanie()
        {
            var kalkulator = new Kalkulator();

            var result = kalkulator.odejmowanie(7, 3);

            Assert.AreNotEqual(7, result);
        }
    }
}