using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Board
    {
        private int _length;
        private int _width;
        private List<Bill> _repository;

        public Board(int length, int width)
        {
            this._length = length;
            this._width = width;
            _repository = new List<Bill>();
        }

        public int getLength() {  return _length; }
        public int getWidth() { return _width; }

        public List<Bill> getRepository() { return _repository;}

        public void addBill(Bill bill) { _repository.Add(bill); }

        public void removeBill(Bill bill) {
            if (bill == null) throw new NullReferenceException();

            _repository.Remove(bill); 
        }

        public void setLength(int length) { _length = length; }

        public void setWidth(int width) { _width = width; }

        
    }
}
