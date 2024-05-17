using System.Collections.Generic;

namespace Data
{
    public interface IBoard
    {
        int Length { get; set; }
        int Width { get; set; }
        List<IBill> getRepository();
        void addBill(IBill bill);
        void removeBill(IBill bill);
        int getSize();
        void setRepository(List<IBill> repository);
    }
}
