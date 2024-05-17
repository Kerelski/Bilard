using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Board: IBoard
    {
        private int _length;
        private int _width;
        private List<IBill> _repository;

        public Board(int length, int width)
        {
            this._length = length;
            this._width = width;
            _repository = new List<IBill>();
        }

        public int Length
        {
            get => _length;
            set => _length = value;
        }

        public int Width
        {
            get => this._width;
            set => this._width = value;
        }

        public List<IBill> getRepository() { return _repository;}

        public void setRepository(List<IBill> repo)
        {
            _repository = repo;
        }

        public void addBill(IBill bill) { _repository.Add(bill); }

        public void removeBill(IBill bill) {
            if (bill == null) throw new NullReferenceException();

            _repository.Remove(bill); 
        }

        public int getSize() {  return _repository.Count(); }
        
    }
}
