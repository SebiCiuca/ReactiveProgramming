using System;

namespace TheBeginning
{
    public class UdemyConsumer : IObserver<string>
    {
        private bool m_Finished = false;
        private readonly string m_CourseDomain;
        private readonly string m_Name;

        public UdemyConsumer(string name, string courseDomain)
        {
            m_CourseDomain = courseDomain;
            m_Name = name;
        }
        public void OnCompleted()
        {
            if (m_Finished)
            {
                OnError(new Exception("This consumer already finished it's lifecycle"));

                return;
            }

            m_Finished = true;

            Console.WriteLine("<- END");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("<- ERROR");

            Console.WriteLine("<- {0}", error.Message);
        }

        public void OnNext(string value)
        {
            if (m_Finished)
            {
                OnError(new Exception("This consumer finished its lifecycle"));

                return;
            }

            if (value.StartsWith(m_CourseDomain))
            {
                //shows the received message
                Console.WriteLine("{0} enrolled for course {1}", m_Name, value);

                //do something

                //ack the caller
                Console.WriteLine("<- OK");
            }
        }
    }
}
