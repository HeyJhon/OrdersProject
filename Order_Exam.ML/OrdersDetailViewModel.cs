using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Exam.ML
{
   public class OrdersDetailViewModel
    {
        public long OrderId { get; set; }
        public string OrderIdFormat { get { return OrderId.ToString("D6"); } }
        public int OrderStatusId { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}
