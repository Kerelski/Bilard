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
            _isDeleteEnable = true;
            _isClearEnable = true;
            _GameModel.AddBill();
            _timer.Start();
        }

        public void Delete()
        {
            _GameModel.DeleteBill();
            OnPropertyChanged("Bills");
        }

        public void ClearAll()
        {
            _GameModel.ClearBoard();
            OnPropertyChanged("Bills");
        }

        public Bill[]? Bills
        {
            get => _GameModel.GetBills().ToArray();
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
