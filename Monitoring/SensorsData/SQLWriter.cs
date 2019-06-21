using Monitoring.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Monitoring
{
    public class SQLWriter : ISQLWriter
    {
        public void WriteNewSensorsData(SensorsTemp[] sensors)
        {
            // Delete all the records from the tables
            using (var context = new SensorsContext())
            {
                foreach (var elem in context.Sensors)
                    context.Remove(elem);
                context.SaveChangesAsync();
            }
            using (var context = new SensorsContext())
            {
                foreach (var e in sensors)
                {
                    //var e2 = new SensorsTempData()
                    //{
                    //    Id = e.Id,
                    //    CRC = e.CRC,
                    //    LastGet = e.LastGet,
                    //    Mount = e.Mount,
                    //    Number = e.Number,
                    //    Tempreture = e.Tempreture
                    //};
                    //context.SensorsData.Add(e2);
                    context.Sensors.Add(e.CreateSensorTempForBD());
                    //context.Sensors.Add(e);
                }
                context.SaveChangesAsync();
            }
            //new Logger().LogMessage("Write to SQL end");

            //// Select a previously inserted city record,
            //// insert a new person record referencing it,
            //// update a previously inserted person (specify the surname) 
            //using (var context = new Vs2015WinFormsEfcSqliteCodeFirst20170304ExampleContext())
            //{
            //    // Pay attention to the Include(city => city.People) part
            //    // simple context.Cities.Single(city => city.Name == "London"); used instead
            //    // would return the city but its .People list would be null.
            //    // Also make sure to handle cases when there are more or less than one records
            //    // meeting to the request conditions in the production code
            //    var london = context.Cities.Include(city => city.People).Single(city => city.Name == "London");
            //    var peter = new Person { Name = "Peter", City = london };
            //    var john = london.People.Single(person => person.Name == "John");
            //    john.Surname = "Smith";
            //    context.Add(peter);
            //    context.Update(john);
            //    context.SaveChanges();
            //}
        }

        //private void WriteToSQLite()
        //{

        //}
    }
}
