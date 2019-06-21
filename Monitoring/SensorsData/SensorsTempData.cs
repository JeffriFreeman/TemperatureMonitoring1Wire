using System;

namespace Monitoring
{
    public class SensorsTempData
    {
        public int Number { get; set; }
        public string Id { get; set; }
        public int Tempreture { get; set; }
        public string Mount { get; set; }
        public bool CRC { get; set; }
        public DateTime LastGet { get; set; }
    }
}
