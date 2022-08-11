using Order_Exam.DAL;
using System.Collections.Generic;

namespace Order_Exam.BLL
{
   public class ProductBLL
    {
        private ProductDAL context;
        public ProductBLL()
        {
            context = new ProductDAL();
        }

        public List<ML.Product> List()
        {
            return context.List();
        }
    }
}
