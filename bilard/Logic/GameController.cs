using Data;
using System.Numerics;


namespace Logic
{
    static class GameController
    {
        public static void createBill(Board board, double weight, double radius, double x, double y) {
             board.addBill(new Bill(weight, radius, x, y)); 
        }
        public static void deleteBill(Board board, Bill bill, double weight, double radius, double x, double y)
        {
            board.removeBill(bill);
        }

        public static void updatePosition(Board board)
        {
            foreach(Bill bill in board.getRepository())
            {
                bill.setX(bill.getX() + 1);
                bill.setY(bill.getY() + 1);
            }
        }
    }
}
