using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Order_Exam.DAL
{
  public class OrderStatusDAL
    {
        public int Insert(ML.OrderStatus orderStatus)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("OrderStatusId", System.Data.SqlDbType.Int) { Value = orderStatus.OrderStatusId });
            parameters.Add(new SqlParameter("Name", System.Data.SqlDbType.NVarChar) { Value = orderStatus.Name });
            parameters.Add(new SqlParameter("Description", System.Data.SqlDbType.NVarChar) { Value = orderStatus.Description});
            parameters.Add(new SqlParameter("Sort", System.Data.SqlDbType.Int) { Value = orderStatus.Sort });
            return ContextDB.ExecuteAction("spInsertOrderStatus", parameters);
        }
        public ML.OrderStatus FindById(int orderStatusId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("OrderStatusId", System.Data.SqlDbType.Int) { Value = orderStatusId });
            return ContextDB.Search<ML.OrderStatus>("spFindtOrderStatusById", parameters);
        }
        public int Update(ML.OrderStatus orderStatus)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("OrderStatusId", System.Data.SqlDbType.Int) { Value = orderStatus.OrderStatusId });
            parameters.Add(new SqlParameter("Name", System.Data.SqlDbType.NVarChar) { Value = orderStatus.Name });
            parameters.Add(new SqlParameter("Description", System.Data.SqlDbType.NVarChar) { Value = orderStatus.Description });
            parameters.Add(new SqlParameter("Sort", System.Data.SqlDbType.Int) { Value = orderStatus.Sort });
            return ContextDB.ExecuteAction("spUpdateOrderStatus", parameters);
        }
        public int DeleteById(int orderStatusId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("OrderStatusId", System.Data.SqlDbType.Int) { Value = orderStatusId });
            return ContextDB.ExecuteAction("spDeleteOrderStatus", parameters);
        }        
        public List<ML.OrderStatus> List()
        {
            return ContextDB.List<ML.OrderStatus>("spListOrderStatus");
        }
    }
}
