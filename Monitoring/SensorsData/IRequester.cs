using System.IO;

namespace Monitoring
{
    public interface IRequester
    {
        SensorsTemp[] ReadDataOfAllSenors();
        SensorsTemp ReadData(SensorsTemp sensorsTemp);
        int GetCountOfSensors();
    }
}