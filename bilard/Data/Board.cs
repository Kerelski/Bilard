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
        private double _length;
        private double _width;
        private List<Bill> _repository;

        public Board(double length, double width)
        {
            this._length = length;
            this._width = width;
            _repository = new List<Bill>();
        }

        public double getLength() {  return _length; }
        public double getWidth() { return _width; }

        public List<Bill> getRepository() { return _repository;}

        public void addBill(Bill bill) { _repository.Add(bill); }

        public void removeBill(Bill bill) { _repository.Remove(bill); }

        public void setLength(double length) { _length = length; }

        public void setWidth(double width) { _width = width; }

        
    }
}
