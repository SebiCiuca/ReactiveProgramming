using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TheBeginning
{
    public class CoursesProducer : IObservable<string>, IDisposable
    {
        private readonly List<IObserver<string>> m_Subscribers = new List<IObserver<string>>();

        //the cancellation token source for starting stopping
        //inner observable working thread

        private readonly CancellationTokenSource m_CancellationSource;

        //the cancellation flag

        private readonly CancellationToken m_CancellationToken;

        //the running task that runs the inner running thread

        private readonly Task m_WorkerTask;

        public CoursesProducer()
        {
            m_CancellationSource = new CancellationTokenSource();
            m_CancellationToken = m_CancellationSource.Token;

            m_WorkerTask = Task.Factory.StartNew(GenerateCourse, m_CancellationToken);
        }

        public void Dispose()
        {
            if (!m_CancellationSource.IsCancellationRequested)

            {

                m_CancellationSource.Cancel();

                while (!m_WorkerTask.IsCanceled)

                    Thread.Sleep(100);

            }

            m_CancellationSource.Dispose();
            m_WorkerTask.Dispose();

            foreach (var observer in m_Subscribers)
                observer.OnCompleted();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (m_Subscribers.Contains(observer))
            {
                throw new ArgumentException("This observer is already subscribed");
            }

            Console.WriteLine("Subscribing for {0}", observer.GetHashCode());

            m_Subscribers.Add(observer);

            return null;
        }
        public void Wait()
        {
            while (!(m_WorkerTask.IsCompleted || m_WorkerTask.IsCanceled))
                Thread.Sleep(100);
        }

        private void GenerateCourse()
        {
            while (!m_CancellationToken.IsCancellationRequested)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                if (input.Equals("Exit"))
                {
                    m_CancellationSource.Cancel();

                    break;
                }

                foreach (var subscriber in m_Subscribers)
                {
                    if (input.Equals("Error"))
                    {
                        subscriber.OnError(new Exception("Error from observable"));
                    }
                    else
                    {
                        subscriber.OnNext(input);
                    }

                }

                m_CancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}
