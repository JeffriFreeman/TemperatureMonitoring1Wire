namespace Monitoring
{
    public interface ISQLWriter
    {
        void WriteNewSensorsData(SensorsTemp[] sensors);
    }
}