namespace TheFinal
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public RegionEnum Region { get; set; }

        public CompanyModel(int id, string name, double initialValue, RegionEnum region)
        {
            CompanyId = id;
            Name = name;
            Value = initialValue;
            Region = region;
        }

        public override string ToString()
        {
            return $"CompanyId {CompanyId}, Name {Name}, Current Market Value {Value}, Region {Region}";
        }
    }
}