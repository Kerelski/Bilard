using Data;
using System;
using System.Numerics;


namespace Logic
{
    static class GameController
    {
        public static void createBill(Board board) {

            int id = board.getRepository().Count();
            int width = board.getWidth();
            int lenght = board.getLength();

            var rand = new Random();

            int radius = rand.Next(15, 30);

            board.addBill(new Bill(
                id,
                1,
                radius,
                rand.Next(0 + radius, width - radius),
                rand.Next(0 + radius, lenght - radius),
                rand.NextDouble() * 2 * Math.PI
                )) ; 
        }
        public static void deleteBill(Board board, int id)
        {
            Bill bill = board.getRepository().Find(e => e.getId() == id);

            board.removeBill(bill);
        }

        public static void updatePosition(Board board)
        {
            double width = board.getWidth();
            double length = board.getLength();

            foreach (Bill bill in board.getRepository())
            {
                // Aktualizacja pozycji kulki
                double newX = bill.getX() + 1 * Math.Cos(bill.getAngle());
                double newY = bill.getY() + 1 * Math.Sin(bill.getAngle());
                int radius = bill.getRadius();
                // Sprawdzenie czy kulka uderzyła w ścianę
                if (newX < 0 + radius || newX > width - radius || newY < 0 + radius || newY > length - radius)
                {
                    // Odbicie kulki od ściany
                    double angle = bill.getAngle();
                    double reflectionAngle = Math.Atan2(-Math.Sin(angle), Math.Cos(angle));
                    bill.setAngle(reflectionAngle);
                }
                else
                {
                    // Aktualizacja pozycji kulki
                    bill.setX(newX);
                    bill.setY(newY);
                }
            }
        }
    }
}
