using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Monitoring
{
     public class Sensors1Wire : ITypeOfSensors
    {
        private Dictionary<string, int> Dict { get; set; }
        private SensorsTemp[] Sensors { get; set; }

        private TextWriter TextWriter { get; set; }
        private ISQLWriter SQLWriter { get; set; }
        private IRequester Requester { get; set; }

        public Sensors1Wire(TextWriter textWriter, ISQLWriter sqlWriter, IRequester requester)
        {
            this.TextWriter = textWriter;
            this.SQLWriter = sqlWriter;
            this.Requester = requester;

            Dict = new Dictionary<string, int>();
            var index = File.ReadAllLines(@"Resources\indexofallsensors.txt");
            Sensors = new SensorsTemp[index.Length];
            for (var i = 0; i < index.Length; i++)
            {
                var tempString = index[i].Split("\t");
                var sensorsId = int.Parse(tempString[0]) - 1;
                Sensors[sensorsId] = new SensorsTemp(i + 1, tempString[1], tempString[2]);
                Dict.Add(tempString[1], sensorsId);
            }
        }

        public void Run()
        {
            while(true)
            {
                TextWriter.Write("Count of sensors {0}/{1} \n", Requester.GetCountOfSensors(), Sensors.Length);
                TextWriter.Write("Request started \n");
                for (var i = 0; i < Sensors.Length; i++)
                {
                    Sensors[i] = Requester.ReadData(Sensors[i]);
                    TextWriter.Write(Sensors[i].ToString());
                }
                TextWriter.Write("Request ended \n");
                SQLWriter.WriteNewSensorsData(Sensors);
                TextWriter.Write("Write to DB end \n");
            }
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (var e in Sensors)
                str.Append(e.ToString() + " \n");
            return str.ToString();
        }
    }
}
