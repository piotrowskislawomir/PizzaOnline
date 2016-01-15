var addedItems = [];
var cost = 0;
var ingredientsObj = [];

$(document).ready(function () {

    getAllIngredientsToCompose();
});

function addIngredientToPizza(elem) {
    
  
  //  $("#added_ingredients").append("<tr><th>" + elem.name + "</th><th>" + elem.value + "</th><th>" + elem.id + "</th></tr>");
    
    $("#added_ingredients").append("<tr><th>" + elem.name + "</th><th>" + elem.value + "</th><th> <input type=\"checkbox\" onclick=\"checkedIngredient(this)\" name=\"ingredient_chbox\" value=\"" + elem.id + "\"></th></tr>");
  
   
  
}

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



    for (var i = 0; i < addedItems.length; i++) {
      //  alert(addedItems[i]);

    }
   

}



function calculatePizzaCost(price) {
    cost = Math.round((cost + price) * 100) / 100;
    $("#total_new_pizza_cost").empty();
    $("#total_new_pizza_cost").append(cost.toString());
}


function addNewPizzaToMenu() {
    /*
    $("#ingredients_table_to_compose").children().each(
                  function () {
                      var str = $(this).attr('id');

                      var res = str.split("_");
                      var id = res[0];
                      var price = res[1];


                      sumOrder.push(price);
                      pizzas.push(id);
                  });
    */
    checkChooseIngredients();
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
                $("#ingredients_table_to_compose").append("<tr><th>" + data[i].name + "</th><th>" + data[i].price + "</th>" +
                    "<th> <input type=\"checkbox\" name=\"" + data[i].price + "\" id=\"" + data[i].id + "\" onclick=\"checkedIngredient(this)\"></th></tr>");
            }
        }
    });
  
}


