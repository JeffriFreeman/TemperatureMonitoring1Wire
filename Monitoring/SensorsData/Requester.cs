using Renci.SshNet;
using System;
using System.Linq;

namespace Monitoring
{
    public class Requester : IRequester
    {
        private readonly PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo("10.0.68.20", "pi", "pass2025");
        public int GetCountOfSensors()
        {
            try
            {
                using (var client = new SshClient(connectionInfo))
                {
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var result = client.RunCommand("cat /sys/devices/w1_bus_master1/w1_master_slaves");
                            var stringResult = result.Result;
                            var keyId = stringResult.Split("\n").Where(item => item.StartsWith("28")).ToArray();
                            return keyId.Length;
                        }
                        else
                        {
                            return 0;
                            //new Logger().LogMessage("SSH connection NOTactive");
                        }
                    }
                }
            }
            catch
            {
                return 0;
                //new Logger().LogMessage("Error " + exception.Message);
            }
        }

        public SensorsTemp ReadData(SensorsTemp sensorsTemp)
        {
            try
            {
                using (var client = new SshClient(connectionInfo))
                {
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var request = "cat /sys/devices/w1_bus_master1/" + sensorsTemp.Id + "/w1_slave";
                            var tempString = client.RunCommand(request);
                            if (tempString.Result == "")
                            {
                                sensorsTemp.SetToBad("No sensor");
                                return sensorsTemp;
                            }
                            else
                            {
                                var resultTempString = tempString.Result.AsSpan();
                                var crc = resultTempString
                                    .Slice(29, 10)
                                    .ToString()
                                    .Split(" ");
                                var temp = resultTempString
                                    .Slice(67, 7)
                                    .ToString()
                                    .Split("=");
                                var test = crc[1] == "YES" ? true : false;
                                sensorsTemp.Update(Int32.Parse(temp[1]), crc[1] == "YES" ? true : false);
                                return sensorsTemp;
                                //new Logger().Log(new SensorsTemp[] { sensor });
                            }
                        }
                        else
                        {
                            sensorsTemp.SetToBad("SSH not connect");
                            return sensorsTemp;
                            //new Logger().LogMessage("SSH connection NOTactive");
                        }
                    }
                }
            }
            catch 
            {
                sensorsTemp.SetToBad("Request error");
                return sensorsTemp;
                //new Logger().LogMessage("Error " + exception.Message);
            }
        }

        public SensorsTemp[] ReadDataOfAllSenors()
        {
            throw new System.NotImplementedException();
        }
    }
}