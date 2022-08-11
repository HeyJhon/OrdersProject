using Order_Exam.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Exam.BLL
{
    public class OrderSatusBLL
    {
        private OrderStatusDAL context;

        public OrderSatusBLL()
        {
            context = new OrderStatusDAL();
        }
        public int Insert(ML.OrderStatus orderStatus)
        {
            return context.Insert(orderStatus);
        }
        public ML.OrderStatus FindById(int orderStatusId)
        {
            return context.FindById(orderStatusId);
        }
        public int Update(ML.OrderStatus orderStatus)
        {
            return context.Update(orderStatus);
        }
        public int DeleteById(int orderStatusId)
        {
            return context.DeleteById(orderStatusId);
        }
        public List<ML.OrderStatus> List()
        {
            return context.List();
        }
    }
}
