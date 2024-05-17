
using Data;
using Logic;

namespace Model
{
    public class GameModel
    {
        private GameController _controller;

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
        public GameController GetController {
            get => _controller;
        }

        public List<IBill> GetBills() => _controller.GetBillList();

        public int GetLength() => _controller.GetLength();

        public int GetWidth() => _controller.GetWidth();

        public int GetSize() => _controller.GetSize();
    }

}
