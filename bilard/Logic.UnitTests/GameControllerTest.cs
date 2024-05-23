using NUnit.Framework;

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

            gameController.CreateBill();
            gameController.CreateBill();

            var bill1 = gameController.GetBillRepo().FirstOrDefault(bill => bill.Id == 0);
            var bill2 = gameController.GetBillRepo().FirstOrDefault(bill => bill.Id == 1);

            bill1.X = 50;
            bill1.Y = 50;
            bill1.SpeedX = 4;
            bill1.SpeedY = 0;

            bill2.X = 50 + bill1.Diameter/2 + bill2.Diameter/2;
            bill2.Y = 50;
            bill2.SpeedX = -4;
            bill2.SpeedY = 0;

            
            gameController.UpdatePosition(bill1);
            gameController.UpdatePosition(bill2);

            
            Assert.That(bill1.SpeedX != 4);
            Assert.That(bill2.SpeedX != -4);
        }
        [Test]
        public void BillRightWallCollisionTest()
        {
            GameController gameController = new GameController(200, 200);

            gameController.CreateBill();

            var bill = gameController.GetBillRepo().FirstOrDefault();

            bill.X = gameController.GetWidth() - bill.Diameter;
            bill.Y = 100;
            bill.SpeedX = 1;
            bill.SpeedY = 0;

            gameController.UpdatePosition(bill);

            Assert.That(bill.SpeedX, Is.EqualTo(-1));
        }

        [Test]
        public void BillTopWallCollisionTest()
        {
            GameController gameController = new GameController(200, 200);

            gameController.CreateBill();

            var bill = gameController.GetBillRepo().FirstOrDefault();

            bill.X = 100;
            bill.Y = 0;
            bill.SpeedX = 0;
            bill.SpeedY = -1;

            gameController.UpdatePosition(bill);

            Assert.That(bill.SpeedY, Is.EqualTo(1));
        }
    }
}
