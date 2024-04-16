using System.Collections.Generic;

namespace Data
{
    public interface IBoard
    {
        int Length { get; set; }
        int Width { get; set; }
        List<Bill> getRepository();
        void addBill(Bill bill);
        void removeBill(Bill bill);
        int getSize();
    }
}
