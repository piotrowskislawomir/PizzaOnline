var allPizzasItems = [];
var controlOrderPizzaItems = [];
var orderPrice = 0;

$(document).ready(function () {

    getAllPizzasToMenu();
    $('[data-toggle="tooltip"]').tooltip();

});


function addPizzaToCard(elem) {

    var choosePizzaitem = allPizzasItems[elem.id];
    controlOrderPizzaItems.push(choosePizzaitem);
    $("#card_ul_id").append('<li class=\"cart-item\" id=\"' + elem.id + '_item\">' + choosePizzaitem.name + "<span class=\"cart-item-price\">" + choosePizzaitem.price.toFixed(2) + "</span><span class=\"cart-item-desc\"><a href=\"#\" id=\"" + elem.id + '\" class=\"cart-button\" onclick=\"removeFromCard(this)\">Usuń</a></span><span class=\"cart-item-price\"></li>');
	 
    returnBill(choosePizzaitem.price);
}

function removeFromCard(elem) {

    var elementToRemoveFromCard = allPizzasItems[elem.id];
    var index = controlOrderPizzaItems.indexOf(elem.id);
    controlOrderPizzaItems.splice(index, 1);
    $("#" + elem.id + "_item").remove();

    returnBill(-elementToRemoveFromCard.price);
}

function orderPizzas() {

    if (orderPrice == 0) {
        alert("Koszyk zamówienia nie może być pusty");
        return;
    }

    var orderAddress = inputOrderAddres();
    var order = "Zamówiłeś pizze o id:";

    for (var i = 0; i < controlOrderPizzaItems.length; i++) {
        order += " " + controlOrderPizzaItems[i].id;
    }

    order += " na łączną kwotę " + orderPrice  + " pod adres: " + orderAddress;

    alert(order);
}

function inputOrderAddres() {

    var address = prompt("Proszę podać adres zamówienia:", "");

    while (address == "") address = prompt("Proszę podać adres zamówienia:", "");
   
    return address;
}

function returnBill(price)
{
    orderPrice = Math.round((orderPrice + price) * 100) / 100;
	
    $("#cart_sum").empty();
        $("#cart_sum").append(orderPrice.toFixed(2).toString());
    
}



function getAllPizzasToMenu() {
  
    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/pizza',
        type: 'GET',
        success: function(data) {
            for (var i = 0; i < data.length; i++) {
                var ingredients = "";
                allPizzasItems[i] = data[i];
                for (var j = 0; j < data[i].toppings.length; j++) { ingredients += " " + data[i].toppings[j].name; }
                console.log(i);
                $("#all_pizzas_table").append("<tr><th title=\"Składniki wybranej pizzy: " + ingredients + "\"> " + data[i].name + "</br><font size=\"1\">" + ingredients + "</font></th><th>" + data[i].price.toFixed(2) + "</th>" +
         "<th> <button type=\"button\" id=\"" + i + "\" onclick=\"addPizzaToCard(this)\">Wybierz</th></tr>");
     
            }
        }
    });

   
   
}