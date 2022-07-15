using System.Reactive.Subjects;

namespace ReactiveProgramming
{
    public static class AsyncSubject
    {
        public static void AsyncSubjectExample()
        {
            //will return only one value ( last value )
            var sensor = new AsyncSubject<double>();

            //we subscribe to sensor here
            sensor.Inspect("async");

            sensor.OnNext(1.0);
            sensor.OnNext(2.0);
            sensor.OnNext(3.0);

            sensor.OnCompleted();

            sensor.OnNext(4.0);

            //sensor.OnCompleted();
        }
    }
}
