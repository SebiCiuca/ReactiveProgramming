namespace TheFinal
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public CompanyModel Company { get; set; }
        public double OrderValue { get; set; }

        public override string ToString()
        {
            return $"For Company {Company} a new order with OrderID: {OrderID} and value {OrderValue} was generated.";
        }
    }
}
