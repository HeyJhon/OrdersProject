using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Order_Exam.BLL
{
    public class OrderBLL
    {
        private DAL.OrderDAL contextOrder;
        public OrderBLL()
        {
            contextOrder = new DAL.OrderDAL();
        }

        public List<ML.OrdersDetailViewModel> List()
        {
            List<ML.OrderViewModel> lista = contextOrder.List();
            List<ML.OrdersDetailViewModel> data = lista.GroupBy(x => new { x.OrderId, x.OrderStatusId })
                .Select(g => new ML.OrdersDetailViewModel
                {
                    OrderId = g.Key.OrderId,
                    OrderStatusId = g.Key.OrderStatusId,
                    Orders = g.Select(o =>new ML.OrderViewModel {
                        OrderId = o.OrderId,
                        //OrderStatusId = o.OrderStatusId,
                        Price = o.Price,
                        ProductName = o.ProductName,
                        ProductSku = o.ProductSku,
                        Quantity = o.Quantity,
                        SubTotal = o.SubTotal
                    }).ToList()
                }).ToList();

            return data;
        }

        public ML.Result Insert(ML.Order order, List<ML.Product> products)
        {
            ML.Result result = GetResultInit();
            int transactionId = 0;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    transactionId = contextOrder.Insert(order);
                    if (transactionId <= 0)
                    {
                        result.Message = "No se pudo generar la orden";
                        return result;
                    }

                    bool flagProduct = false;
                    products = products.Where(p => p.Quantity > 0).ToList();

                    foreach (var product in products)
                    {
                        int insertDetail = InsertOrderDetail(transactionId, product);

                        if (insertDetail <= 0)
                        {
                            result.Message = $"No se pudo guardar el producto: {product.Name}";
                            return result;
                        }
                        else
                            flagProduct = true;
                    }

                    if (flagProduct)
                    {
                        scope.Complete();
                        result = GetResultOk(flagProduct,transactionId,order,products, result);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Hubo un fallo en el servidor." + ex.Message;
            }

            return result;
        }

        private ML.Result GetResultInit()
        {
            return new ML.Result()
            {
                Data = null,
                Message = "Ocurrio un error",
                Flag = false
            };
        }

        public ML.Result SetOrderStatus(int orderId, int orderStatusId)
        {
            ML.Result result = GetResultInit();
            int orderStatusEnd = orderStatusId + 1;
            if (orderStatusEnd > 5)
                orderStatusEnd = 5;

            int change = contextOrder.SetOrderStatus(orderId, orderStatusId, orderStatusEnd);

            if(change > 0)
            {
                result.Flag = true;
                result.Message = "Se actualizó estatus correctamente";
            }
            return result;
        }
        private int InsertOrderDetail(int transactionId, ML.Product product)
        {
            ML.OrderDetail detail = new ML.OrderDetail()
            {
                OrderId = transactionId,
                ProductSku = product.Sku,
                Quantity = product.Quantity,

            };
            return new DAL.OrderDetailDAL().Insert(detail);
        }
        private ML.Result GetResultOk(bool flagProduct,int transactionId,  ML.Order order, List<ML.Product> products, ML.Result result)
        {
            result.Flag = flagProduct;
            result.Message = $"El pedido  #{transactionId:D6} se generó correctamente";

            result.Data = new
            {
                IdTransaccion = transactionId.ToString("D6"),
                order.CustomerId,
                order.UserId,
                productList = products,
                Total = products.Sum(p => p.Quantity * p.Price).ToString("N2")
            };

            return result;
        }

    }
}
