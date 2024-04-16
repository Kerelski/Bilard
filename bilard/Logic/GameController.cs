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
            int lenght = _board.Width;

            var rand = new Random();

            int radius = rand.Next(15, 30);

            _board.addBill(new Bill(
                id,
                1,
                radius,
                rand.Next(0 + radius, width - radius),
                rand.Next(0 + radius, lenght - radius),
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
            double length = _board.Width;

            foreach (Bill bill in _board.getRepository())
            {
                // Aktualizacja pozycji kulki
                double newX = bill.X + 1 * Math.Cos(bill.Angle);
                double newY = bill.Y + 1 * Math.Sin(bill.Angle);
                int radius = bill.Radius;
                // Sprawdzenie czy kulka uderzyła w ścianę
                if (newX < 0 + radius || newX > width - radius || newY < 0 + radius || newY > length - radius)
                {
                    // Odbicie kulki od ściany
                    double angle = bill.Angle;
                    double reflectionAngle = Math.Atan2(-Math.Sin(angle), Math.Cos(angle));
                    bill.Angle = reflectionAngle;
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
        
        public List<Bill> GetBillList() => _board.getRepository();
        

    }
}
