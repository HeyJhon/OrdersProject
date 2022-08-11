using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Exam.ML
{
    public class OrderViewModel
    {
		public long OrderId { get; set; }
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public short OrderStatusId { get; set; }
	}
}
