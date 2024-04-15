namespace Data
{
    public class Bill
    {
        private double _weight;
        private double _radius;
        private double _x;
        private double _y;

        public Bill(double wieght, double radius, double _x, double _y)
        {
            this._radius = radius;
            this._weight = wieght;
            this._x = _x; 
            this._y = _y;
        }
        public double getWeight() {  return _weight; }
        public double getRadius() { return _radius; }
        public double getX() { return _x; }
        public double getY() { return _y;}

        public void setX(double x) { _x = x; }

        public void setY(double y) { _y = y; }

    }
}
