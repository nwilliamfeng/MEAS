using System;


namespace MEAS
{
    public sealed class Environment:Entity
    {
        public DateTime Time { get; set; }

        public double Humidity { get; set; }

        public double Temperature { get; set; }

        public string Address { get; set; }
    }
}
