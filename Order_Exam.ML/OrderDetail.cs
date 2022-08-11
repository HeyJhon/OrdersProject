namespace Order_Exam.ML
{
   public class OrderDetail
    {
        public long OrderDetailId { get; set; }
        public string ProductSku { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public long OrderId { get; set; }
    }
}
