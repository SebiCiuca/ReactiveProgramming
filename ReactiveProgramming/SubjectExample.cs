using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReactiveProgramming
{
    public static class SubjectExample
    {
        public static void RunSubjectExample()
        {
            var market = new Subject<double>(); //obserable
            var usMarketConsumer = new Subject<double>(); //observer
            var europeMarketConsumer = new Subject<double>(); //observer of 'market' and obserable for inspect

            market
                //observable decide what values should be subscribed for this observer
                //only values over 50 should be "sent" to usMarket 
                //basically usMarket has no clue if values under 50 were ever on the market
                .Where(marketValue => marketValue > 50)
                .Subscribe(usMarketConsumer);

            market
                .Where(marketValue => marketValue < 50)
                .Subscribe(europeMarketConsumer);

            usMarketConsumer.Inspect("US Market");
            europeMarketConsumer.Inspect("EU Market");

            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                var marketValue = random.NextDouble() * 100;

                market.OnNext(marketValue);
            }

        }
    }
}
