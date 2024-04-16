using System.Runtime.CompilerServices;

namespace Data
{
    public class Bill
    {
        private int _id;
        private double _weight;
        private int _radius;
        private double _x;
        private double _y;
        private double _angle; //od 0 do 6.28 (2*pi)
        private int[] _color;

        public Bill(int id, double wieght, int radius, double _x, double _y, double angle)
        {
            this._id = id;
            this._radius = radius;
            this._weight = wieght;
            this._x = _x; 
            this._y = _y;
            this._angle = angle;
            this._color = new int[3];
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
        
        public int Radius
        {
            get => this._radius;
            set => this._radius = value;

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
