using System;
using System.Reactive.Subjects;

namespace ReactiveProgramming
{   
    public static class BehaviorSubjectExample
    {
       public  static void BehaviorSubject()
        {
            // initalize a subject with default value
            var sensorReading = new BehaviorSubject<double>(1.0);

            //step 4 if OnComplete is called nothing is sent to subscriber.
            //sensorReading.OnCompleted();

            //step 2 -> if new value is passed it will return this value
            //sensorReading.OnNext(0.99);

            //step 1 -> if only subscribe it returns default value
            sensorReading.Subscribe(x => Console.WriteLine($"Sensor Value {x}"));

            //step 3 -> if other value is passed it will return both values
            //sensorReading.OnNext(0.98);
        }
    }
}
