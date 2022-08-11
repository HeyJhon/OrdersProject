using System.Collections.Generic;
using System.Data.SqlClient;

namespace Order_Exam.DAL
{
   public class OrderDAL
    {
        public List<ML.OrderViewModel> List()
        {
            return ContextDB.List<ML.OrderViewModel>("spListOrders");
        }
        public int Insert(ML.Order order)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("CustomerId", System.Data.SqlDbType.Int) { Value = order.CustomerId });
            parameters.Add(new SqlParameter("UserId", System.Data.SqlDbType.Int) { Value = order.UserId });
            return ContextDB.ExecuteAction("spInsertOrder", parameters);
        }
        public int SetOrderStatus(int orderId, int StatusStart, int StatusEnd)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("StatusStart", System.Data.SqlDbType.Int) { Value = StatusStart });
            parameters.Add(new SqlParameter("StatusEnd", System.Data.SqlDbType.Int) { Value = StatusEnd });
            parameters.Add(new SqlParameter("OrderId", System.Data.SqlDbType.BigInt) { Value = orderId });
            return ContextDB.ExecuteAction("spSetOrderStatus", parameters);
        }
    }
}
