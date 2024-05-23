using System;
using System.Collections.Concurrent;
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
        //private List<IBill> _repository;
        private ConcurrentBag<IBill> _repository;
        private readonly object _lock = new object();

        public Board(int length, int width)
        {
            this._length = length;
            this._width = width;
            //_repository = new List<IBill>();
            _repository = new ConcurrentBag<IBill>();
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

       // public List<IBill> getRepository() { return _repository;}
        public ConcurrentBag<IBill> getRepository() => _repository;

        public void setRepository(ConcurrentBag<IBill> repo)
        {
            _repository = repo;
        }

        public void addBill(IBill bill) { _repository.Add(bill); }

        public void removeBill(IBill bill) {
            if (bill == null) throw new NullReferenceException();
            ConcurrentBag<IBill> newBag = new ConcurrentBag<IBill>(_repository.Except(new[] { bill }));
            _repository = newBag;
        }

        public int getSize() {  return _repository.Count(); }
       
        public object Lock => _lock;
    }
}
