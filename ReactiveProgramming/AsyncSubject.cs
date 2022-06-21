using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveProgramming
{
    public static class AsyncSubject
    {
        public static void AsyncSubjectExample()
        {
            var sensonr = new AsyncSubject<double>();

            sensonr.Inspect("async");

            sensonr.OnNext(1.0);
            sensonr.OnNext(2.0);
            sensonr.OnNext(3.0);

            sensonr.OnCompleted();

            sensonr.OnNext(4.0);
        }
    }
}
