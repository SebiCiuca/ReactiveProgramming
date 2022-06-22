using System;
using System.Reactive.Subjects;

namespace ReactiveProgramming
{   
    public static class BehaviorSubjectExample
    {
       public  static void BehaviorSubject()
        {
            var sensorReading = new BehaviorSubject<double>(1.0);

            //step 4
            sensorReading.OnCompleted();
            //step 2
            sensorReading.OnNext(0.99);
            //step 1
            sensorReading.Subscribe(x => Console.WriteLine($"Sensor Value {x}"));
            //step 3
            sensorReading.OnNext(0.98);
        }
    }
}
