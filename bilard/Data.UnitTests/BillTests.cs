using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Data.UnitTests
{
    internal class BillTests
    {
        [Test]
        public void BillConstructorTest()
        {
            Bill bill = new Bill(1, 5, 15, 32, 60, 2, 3);
            Assert.That(bill.Id == 1);
            Assert.That(bill.Weight == 5);
            Assert.That(bill.Diameter == 15);
            Assert.That(bill.X == 32);
            Assert.That(bill.Y == 60);
            Assert.That(bill.SpeedX == 2);
            Assert.That(bill.SpeedY == 3);
            Assert.That(Regex.IsMatch(bill.Color, "^#[0-9A-Fa-f]{6}$"));

        }

        [Test]
        public void SettersTest()
        {
            Bill bill = new Bill(1, 5, 15, 32, 60, 2,3);

            bill.Id = 5;
            Assert.That(bill.Id == 5);

            bill.Diameter = 2;
            Assert.That(bill.Diameter == 2);
            Assert.Throws<InvalidDataException>(() => bill.Diameter = -5);

            bill.SpeedX = 4;
            Assert.That(bill.SpeedX == 4);

            bill.SpeedY = 1;
            Assert.That(bill.SpeedY == 1);

            bill.Weight = 10;
            Assert.That(bill.Weight == 10);
            Assert.Throws<InvalidDataException>(() => bill.Weight = -1);
        }



    }
}
