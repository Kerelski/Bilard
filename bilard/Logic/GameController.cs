using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;
using System.Threading;


namespace Logic
{
    public class GameController : IGameController
    {
        private IBoard _board;
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
            {
                
                lock (_board.Lock)
                {

                    int id = _board.getRepository().Count();
                    int width = _board.Width;
                    int lenght = _board.Length;

                    var rand = new Random();

                    int diameter = rand.Next(50, 75);

                    double x = rand.Next(0, width - diameter);
                    double y = rand.Next(0, lenght - diameter);
                    double sX, sY;

                    do
                    {
                        sX = rand.NextDouble() * 2 - 1;
                        sY = rand.NextDouble() * 2 - 1;
                    } while (sX == 0 && sY == 0);

                    Bill bill = new Bill(
                        id,
                        diameter / 10,
                        diameter,
                        x,
                        y,
                        sX,
                        sY
                        );

                    bool flag = false;
                    do
                    {
                        flag = false;
                        foreach (Bill secBill in _board.getRepository())
                        {
                            if (IsColliding(bill.X, bill.Y, diameter, secBill))
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
                    DiagnosticLogger.Log($"Ball {bill.Id} created at ({bill.X}, {bill.Y}) with diameter {bill.Diameter}");
                }
                
            }
        }
        public void DeleteBill(int id)
        {
            lock (_board.Lock)
            {
            IBill bill = _board.getRepository().FirstOrDefault(bill => bill.Id == id);
                if (bill != null)
                {
                    bill.IsMoving = false;
                    bill.OnChange -= UpdatePosition;
                    _board.removeBill(bill);
                    DiagnosticLogger.Log($"Ball {bill.Id} removed from board");
                }

            }
        }

        private bool IsColliding(double x, double y, double d, IBill bill2)
        {
            double dx = x - bill2.X;
            double dy = y - bill2.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            
            return distance <= ((d / 2) + (bill2.Diameter / 2));
        }

        public void UpdatePosition(IBill bill){
            {
                lock (bill.Lock)
                {
                    double newX = bill.X + bill.SpeedX;
                    double newY = bill.Y + bill.SpeedY;
                    
                    foreach (IBill secBill in _board.getRepository())
                    {
                        if (bill.Id == secBill.Id) continue;
                        if (IsColliding(newX, newY, bill.Diameter, secBill))

                            if(Monitor.TryEnter(secBill.Lock)) 
                           {
                                
                                try
                                {
                                    DiagnosticLogger.Log($"Collision between ball {bill.Id}({bill.X}, {bill.Y}, {bill.Diameter}) and ball {secBill.Id}({secBill.X}, {secBill.Y}, {secBill.Diameter})");
                                    double dx = newX - secBill.X;
                                    double dy = newY - secBill.Y;
                                    double distance = Math.Sqrt((dx * dx) + (dy * dy));
                                    double angle = Math.Atan2(dy, dx);
                                    if (distance == 0) continue;
                                    dx /= distance;
                                    dy /= distance;
                                    double tx = -dy;
                                    double ty = dx;

                                    double v1n = dx * bill.SpeedX + dy * bill.SpeedY;
                                    double v1t = tx * bill.SpeedX + ty * bill.SpeedY;
                                    double v2n = dx * secBill.SpeedX + dy * secBill.SpeedY;
                                    double v2t = tx * secBill.SpeedX + ty * secBill.SpeedY;

                                    double v1nAfter = (v1n * (bill.Weight - secBill.Weight) + 2 * secBill.Weight * v2n) / (bill.Weight + secBill.Weight);
                                    double v2nAfter = (v2n * (secBill.Weight - bill.Weight) + 2 * bill.Weight * v1n) / (bill.Weight + secBill.Weight);

                                    bill.SpeedX = v1nAfter * dx + v1t * tx;
                                    bill.SpeedY = v1nAfter * dy + v1t * ty;
                                    secBill.SpeedX = v2nAfter * dx + v2t * tx;
                                    secBill.SpeedY = v2nAfter * dy + v2t * ty;

                                    double diff = ((bill.Diameter / 2 + secBill.Diameter / 2) - distance) / 2;
                                    newX += diff * dx;
                                    newY += diff * dy;
                                    secBill.X -= diff * dx;
                                    secBill.Y -= diff * dy;
                                }
                                finally
                                {
                                    Monitor.Exit(secBill.Lock);
                                }
                                   
                            }
                            else
                            {
                                return;
                            }
                            
                       
                    }

                    //kolizje ze ścianą 
                    if (newX < 0)
                    {
                        newX = 0;
                        bill.SpeedX *= -1;
                    }
                    if (newX > _width - bill.Diameter)
                    {

                        newX = _width - bill.Diameter;
                        bill.SpeedX *= -1;
                    }
                    if (newY < 0)
                    {

                        newY = 0;
                        bill.SpeedY *= -1;
                    }
                    if (newY > _length - bill.Diameter)
                    {

                        newY = _length - bill.Diameter;
                        bill.SpeedY *= -1;
                    }

                    bill.X = newX;
                    bill.Y = newY;

                }
            }
                
         
        
           
        }
   
    public void ClearBoard()
        {
            lock(_board.Lock)
            {
                foreach (IBill bill in _board.getRepository())
                {
                    bill.IsMoving = false;
                    bill.OnChange -= UpdatePosition;
                }
                int count = _board.getRepository().Count;
                _board.getRepository().Clear();
                DiagnosticLogger.Log($"All ({count}) balls removed from board");
            }
            
        }

        public int GetSize() => _board.getSize();

        public int GetLength() => _board.Length;

        public int GetWidth() => _board.Width;
        public ConcurrentBag<IBill> GetBillRepo() => _board.getRepository();
        
    }
}
