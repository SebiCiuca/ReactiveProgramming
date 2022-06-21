using System;
using System.Reactive.Subjects;
using System.Threading;

namespace ReactiveProgramming
{
    public static class ReplaySubjectExample
    {
        public static void  TimeReplaySubject()
        {
            // we only let the market open for 0.5 seconds
            var timeWindow = TimeSpan.FromMilliseconds(500);
            var market = new ReplaySubject<double>(timeWindow); //obserable

            market.OnNext(3d);
            Thread.Sleep(200);
            market.OnNext(434d);
            Thread.Sleep(200);
            market.OnNext(23d);
            Thread.Sleep(200);

            market.Subscribe(x => Console.WriteLine($"Market published price {x}"));
        }

        public static void BufferReplaySubject()
        {
            var market = new ReplaySubject<double>(3); //obserable

            market.OnNext(1d);
            Thread.Sleep(200);
            market.OnNext(2d);
            Thread.Sleep(200);
            market.OnNext(3d);
            Thread.Sleep(200);
            market.OnNext(4d);
            Thread.Sleep(200);
            market.OnNext(5d);
            Thread.Sleep(200);
            market.OnNext(6d);

            market.Subscribe(x => Console.WriteLine($"Market published price {x}"));
        }
    }
}
