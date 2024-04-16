using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Resolvers;
using Data;
using System.Windows.Threading;
using Model;
using System.Collections;

namespace ViewModel
{
    public class GUIController : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            {
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }


        public ICommand _add { get; }
        public ICommand _delete { get; }
        public ICommand _clear { get; }
        private GameModel _GameModel;
        private DispatcherTimer _timer;
        private int _length = 600;
        private int _width = 1000;
        private bool _isAddEnable;
        private bool _isDeleteEnable;
        private bool _isClearEnable;

        public GUIController()
        {
            _add = new RelayCommand(Add);
            _delete = new RelayCommand(Delete);
            _clear = new RelayCommand(ClearAll);
            _GameModel = new GameModel(_length, _width);
            _isAddEnable = true;
            _isDeleteEnable = false;
            _isClearEnable = false;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += _timer_Tick;
            
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _GameModel.UpdatePosition();
           
            OnPropertyChanged("Bills");
        }

        public void Add()
        {
            IsDeleteEnable = true;
            IsClearEnable = true;
            _GameModel.AddBill();
            _timer.Start();
        }

        public void Delete()
        {
            if (_GameModel.GetSize() > 1)
            {
                _GameModel.DeleteBill();
            }
            else if(_GameModel.GetSize() == 1)
            {
                IsClearEnable = false;
                IsDeleteEnable = false;
                _GameModel.DeleteBill();
                
            }
            else
            {
        
                return;
            }
            OnPropertyChanged("Bills");
        }

        public void ClearAll()
        {
            if (_GameModel.GetSize() > 0) 
            {
                _GameModel.ClearBoard();
                IsClearEnable = false;
                IsDeleteEnable = false;
            }
            else
            {
                
                return;
            };
            
            OnPropertyChanged("Bills");
        }

        public Bill[]? Bills
        {
            get => _GameModel.GetBills().ToArray();
        }

        public int GetBoardLength
        {
            get => _GameModel.GetLength();
        }

        public int GetBoardWidth
        {
            get => _GameModel.GetWidth();
        }

        public bool IsAddEnable
        {
            get => _isAddEnable;
            set {
                _isAddEnable = value;
                OnPropertyChanged();
            } 

        }
        public bool IsDeleteEnable
        {
            get => _isDeleteEnable;
            set
            {
                _isDeleteEnable = value;
                OnPropertyChanged();
            }

        }

        public bool IsClearEnable
        {
            get => _isClearEnable;
            set
            {
                _isClearEnable = value;
                OnPropertyChanged();
            }

        }
    }
}
