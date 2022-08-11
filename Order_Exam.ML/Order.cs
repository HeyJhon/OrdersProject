using System;
namespace Order_Exam.ML
{
  public class Order
    {
		public long OrderId { get; set; }
		public DateTime DateTransaction { get; set; }
		public int CustomerId { get; set; }
		public int UserId { get; set; }
		public decimal Total { get; set; }
		public short State { get; set; }
		public short OrderStatusId { get; set; }
	}
}
