using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Data
{
    public interface IBoard
    {
        int Length { get; set; }
        int Width { get; set; }
        ConcurrentBag<IBill> getRepository();
        void addBill(IBill bill);
        void removeBill(IBill bill);
        int getSize();
        void setRepository(ConcurrentBag<IBill> repository);

        public object Lock { get; }
    }
}
