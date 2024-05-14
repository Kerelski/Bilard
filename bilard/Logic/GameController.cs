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

            int id = _board.getRepository().Count();
            int width = _board.Width;
            int lenght = _board.Length;

            var rand = new Random();

            int diameter = rand.Next(25, 75);

            _board.addBill(new Bill(
                id,
                diameter/10,
                diameter,
                rand.Next(0, width - diameter*2),
                rand.Next(0, lenght - diameter*2),
                rand.NextDouble() * 2 * Math.PI,
                //rand.Next(1, 5)
                1
                )) ; 
        }
        public void DeleteBill(int id)
        {
            Bill bill = _board.getRepository().Find(e => e.Id == id);

            _board.removeBill(bill);
        }
         /*public void BounceBills()
         {
             List<Bill> Bills = _board.getRepository();
             for (int i = 0; i < Bills.Count; i++)
             {
                 for (int j = i + 1; j < Bills.Count; j++)
                 {
                     if (IsColliding(Bills[i], Bills[j]))
                     {
                        lock (_board)
                        { 
                             // Obliczamy nowy kąt po zderzeniu
                             double angle = Math.Atan2(Bills[j].Y - Bills[i].Y, Bills[j].X - Bills[i].X);
                             Bills[i].Angle = 2 * angle - Bills[i].Angle;
                             Bills[j].Angle = 2 * angle - Bills[j].Angle;
                             double tempSpeed = Bills[i].Speed;

                             Bills[i].Speed = ((Bills[i].Weight - Bills[j].Weight) * Bills[i].Speed + 2 * Bills[j].Weight * Bills[j].Speed) / (Bills[i].Weight + Bills[j].Weight);
                             Bills[j].Speed = ((Bills[j].Weight - Bills[i].Weight) * Bills[j].Speed + 2 * Bills[i].Weight * tempSpeed) / (Bills[i].Weight + Bills[j].Weight);
                     
                        }
                     }
                 }
             }
         }*/

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
            List<Bill> Bills = _board.getRepository();
            Parallel.For(0, Bills.Count, i =>
            {
                if (Bills[i] == bill) return; // Pominięcie bieżącej kulki

                if (IsColliding(Bills[i], bill))
                {
                    lock (_board)
                    {
                        double angle = Math.Atan2(bill.Y - Bills[i].Y, bill.X - Bills[i].X);
                        Bills[i].Angle = 2 * angle - Bills[i].Angle;
                        bill.Angle = 2 * angle - bill.Angle;                  

                        double tempSpeed = Bills[i].Speed;

                        Bills[i].Speed = ((Bills[i].Weight - bill.Weight) * Bills[i].Speed + 2 * bill.Weight * bill.Speed) / (Bills[i].Weight + bill.Weight);
                        bill.Speed = ((bill.Weight - Bills[i].Weight) * bill.Speed + 2 * Bills[i].Weight * tempSpeed) / (Bills[i].Weight + bill.Weight);                        _board.setRepository(Bills);
                    }
                }
            });
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
            

            // Aktualizacja pozycji kulki
            bill.X = newX;
            bill.Y = newY;
            
                
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
