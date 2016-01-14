$(document).ready(function () {

    getAllIngredients();
});

function addIngredientToPizza(elem) {
    
  
    $("#added_ingredients").append("<tr><th>" + elem.name + "</th><th>" + elem.value + "</th><th>" + elem.id + "</th></tr>");

}


function getAllIngredients() {

    if ($('#ingredients_table tr').length > 2)
        clearTable();

    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/ingredients',
        type: 'GET',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#ingredients_table_to_compose").append("<tr><th>" + data[i].name + "</th><th>" + data[i].price + "</th>" +
                    "<th><button onClick=\"addIngredientToPizza(this);\" id=\"" + data[i].id + "\" name=\"" + data[i].name +"\" value=\"" + data[i].price + "\">Dodaj składnik do pizzy</button></th></tr>");
            }
        }
    });

}


