
function getAllOrders() {

    var Order1 =
    {
        
    };

    var Order2 =
    {

    };

    var Order3 =
    {

    };




    for (var i = 0; i < data.length; i++) {
        $("#ingredients_table").append("<tr><th>" + data[i].name + "</th><th>" + data[i].price.toFixed(2) + "</th>" +
            "<th><button onClick=\"removeIngredient(this);\" class=\"ingredient_remove_btn\" id=" + data[i].id + ">Usuń składnik</button></th></tr>");
    }
   
    /*
    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/ingredient',
        type: 'GET',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#ingredients_table").append("<tr><th>" + data[i].name + "</th><th>" + data[i].price.toFixed(2) + "</th>" +
                    "<th><button onClick=\"removeIngredient(this);\" class=\"ingredient_remove_btn\" id=" + data[i].id + ">Usuń składnik</button></th></tr>");
            }
        }
    });*/

}