
using Data;
using Logic;
using System.Collections.Concurrent;

namespace Model
{
    public class GameModel
    {
        private IGameController _controller;

        public GameModel(int x, int y)
        {
             this._controller = new GameController(x, y);
        }

        public void AddBill()
        {
            _controller.CreateBill();
        }

        public void DeleteBill()
        {
            _controller.DeleteBill(_controller.GetSize()-1);
        }

        public void ClearBoard()
        {
            _controller.ClearBoard();
        }
        public IGameController GetController {
            get => _controller;
        }

        public ConcurrentBag<IBill> GetBills() => _controller.GetBillRepo();

        public int GetLength() => _controller.GetLength();

        public int GetWidth() => _controller.GetWidth();

        public int GetSize() => _controller.GetSize();
    }

}
