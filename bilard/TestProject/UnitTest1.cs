using bilard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Poprawna_Suma() 
        {
            var kalkulator = new Kalkulator();

            var result = kalkulator.suma(7, 3);

            Assert.AreEqual(10, result);   
        }
    }
}