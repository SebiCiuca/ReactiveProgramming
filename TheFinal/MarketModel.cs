using System;
using System.Collections.Generic;
using System.Linq;

namespace TheFinal
{
    public class MarketModel
    {
        public MarketModel(string name, List<CompanyModel> companies)
        {
            Name = name;
            Companies = companies;
        }
        public string Name { get; set; }
        public List<CompanyModel> Companies { get; set; }

        public void ProcessOrder(int companyId, double orderValue)
        {
            var company = Companies.FirstOrDefault(c => c.CompanyId == companyId);

            company.Value += orderValue;

            Console.WriteLine(company);
        }
    }
}
