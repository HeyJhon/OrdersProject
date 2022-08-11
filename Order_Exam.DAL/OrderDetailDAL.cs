using System.Collections.Generic;
using System.Data.SqlClient;

namespace Order_Exam.DAL
{
    public class OrderDetailDAL
    {
        public int Insert(ML.OrderDetail orderDetail)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("OrderId", System.Data.SqlDbType.Int) { Value = orderDetail.OrderId });
            parameters.Add(new SqlParameter("ProductSku", System.Data.SqlDbType.NVarChar) { Value = orderDetail.ProductSku });
            parameters.Add(new SqlParameter("Quantity", System.Data.SqlDbType.Int) { Value = orderDetail.Quantity });
            return ContextDB.ExecuteAction("spInsertOrderDetail", parameters);
        }
    }
}
