﻿namespace Data
{
    public interface IBill
    {
        int Id { get; set; }
        string Color { get; }
        double Weight { get; set; }
        double Diameter { get; set; }
        double X { get; set; }
        double Y { get; set; }
        double Angle { get; set; }
    }
}
