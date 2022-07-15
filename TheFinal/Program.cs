using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TheFinal
{
    internal class Program
    {
        static MarketModel euMarket;
        static MarketModel usMarket;
        static void Main(string[] args)
        {
            //we generate some random comapanies
            List<CompanyModel> euCompanies = new List<CompanyModel>();
            List<CompanyModel> usCompanies = new List<CompanyModel>();

            //generate companies
            for (int i = 0; i < 10; i++)
            {
                var newCompany = GenerateCompany(i);

                if (newCompany.Region == RegionEnum.EU)
                {
                    euCompanies.Add(newCompany);

                    continue;
                }

                usCompanies.Add(newCompany);
            }

            //we create our markets
            usMarket = new MarketModel("Us market", usCompanies);
            euMarket = new MarketModel("Eu market", euCompanies);

            //we create our all orders observable 
            var order = new Subject<OrderModel>();

            //we create orders subject (observers) for each market
            var usMarketOrder = new Subject<OrderModel>();
            var euMarketOrder = new Subject<OrderModel>();

            //we filter from all orders those for us and only subscribe the us observer to the us market order
            order
                .Where(o => o.Company.Region == RegionEnum.US)
                .Subscribe(usMarketOrder);
            order
                .Where(o => o.Company.Region == RegionEnum.EU)
                .Subscribe(euMarketOrder);

            //when a new order for US is posted we will Process it
            usMarketOrder.Inspect("US Market",
                o => ProcessOrder(o,RegionEnum.US),
                ex => Console.WriteLine($"US market has generated exception {ex.Message}"),
                () => Console.WriteLine($"US market has completed"));

            //when a new order for EU is posted we will Process it
            euMarketOrder.Inspect("EU Market",
                o => ProcessOrder(o, RegionEnum.EU),
                ex => Console.WriteLine($"EU market has generated exception {ex.Message}"),
                () => Console.WriteLine($"EU market has completed"));

            //We generate 30 orders
            var random = new Random();
            for (int i = 0; i < 30; i++)
            {
                var companyId = random.Next(0,9);
                order.OnNext(new OrderModel
                {
                    Company = GetCompany(companyId),
                    OrderID = i,
                    OrderValue = random.NextDouble() * 100
                });
            }

            order.OnCompleted();
        }

        public static CompanyModel GenerateCompany(int i)
        {
            var region = i > 5 ? RegionEnum.EU : RegionEnum.US;

            return new CompanyModel(i, $"Comapany_{i}", 0, region);
        }

        public static CompanyModel GetCompany(int i)
        {
            if(i >= 10)
            {
                i -= 10;
            }

            if(i > 5)
            {
                return euMarket.Companies.FirstOrDefault(c => c.CompanyId == i);
            }else
            {
                return usMarket.Companies.FirstOrDefault(c => c.CompanyId == i);
            }
        }

        private static void ProcessOrder(OrderModel order, RegionEnum region)
        {
            Console.WriteLine(order);
            
            if (region == RegionEnum.EU)
            {
                euMarket.ProcessOrder(order.Company.CompanyId, order.OrderValue);
            }

            if (region == RegionEnum.US)
            {
                usMarket.ProcessOrder(order.Company.CompanyId, order.OrderValue);
            }
        }
    }
}
