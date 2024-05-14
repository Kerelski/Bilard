using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data
{
    public class Bill : IBill
    {
        private int _id;
        private double _weight;
        private int _diameter;
        private double _x;
        private double _y;
        private double _speed;
        private double _angle; //od 0 do 6.28 (2*pi)
        private int[] _color;

        public Bill(int id, double weight, int diameter, double x, double y, double angle, double speed)
        {
            this._id = id;
            this._diameter = diameter;
            this._weight = weight;
            this._x = x; 
            this._y = y;
            this._angle = angle;
            this._color = new int[3];
            this._speed = speed;
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
        
        public int Diameter
        {
            get => this._diameter;
            set 
            {
                if (value < 0) throw new InvalidDataException();
                this._diameter = value;
            } 

        }

        public double Speed
        {
            get => _speed;
            set => _speed = value;
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
        
        public double Angle
        {
            get => _angle;
            set => _angle = value;
        }

      
    }
}
