$(document).ready(function() {

    $("#add_ingredient").click(
    function (event) {
    var ingredient = {
        name: $("#ingredient_name").val(),
        price: parseFloat($("#ingredient_price").val())
    };

    var ingedientJson = JSON.stringify(ingredient);
    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/ingredient',
        type: 'POST',
        data: ingedientJson,
        success: function () {
          
            getAllIngredients();
        }
    });
      $('#ingredient_name').val("");
      $('#ingredient_price').val("");
      event.preventDefault();
    });
    
    getAllIngredients();
});

function removeIngredient(obj) {

    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/ingredient/' + obj.id,
        type: 'DELETE',
        success: function () {
            alert("USUWANIE");
        }

    });

    getAllIngredients();
    $.blockUI({ message: '<h1>Trwa usuwanie produktu...</h1>' });
    setTimeout($.unblockUI, 4000);
    location.reload();
}

function clearTable() {
  //  $("#ingredients_table tr").empty();
    $("#ingredients_table tr:gt(2)").detach();
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
                    $("#ingredients_table").append("<tr><th>" + data[i].name + "</th><th>" + data[i].price + "</th>" +
                        "<th><button onClick=\"removeIngredient(this);\" class=\"ingredient_remove_btn\" id=" + data[i].id + ">Usuń składnik</button></th></tr>");
                }
            }
        });
   
}


