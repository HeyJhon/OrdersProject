var app = new Vue({
    el: "#root",
    data: { 
        listStatus:[]
    },
    methods: {
        GetList: function()
        {
            console.log(url_base)
            axios.get(url_base + "/List")
                .then(function (response) {
                    console.log(response.data)
                    app.listStatus = response.data
                })
                .catch(function (error){
                    console.log(error)
                })                
        }
    },
    created() {
        this.GetList();
    }

});