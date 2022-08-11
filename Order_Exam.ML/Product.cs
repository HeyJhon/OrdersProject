namespace Order_Exam.ML
{
    public class Product
    {
		public string Sku { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Stock { get; set; }
		public short State { get; set; }
		public int StockLimitAlert { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
