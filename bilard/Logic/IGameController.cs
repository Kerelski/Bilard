using Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IGameController
    {
        public event NotifyDelegateGameController.NotifyBillController? OnChange;
        void CreateBill();
        void DeleteBill(int id);
        void UpdatePosition(IBill bill);
        void ClearBoard();
        int GetSize();
        int GetLength();
        int GetWidth();
        ConcurrentBag<IBill> GetBillRepo();
    }
}
