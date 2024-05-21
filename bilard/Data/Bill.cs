using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data
{
    public class Bill : IBill
    {
        private int _id;
        private double _weight;
        private double _diameter;
        private double _x;
        private double _y;
        private double _speedX;
        private double _speedY;
       
        private int[] _color;
        public event NotifyDelegateBill.NotifyBill? OnChange;
        private bool _isMoving = true;

        public Bill(int id, double weight, int diameter, double x, double y, double speedX, double speedY)
        {
            this._id = id;
            this._diameter = diameter;
            this._weight = weight;
            this._x = x; 
            this._y = y;
            this._color = new int[3];
            this._speedX = speedX;
            this._speedY = speedY;
            var rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                _color[i] = rand.Next(0, 255);
            }
        }
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Color
        {
            get
            {
                string colorHex = $"#{_color[0].ToString("X2")}{_color[1].ToString("X2")}{_color[2].ToString("X2")}";
                return colorHex;
            }
        }
        public double Weight
        {
            get => _weight; 
            set => _weight = value;
        }
        
        public double Diameter
        {
            get => this._diameter;
            set 
            {
                this._diameter = value;
            } 

        }

        public double SpeedX
        {
            get => _speedX;
            set => _speedX = value;
        }
        public double SpeedY
        {
            get=> _speedY;
            set => _speedY = value;
        }
        public double X
        {
            get => (double)_x;
            set => _x = value;

        }

        public double Y
        {
            get => (double)_y;
            set => _y = value;
        }
        
   

        public bool IsMoving
        {
            get=> _isMoving;
            set => _isMoving = value;
        }

        public void MoveAsync(Barrier barrier)
        {
            barrier.AddParticipant();
            Task.Run(() => Move(barrier));
        }

        private void Move(Barrier barrier)
        {
            while(IsMoving)
            {
                OnChange?.Invoke(this);
                barrier.SignalAndWait();
            }
           barrier.RemoveParticipant();
        }
    }
}
