using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monitoring
{
    public class SensorsTemp
    {
        public SensorsTemp(int number, string id, string mount)
        {
            Number = number;
            Id = id;
            Mount = mount;

            Tempreture = 0;
            CRC = false;
            LastGet = DateTime.Now;
            Status = "Created";
        }

        

        public int Number { get; private set; }
        public string Id { get; private set; }
        public int Tempreture { get; private set; }
        public string Mount { get; private set; }
        public bool CRC { get; private set; }
        public DateTime LastGet { get; private set; }
        [NotMapped]
        public string Status { get; private set; }

        public void Update(int tempreture, bool crc)
        {
            Tempreture = tempreture;
            CRC = crc;
            LastGet = DateTime.Now;
            Status = "Ок";
        }

        public void SetToBad(string message)
        {
            Tempreture = 0;
            CRC = false;
            LastGet = DateTime.Now;
            Status = message;
        }

        private SensorsTemp()
        {
        }

        public SensorsTemp CreateSensorTempForBD() //чтобы не переписывать код в запросах OPC сервера
        {
            return new SensorsTemp()
            {
                Id = this.Id,
                CRC = this.CRC,
                LastGet = this.LastGet,
                Mount = this.Mount,
                Number = this.Number - 1,
                Tempreture = this.Tempreture
            };
        }

        public override string ToString()
        {
            return Number + "\t" + (double)Tempreture / 1000 + "\t" + CRC + "\t" + Mount + "\t" + DateTime.Now + "\t" + Status + " \n";
        }
    }
}
