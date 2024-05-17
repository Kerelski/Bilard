using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal interface IGameController
    {
        void CreateBill();
        void DeleteBill(int id);
        void UpdatePosition(IBill bill);
        void ClearBoard();
        int GetSize();
        int GetLength();
        int GetWidth();
        List<IBill> GetBillList();
    }
}
