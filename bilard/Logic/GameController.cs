using Data;
using System;
using System.Numerics;


namespace Logic
{
    public class GameController
    {
        private Board _board;

        public GameController(int x, int y)
        {
            this._board = new Board(x, y);
        }

        public void CreateBill() {

            int id = _board.getRepository().Count();
            int width = _board.Width;
            int lenght = _board.Length;

            var rand = new Random();

            int radius = rand.Next(25, 75);

            _board.addBill(new Bill(
                id,
                1,
                radius,
                rand.Next(0, width-radius*2),
                rand.Next(0, lenght - radius*2),
                rand.NextDouble() * 2 * Math.PI
                )) ; 
        }
        public void DeleteBill(int id)
        {
            Bill bill = _board.getRepository().Find(e => e.Id == id);

            _board.removeBill(bill);
        }

        public void UpdatePosition()
        {
            double width = _board.Width;
            double length = _board.Length;

            foreach (Bill bill in _board.getRepository())

            {
                // Aktualizacja pozycji kulki
                double newX = bill.X + 3 * Math.Cos(bill.Angle);
                double newY = bill.Y + 3 * Math.Sin(bill.Angle);
                int radius = bill.Radius;

                // Sprawdzenie czy kulka uderzyła w ścianę górną, dolną lub boczną
                if (newX < 0 || newX > width - radius || newY < 0 || newY > length - radius)
                {
                    // Odbicie kulki od ściany
                    double angle = bill.Angle;

                    // Odbicie od bocznych ścian
                    if (newX < 0 || newX > width - radius)
                    {
                        angle = Math.PI - angle; // Odbicie kąta
                    }
                    else
                    {
                        angle = -angle; // Odbicie od górnej lub dolnej ściany
                    }

                    bill.Angle = angle;
                }
                else
                {
                    // Aktualizacja pozycji kulki
                    bill.X = newX;
                    bill.Y = newY;
                }
            }
        }

        public void ClearBoard()
        {
            _board.getRepository().Clear();
        }

        public int GetSize() => _board.getSize();

        public int GetLength() => _board.Length;

        public int GetWidth() => _board.Width;
        public List<Bill> GetBillList() => _board.getRepository();
        


    }
}
