namespace Data
{
    public interface IBill
    {
        int Id { get; set; }
        string Color { get; }
        double Weight { get; set; }
        double Diameter { get; set; }
        double X { get; set; }
        double Y { get; set; }   
        double SpeedX { get; set; }
        double SpeedY { get; set; }
        bool IsMoving { get; set; }

        event NotifyDelegateBill.NotifyBill? OnChange;
        void MoveAsync(Barrier barrier);
        public object Lock { get; }
    }
}
