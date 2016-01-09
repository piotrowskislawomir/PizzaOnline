
function addIngredient() {


    var ingredient = {
        name: $("#ingredient_name").val(),
        price: parseFloat($("#ingredient_price").val())
    };

    var ingedientJson = JSON.stringify(ingredient);
    alert(ingedientJson);
    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/ingredient',
        type: 'POST',
        data: ingedientJson,
        success: function () { alert('PUT completed'); }
    });
}


function refreshIngredientsList() {
    {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: 'http://localhost:5413/api/ingredient',
            data: '[{"name":skl,"price":10}]',
            processData: true,
            dataType: "json",
            success: function () { alert('PUT completed'); }
           
        });
    }
}
