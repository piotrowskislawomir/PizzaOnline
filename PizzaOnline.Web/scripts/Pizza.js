var allPizzasItems = [];
var controlOrderPizzaItems = [];
var orderPrice = 0;

$(document).ready(function () {

    getAllPizzasToRemove();

});

function removePizza(obj) {

    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/pizza/' + obj.id,
        type: 'DELETE',
        success: function () {
            alert("USUWANIE");
        }

    });

    getAllPizzasToRemove();
    $.blockUI({ message: '<h1>Trwa usuwanie pizzy...</h1>' });
    setTimeout($.unblockUI, 4000);
    location.reload();
}




function getAllPizzasToRemove() {

    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/pizza',
        type: 'GET',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var ingredients = "";
                for (var j = 0; j < data[i].toppings.length; j++) {
                    ingredients += " " + data[i].toppings[j].name;
                }

                $("#all_pizzas_admin_table").append("<tr><th title=\"Składniki wybranej pizzy: " + ingredients + "\"> " + data[i].name + "</br><font size=\"1\">" + ingredients + "</font></th><th>" + data[i].price.toFixed(2) + "</th>" +
                  "<th> <button type=\"button\" id=\"" + data[i].id + "\" onclick=\"removePizza(this)\">Usuń pizze</th></tr>");

            }
        }
    });
}
