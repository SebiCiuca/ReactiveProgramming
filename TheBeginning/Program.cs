using System;

namespace TheBeginning
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Add courses and see people enrollments!");

            using (var trainer = new CoursesProducer())
            using (var netConsumer = trainer.Subscribe(new UdemyConsumer(".NET Consummer", ".NET")))
            using (var javaConsumer = trainer.Subscribe(new UdemyConsumer("Java Consumer", "Java")))
            using (var reactConsumer = trainer.Subscribe(new UdemyConsumer("React Consumer", "React")))
            using (var allClasssesConsumer = trainer.Subscribe(new UdemyConsumer("Junky consumer", string.Empty)))

                trainer.Wait();


            Console.WriteLine("END");
            Console.ReadLine();

        }
    }
}
