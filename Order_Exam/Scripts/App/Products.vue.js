var app = new Vue({
    el: "#root",
    data: {
        listProducts: [],
        order: {
            CustomerId: 1,
            UserId: 1
        },
        proccess: false,

    },
    computed: {
        totalPrice: function () {
            let sum = 0;
            this.listProducts.forEach(function (item) {
                sum += item.Price * item.Quantity;
            });
            return sum;
        }
    },
    methods: {
        GetList: function () {
            console.log(url_base)
            axios.get(url_base + "/Products/List")
                .then(function (response) {
                    console.log(response.data)
                    app.listProducts = response.data
                })
                .catch(function (error) {
                    console.log(error)
                })
        },

        GetImageDetail: function (url) {
            Swal.fire({
                width: 800,
                imageUrl: url,
                imageWidth: 800,
                confirmButtonText: 'Close'
            })
        },

        Add: function (index) {
            let product = app.listProducts[index];
            if (product.Stock < 1) return;
            product.Quantity += 1;
            product.Stock -= 1;
        },

        Substract: function (index) {
            let product = app.listProducts[index];
            if (product.Cantidad < 1) return;
            product.Quantity -= 1;
            product.Stock += 1;
        },

        CreateOrder: function (event) {

            event.preventDefault()
            if (app.proccess) return;
            app.proccess = true;

            axios.post(url_base + "/Orders/CreateOrder",
                {
                    order: app.order,
                    products: app.listProducts
                }).then(function (response) {
                    let data = response.data;
                    if (data.Flag) {
                        app.ResetItems();
                        Swal.fire({
                            width: 800,
                            position: 'center',
                            icon: 'success',                            
                            title: data.Message,
                            confirmButtonText: 'Accept'
                        });
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

        ResetItems: function () {
            app.GetList();
            app.order.CustomerId = 1
            app.order.UserId = 1
        },
    },
    
    created() {
        this.GetList();
    }

});