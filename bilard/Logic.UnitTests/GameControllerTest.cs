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
            Assert.That(gameController.GetBillList()[0].Y >= 0);
            Assert.That(gameController.GetBillList()[0].X >= 0);
            Assert.That(gameController.GetBillList()[0].Y < gameController.GetLength());
            Assert.That(gameController.GetBillList()[0].X < gameController.GetWidth());

            gameController.DeleteBill(0);
            Assert.That(gameController.GetSize() == 0);

        }

        [Test]
        public void UpdatePositionTest()
        {
            GameController gameController = new GameController(200, 200);
            gameController.CreateBill();
            Assert.That(gameController.GetBillList()[0].Y >= 0);
            Assert.That(gameController.GetBillList()[0].X >= 0);
            Assert.That(gameController.GetBillList()[0].Y < gameController.GetLength());
            Assert.That(gameController.GetBillList()[0].X < gameController.GetWidth());

            gameController.UpdatePosition();
            Assert.That(gameController.GetBillList()[0].Y >= 0);
            Assert.That(gameController.GetBillList()[0].X >= 0);
            Assert.That(gameController.GetBillList()[0].Y < gameController.GetLength());
            Assert.That(gameController.GetBillList()[0].X < gameController.GetWidth());
        }

        [Test]
        public void ClearBoardTest()
        {
            GameController gameController = new GameController(200, 200);
            for(int i = 0; i < 10; i++) 
            {
                gameController.CreateBill();
                Assert.That(gameController.GetBillList().Count == i + 1);
                Assert.That(gameController.GetBillList()[i].Y >= 0);
                Assert.That(gameController.GetBillList()[i].X >= 0);
                Assert.That(gameController.GetBillList()[0].Y < gameController.GetLength());
                Assert.That(gameController.GetBillList()[0].X < gameController.GetWidth());
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
    }
}
