using NUnit.Framework;

namespace Data.UnitTests
{
    internal class BoardTests
    {
        [Test]
        public void BoardConstructorTest() 
        {
            Board board = new Board(300, 500);
            Assert.That(board.Length == 300);
            Assert.That(board.Width == 500);
            Assert.That(board.getRepository().Count == 0);
        }

        [Test]
        public void SettersTest()
        {
            Board board = new Board(300, 500);

            board.Length = 400;
            Assert.That(board.Length == 400);

            board.Width = 700;
            Assert.That(board.Width == 700);
        }

        [Test]
        public void BillsManipulationTest()
        {
            Board board = new Board(300, 500);
            Bill bill = new Bill(1, 5, 15, 32, 60, 1, 2);

            board.addBill(bill);
            Assert.That(board.getSize() == 1);
            Assert.That(board.getRepository()[0] == bill);

            board.removeBill(bill);
            Assert.That(board.getSize() == 0);
        }
    }
}
