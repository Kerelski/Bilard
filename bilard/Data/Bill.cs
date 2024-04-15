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

        public Bill(int id, double wieght, int radius, double _x, double _y, double angle)
        {
            this._id = id;
            this._radius = radius;
            this._weight = wieght;
            this._x = _x; 
            this._y = _y;
            this._angle = angle;
        }
        public int getId() { return _id; }
        public double getWeight() {  return _weight; }
        public int getRadius() { return _radius; }
        public double getX() { return _x; }
        public double getY() { return _y;}

        public double getAngle() { return _angle;}

        public void setX(double x) { _x = x; }
        public void setY(double y) { _y = y; }
        public void setAngle(double angle) { _angle = angle; }

    }
}
