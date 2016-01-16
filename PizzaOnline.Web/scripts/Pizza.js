$(document).ready(function () {
   
    getAllPizzas();
});


function clearTable() {
    $("#ingredients_table tr:gt(2)").detach();
}

function getAllPizzas() {
    clearTable();

    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/pizza',
        type: 'GET',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var ingredients = "";
                for (var j = 0; j < data[i].toppings.length; j++) { ingredients += " " + data[i].toppings[j].name; }

                $("#pizzas").append("<tr><th title=\"Składniki wybranej pizzy: " + ingredients + "\"> " + data[i].name + "</br><font size=\"1\">" + ingredients + "</font></th><th>" + data[i].price.toFixed(2) + "</th>" +
         "<th> <button type=\"button\" id=\"" + data[i].id + "\" onclick=\"deletePizza(this)\">Usuń</th></tr>");

            }
        }
    });



}