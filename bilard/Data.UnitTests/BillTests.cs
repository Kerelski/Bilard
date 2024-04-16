using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Data.UnitTests
{
    internal class BillTests
    {
        [Test]
        public void BillConstructorTest()
        {
            Bill bill = new Bill(1, 5, 15, 32, 60, 1);
            Assert.That(bill.Id == 1);
            Assert.That(bill.Weight == 5);
            Assert.That(bill.Radius == 15);
            Assert.That(bill.X == 32);
            Assert.That(bill.Y == 60);
            Assert.That(bill.Angle == 1);
            Assert.That(Regex.IsMatch(bill.Color, "^#[0-9A-Fa-f]{6}$"));

        }

        [Test]
        public void SettersTest()
        {
            Bill bill = new Bill(1, 5, 15, 32, 60, 1);

            bill.Id = 5;
            Assert.That(bill.Id == 5);

            bill.Radius = 2;
            Assert.That(bill.Radius == 2);

            Assert.Throws<InvalidDataException>(() => bill.Radius = -5);

            bill.Angle = 2* Math.PI;
            Assert.That(bill.Angle == 2 * Math.PI);

        }



    }
}
