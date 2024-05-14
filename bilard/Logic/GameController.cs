using Data;
using System;
using System.Numerics;
using System.Threading;


namespace Logic
{
    public class GameController
    {
        private Board _board;

        public GameController(int x, int y)
        {
            this._board = new Board(x, y);
        }

        public void CreateNumberOfBills(int numberOfBills)
        {
            Parallel.For(0, numberOfBills, i =>
            {
                CreateBill();
            });
        }
        public void CreateBill() {
            lock (_board)
            {
                int id = _board.getRepository().Count();
                int width = _board.Width;
                int lenght = _board.Length;

                var rand = new Random();

                int diameter = rand.Next(25, 75);

                Bill bill = new Bill(
                    id,
                    diameter / 10,
                    diameter,
                    rand.Next(0, width-diameter),
                    rand.Next(0, lenght-diameter),
                    rand.NextDouble() * 2 * Math.PI,
                    rand.Next(1, 5)

                    );
                bool flag = false;
                do
                {
                    flag = false;
                    foreach (Bill secBill in _board.getRepository())
                    {
                        if (IsColliding(bill, secBill))
                        {
                            flag = true;
                            bill.X = rand.Next(0, width - diameter);
                            bill.Y = rand.Next(0, lenght - diameter);
                            break;

                        }

                    }
                     
                } while (flag);
                _board.addBill(bill);
            }
            
        }
        public void DeleteBill(int id)
        {
            Bill bill = _board.getRepository().Find(e => e.Id == id);

            _board.removeBill(bill);
        } 

        private bool IsColliding(Bill bill1, Bill bill2)
        {
            double dx = bill1.X - bill2.X;
            double dy = bill1.Y - bill2.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance <= (bill1.Diameter / 2 + bill2.Diameter / 2);
        }

        public void StartSimulation()
        {
            foreach (Bill bill in _board.getRepository())
            {
                Task.Run(() => UpdatePosition(bill));
            }
        }
        public void UpdatePosition(Bill bill)
        {
            double width = _board.Width;
            double length = _board.Length;
            //odbicia od scian
            // Aktualizacja pozycji kulki
            double newX = bill.X + bill.Speed * Math.Cos(bill.Angle);
            double newY = bill.Y + bill.Speed * Math.Sin(bill.Angle);
            double diameter = bill.Diameter;

                    // Sprawdzenie czy kulka uderzyła w ścianę górną, dolną lub boczną
            if (newX < 0 || newX > width - diameter || newY < 0 || newY > length - diameter)
            {
                        // Odbicie kulki od ściany
                double angle = bill.Angle;

                        // Odbicie od bocznych ścian
                if (newX < 0 || newX > width - diameter)
                {
                    angle = Math.PI - angle; // Odbicie kąta

                }
                else
                {
                    angle = -angle; // Odbicie od górnej lub dolnej ściany
                }

                bill.Angle = angle;
                newX = bill.X + bill.Speed * Math.Cos(bill.Angle);
                newY = bill.Y + bill.Speed * Math.Sin(bill.Angle);

            }
            
            double oldX = bill.X;
            double oldY = bill.Y;

            // Aktualizacja pozycji kulki
            bill.X = newX;
            bill.Y = newY;

            foreach (Bill secBill in _board.getRepository())
            {
                if (bill == secBill) continue;
                else
                {
                    if(IsColliding(bill, secBill))
                    lock (_board)
                    {
                        double angle = Math.Atan2(secBill.Y - bill.Y, secBill.X - bill.X);
                        secBill.Angle = 2 * angle - secBill.Angle;
                        bill.Angle = 2 * angle - bill.Angle;

                        double tempSpeed = bill.Speed;

                        secBill.Speed = ((secBill.Weight - bill.Weight) * secBill.Speed + 2 * bill.Weight * bill.Speed) / (secBill.Weight + bill.Weight);
                        bill.Speed = ((bill.Weight - secBill.Weight) * bill.Speed + 2 * secBill.Weight * tempSpeed) / (secBill.Weight + bill.Weight);
                        bill.X += bill.X - secBill.X;
                        bill.Y += bill.Y - secBill.Y; 
                        
                    }
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
