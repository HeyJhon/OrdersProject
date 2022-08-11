using System.Collections.Generic;
using System.Data.SqlClient;

namespace Order_Exam.DAL
{
    public class ProductDAL
    {  
        public List<ML.Product> List()
        {
            return ContextDB.List<ML.Product>("spListProduct");
        }
    }
}
