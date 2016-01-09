$(document).ready(function() {

$("#add_ingredient").click(
    function(event) {
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
            success: function() {
              //  alert('POST OK'); 
            }
        });
        event.preventDefault();
        $('#ingredient_name').val("");
        $('#ingredient_price').val("");
        getAllIngredients();
    });
});

function getAllIngredients() {
    {
        $.ajax({
            dataType: "json",
            contentType: "application/json",
            url: 'http://localhost:5413/api/ingredients',
            type: 'GET',
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    $(".ingredients_table").append("<tr><td>" + data[i].name + "</td><td>" + data[i].price + "</td></tr>");
                }
            }
        });
    }
}
