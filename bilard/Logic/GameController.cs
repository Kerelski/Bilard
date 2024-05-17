using Data;
using System;
using System.Numerics;
using System.Threading;


namespace Logic
{
    public class GameController : IGameController
    {
        private IBoard _board;
        private readonly object _lock = new object();
        public event NotifyDelegateGameController.NotifyBillController? OnChange;
        private double _width;
        private double _length;
        Barrier barrier;

        public GameController(int x, int y)
        {
            this._board = new Board(x, y);
            this._length = x;
            this._width = y;
            barrier = new Barrier(0, (b) =>
            {
                OnChange?.Invoke();
                Thread.Sleep(10);
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

                double x = rand.Next(0, width - diameter);
                double y = rand.Next(0, lenght - diameter);

                Bill bill = new Bill(
                    id,
                    diameter / 10,
                    diameter,
                    x,
                    y,
                    rand.NextDouble() * 2 * Math.PI,
                    rand.Next(1, 5)

                    );
                bool flag = false;
                do
                {
                    flag = false;
                    foreach (Bill secBill in _board.getRepository())
                    {
                        if (IsColliding(x, y, diameter, secBill))
                        {
                            flag = true;
                            bill.X = rand.Next(0, width - diameter);
                            bill.Y = rand.Next(0, lenght - diameter);
                            break;

                        }

                    }
                     
                } while (flag);
                bill.OnChange += UpdatePosition;
                bill.MoveAsync(barrier);
                _board.addBill(bill);
            }
            
        }
        public void DeleteBill(int id)
        {
            IBill bill = _board.getRepository().Find(e => e.Id == id);
            bill.IsMoving = false;
            bill.OnChange -= UpdatePosition;
            _board.removeBill(bill);
        } 

        private bool IsColliding(double x,double y, double d, IBill bill2)
        {
            double dx = bill2.X - x;
            double dy = bill2.Y - y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance < (d / 2 + bill2.Diameter / 2);
        }

        public void UpdatePosition(IBill bill)
        {
            lock (_lock)
            {
                
                
                // Aktualizacja pozycji kulki
                double newX = bill.X + bill.Speed * Math.Cos(bill.Angle);
                double newY = bill.Y + bill.Speed * Math.Sin(bill.Angle);
                double diameter = bill.Diameter;

                //sprawdzenie kolizji
                foreach (IBill secBill in _board.getRepository())
                {
                    
                        if (bill.Id != secBill.Id && IsColliding(newX, newY, diameter, secBill))
                        
                        {
                            double dx = secBill.X - newX;
                            double dy = secBill.Y - newY;
                            double distance = Math.Sqrt(dx * dx + dy * dy);
                            double angle = Math.Atan2(dx, dy);

                            double nx = dx / distance;
                            double ny = dy / distance;

                            double p = 2 * (bill.Speed * Math.Cos(bill.Angle) * nx + bill.Speed * Math.Sin(bill.Angle) * ny -
                                        secBill.Speed * Math.Cos(secBill.Angle) * nx - secBill.Speed * Math.Sin(secBill.Angle) * ny)/
                                        (bill.Weight+secBill.Weight);

                            bill.Speed = bill.Speed - p * secBill.Weight * nx;
                            bill.Angle = Math.Atan2(bill.Speed * Math.Sin(bill.Angle) - p * secBill.Weight * ny, bill.Speed * Math.Cos(bill.Angle) - p * secBill.Weight * nx);

                            secBill.Speed = secBill.Speed+p*bill.Weight*nx;
                            secBill.Angle = Math.Atan2(secBill.Speed * Math.Sin(secBill.Angle) + p * bill.Weight * ny, secBill.Speed * Math.Cos(secBill.Angle) + p * bill.Weight * nx);

                            bill.Angle = (bill.Angle + 2 * Math.PI) % (2 * Math.PI);
                            secBill.Angle = (secBill.Angle + 2 * Math.PI) % (2 * Math.PI);

                            double diff = (distance - bill.Diameter/2 - secBill.Diameter/2)/2;
                            newX -= diff * nx;
                            newY -= diff * ny;
                            secBill.X += diff * nx;
                            secBill.Y += diff * ny;
                          
                        }
                    
                }
                //odbicia od scian
                // Sprawdzenie czy kulka uderzyła w ścianę górną, dolną lub boczną
                if (newX < 0 || newX > _width - diameter || newY < 0 || newY > _length - diameter)
                {
                    // Odbicie kulki od ściany
                    double angle = bill.Angle;

                    // Odbicie od bocznych ścian
                    if (newX < 0 || newX > _width - diameter)
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

                // Aktualizacja pozycji kulki
                bill.X = newX;
                bill.Y = newY;

            }
           
        }

        public void ClearBoard()
        {
            foreach (IBill bill in _board.getRepository())
            {
               bill.IsMoving = false;
                bill.OnChange -= UpdatePosition;
            }
            _board.getRepository().Clear();
        }

        public int GetSize() => _board.getSize();

        public int GetLength() => _board.Length;

        public int GetWidth() => _board.Width;
        public List<IBill> GetBillList() => _board.getRepository();
        
    }
}
