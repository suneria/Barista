namespace Stock
{
    public class ListedStock
    {
        public Market Market { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public enum Market
    {
        KOSPI,
        KOSDAQ
    }
}
