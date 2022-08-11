var app = new Vue({
    el: "#root",
    data: {
        listOrders: [],
        proccess : false,
        statusCatalog: [
            {
                Id: 1, Value: "Pending"
            },
            {
                Id: 2, Value: "In Progress"
            },
            {
                Id: 3, Value: "Completed"
            },
            {
                Id: 4, Value: "Delivered"
            },
            {
                Id: 5, Value: "Canceled"
            }  
            ]
    },

    methods: {
        GetList: function () {
            axios.get(url_base + "/Orders/List")
                .then(function (response) {
                    console.log(response.data)
                    app.listOrders = response.data
                    
                })
                .catch(function (error) {
                    console.log(error)
                })
        },
        SetStatus: function (order, isCancel) {
            let OrderStatusId = order.OrderStatusId;
            if (isCancel === 1) {
                OrderStatusId = 5;
                console.log("OrderStatusId:" + OrderStatusId);

            }
            axios.post(url_base + "/Orders/SetOrderStatus",
                {
                    orderId: order.OrderId,
                    orderStatusId: OrderStatusId
                }).then(function (response) {
                    let data = response.data;
                    if (data.Flag) {                        
                        order.OrderStatusId++;
                        if (isCancel == 1) {
                            app.GetList();
                        }
                    }
                    else {
                        Swal.fire({
                            width: 800,
                            position: 'center',
                            icon: 'error',
                            title: data.Message,
                            confirmButtonText: 'Close'
                        })
                    }
                    app.proccess = false;
                }
            );         
        },
        formatStatusBadge: function (OrderStatusId) {
                let result = app.statusCatalog.filter(s => s.Id === OrderStatusId);
                return result[0].Value;
        },
        formatStatusButton: function (OrderStatusId) {
            if (OrderStatusId < 5) {
                let result = app.statusCatalog.filter(s => s.Id === OrderStatusId);
                return result[0].Value;
            }
            else {
                return "Done"
            }
        }
    },
    created() {
        this.GetList();
    }

});