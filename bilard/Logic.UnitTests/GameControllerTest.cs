using Data;
using NUnit.Framework;
using System.Diagnostics;

namespace Logic.UnitTests
{
    internal class GameControllerTest
    {

        [Test]
        public void CreateDeleteBillTest()
        {
            GameController gameController = new GameController(200, 200);
            gameController.CreateBill();
            Assert.That(gameController.GetSize() == 1);
            Assert.That(gameController.GetBillRepo().FirstOrDefault().Y >= 0);
            Assert.That(gameController.GetBillRepo().FirstOrDefault().X >= 0);
            Assert.That(gameController.GetBillRepo().FirstOrDefault().Y < gameController.GetLength());
            Assert.That(gameController.GetBillRepo().FirstOrDefault().X < gameController.GetWidth());

            gameController.DeleteBill(0);
            Assert.That(gameController.GetSize() == 0);

        }

        [Test]
        public void UpdatePositionTest()
        {
            GameController gameController = new GameController(200, 200);
            gameController.CreateBill();
            Assert.That(gameController.GetBillRepo().FirstOrDefault().Y >= 0);
            Assert.That(gameController.GetBillRepo().FirstOrDefault().X >= 0);
            Assert.That(gameController.GetBillRepo().FirstOrDefault().Y < gameController.GetLength());
            Assert.That(gameController.GetBillRepo().FirstOrDefault().X < gameController.GetWidth());

            gameController.UpdatePosition(gameController.GetBillRepo().FirstOrDefault());
            Assert.That(gameController.GetBillRepo().FirstOrDefault().Y >= 0);
            Assert.That(gameController.GetBillRepo().FirstOrDefault().X >= 0);
            Assert.That(gameController.GetBillRepo().FirstOrDefault().Y < gameController.GetLength());
            Assert.That(gameController.GetBillRepo().FirstOrDefault().X < gameController.GetWidth());
        }

        [Test]
        public void ClearBoardTest()
        {
            GameController gameController = new GameController(200, 200);
            for (int i = 0; i < 3; i++)
            {
                gameController.CreateBill();
                Assert.That(gameController.GetBillRepo().Count == i+1);
                Assert.That(gameController.GetBillRepo().FirstOrDefault(bill => bill.Id == i).Y >= 0);
                Assert.That(gameController.GetBillRepo().FirstOrDefault(bill => bill.Id == i).X >= 0);
                Assert.That(gameController.GetBillRepo().FirstOrDefault(bill => bill.Id == i).Y < gameController.GetLength());
                Assert.That(gameController.GetBillRepo().FirstOrDefault(bill => bill.Id == i).X < gameController.GetWidth());
            }

            gameController.ClearBoard();
            Assert.That(gameController.GetSize() == 0);

        }

        [Test]
        public void BoardSizeTest()
        {
            GameController gameController = new GameController(200, 400);
            Assert.That(gameController.GetWidth() == 400);
            Assert.That(gameController.GetLength() == 200);
        }

        [Test]
        public void BillCollisionTest()
        {
            GameController gameController = new GameController(200, 200);

            gameController.GetBillRepo().Add(new Bill(0, 5, 50, 50, 50, 10, 0));
            gameController.GetBillRepo().Add(new Bill(1, 5, 50, 105, 50, -10, 0));

            var bill1 = gameController.GetBillRepo().FirstOrDefault(bill => bill.Id == 0);
            var bill2 = gameController.GetBillRepo().FirstOrDefault(bill => bill.Id == 1);

            gameController.UpdatePosition(bill1);
            gameController.UpdatePosition(bill2);

            Assert.That(bill1.SpeedX != 10, "" + bill1.Diameter+ " " + bill2.Diameter +" "+ bill1.X + " " + bill2.X +" "+ bill1.Y + " " + bill2.Y);
            Assert.That(bill2.SpeedX != -10);
        }
        [Test]
        public void BillRightWallCollisionTest()
        {
            GameController gameController = new GameController(200, 200);

            gameController.GetBillRepo().Add(new Bill(0, 5, 50, gameController.GetWidth() - 50, 100, 1, 0));

            var bill = gameController.GetBillRepo().FirstOrDefault();

            gameController.UpdatePosition(bill);

            Assert.That(bill.SpeedX, Is.EqualTo(-1));
        }

        [Test]
        public void BillTopWallCollisionTest()
        {
            GameController gameController = new GameController(200, 200);

            gameController.GetBillRepo().Add(new Bill(0, 5, 50, 100, 0, 0, -1));

            var bill = gameController.GetBillRepo().FirstOrDefault();

            gameController.UpdatePosition(bill);

            Assert.That(bill.SpeedY, Is.EqualTo(1));
        }
    }
}
