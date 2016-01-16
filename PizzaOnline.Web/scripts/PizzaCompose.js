var addedItems = [];
var cost = 0;
var ingredientsObj = [];

$(document).ready(function () {

    getAllIngredientsToCompose();

});


function getItemAsObj(id) {
    for (var i = 0; i < ingredientsObj.length; i++) {
        if (parseInt(id) == parseInt(ingredientsObj[i].id)) {
            return ingredientsObj[i];
        }
    }
}


function checkedIngredient(elem) {

    var item = getItemAsObj(elem.id);
    var price = item.price;
  
    if (elem.checked == true) {
        addedItems.push(elem.id);
        calculatePizzaCost(price);
    }
    else {
        var index = addedItems.indexOf(elem.id);
        addedItems.splice(index, 1);
        calculatePizzaCost(-price);
    }
}


function calculatePizzaCost(price) {
    cost = Math.round((cost + price) * 100) / 100;
    $("#total_new_pizza_cost").empty();
    $("#total_new_pizza_cost").append(cost.toFixed(2).toString());
    $("#pizza_compose_cost").empty();
    $("#pizza_compose_cost").val(cost.toFixed(2));
}


function createPizzaModel() {
    
    var toppingsTable = [];

    for (var i = 0; i < addedItems.length; i++) {
        toppingsTable[i] = { Id: addedItems[i] };
    }

    var pizzaModel = {
        Name: $("#new_pizza_name").val(),
        Price: parseFloat($("#pizza_compose_cost").val()),
        Toppings: toppingsTable
    };

    return pizzaModel;
}

function checkPizzasInMenu() {
    // window.location.replace("")
    alert("tu przekierowanie do listy wszystki pizz");

}


function addNewPizzaToMenu() {

           var pizzaModel = createPizzaModel();
           var pizzaModelJson = JSON.stringify(pizzaModel);

           $.ajax({
               dataType: "json",
               contentType: "application/json",
               url: 'http://localhost:5413/api/pizza',
               type: 'POST',
               data: pizzaModelJson,
               success: function () {
                   window.location.href = "http://localhost:5453/Pizza";
               }
           });
}


function getAllIngredientsToCompose() {

    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/ingredient',
        type: 'GET',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                ingredientsObj[i] = data[i];
                $("#ingredients_table_to_compose").append("<tr><th>" + data[i].name + "</th><th>" + data[i].price.toFixed(2) + "</th>" +
                    "<th> <input type=\"checkbox\" name=\"" + data[i].price + "\" id=\"" + data[i].id + "\" onclick=\"checkedIngredient(this)\"></th></tr>");
            }
        }
    });
  
}


