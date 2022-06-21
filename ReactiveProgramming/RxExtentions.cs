using System;
using System.Reactive.Linq;

namespace ReactiveProgramming
{
    public static class RxExtentions
    {
        public static IDisposable Inspect<T>(this IObservable<T> self, string name)
        {
            return self.Subscribe(
                x => OnNext(name,x),
                ex => OnError(name,ex),
                () => OnCompleted(name)
            );
        }

        public static void OnNext<T>(string name, T value)
        {
            Console.WriteLine($"{name} has generated value {value}");
        }

        public static void OnError(string name, Exception ex)
        {
            Console.WriteLine($"{name} has generated exception {ex.Message}");
        }

        public static void OnCompleted(string name)
        {
            Console.WriteLine($"{name} has completed");
        }
    }
}
