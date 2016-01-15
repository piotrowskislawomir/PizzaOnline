

var noCheatVar = 0;

function Dodaj(elem)
{
	var i;
	//alert($(".cart-top"));
      $("#card_ul_id").append( '<li class=\"cart-item\" id=\"'+elem.id+'_item\">' + elem.name + "<span class=\"cart-item-price\">" + elem.value + "</span><span class=\"cart-item-desc\"><a href=\"#\" id=\"" + elem.id + '_'+elem.value+'\" class=\"cart-button\" onclick=\"removeFromCard(this)\">Usuń</a></span><span class=\"cart-item-price\"></li>');
	 
	returnBill(elem.value);

	var jSON = LoadFakeData();
    convertJSonToList(jSON);
}

function removeFromCard(elem)
{
	var str = elem.id;
	var res = str.split("_");
	var id = res[0];
	var price = res[1];
//	alert(id);
	//alert(price);
//	var variable = '\'#'+elem.id+'\'';
	//alert(variable);
	//alert($("\"#"+elem.id+"_item\""));
	//$(variable);
//	alert(document.getElementById("2item"));
	//$(variable).remove();
    //alert(document.getElementById(elem.id).href);
  	$("#"+id+"_item").remove();
	returnBill(-price);
	
//returnBill(-(elem.value));
	
}

function orderPizzas()
{
//	alert($("#card_ul_id").children().length);
	//alert($("#card_ul_id").children[0]);
    var sumControl = 0;
    var pizzas = [];
    var sumOrder = [];
	$("#card_ul_id").children().each(
                  function(){
                      var str = $(this).attr('id');

 					var res = str.split("_");
 					var id = res[0];
 					var price = res[1];


 					sumOrder.push(price);
					pizzas.push(id);
	});

    var endSum = 0;
	var order = "Zamówiłeś pizze o id:";
	for(var i=0; i<pizzas.length; i++)
	{
		order += " " + pizzas[i];
	}

	for (var i = 0; i < sumOrder.length; i++) {
	    endSum += parseFloat(sumOrder[i]);
    }

	if (Math.round($("#cart_sum").html() * 100) / 100 != noCheatVar)
    {
        alert("jesteś oszutem Ty figlarzu Ty ;)");
	} else {
	    alert(order);
	}
	
	
}


function returnBill(price)
{
    //var sum = sum + price;
    var war = 0;
	var price_int = 0;;
	var war_int = 0;
	
	war = Math.round($("#cart_sum").html() * 100) / 100;
	//alert(war);
	
	price_int = parseFloat(price) || 0;
    war_int = parseFloat(war) || 0;
	
    noCheatVar += price_int;


	var sum = 0;
	sum = Math.round((price_int + war_int) * 100) / 100;
	//alert(price_int);
	//alert(war_int);
	
	//Math.round($("#cart_sum").html() * 100) / 100;
	
	$( "#cart_sum" ).empty();
    $( "#cart_sum" ).append(sum.toString());
}


function convertJSonToList(table_obj) {

    alert(table_obj);
    var Toppingsv = table_obj.Toppings;
    alert(Toppingsv[0]);

}


function LoadFakeData() {
   

    var Pizza_1 = '{ "Name" : "Margaritta", "Price" : "10.20",  "Toppings" : [' +
                        '{ "Id":"290"},' +
                        '{ "Id":"291"}' +
        ']}';

    var Pizza_2 = '{ "Name" : "Z szynką", "Price" : "13.20",  "Toppings" : [' +
                        '{ "Id":"296"},' +
                        '{ "Id":"293"}' +
        ']}';

    var jSonArray = [];
    jSonArray[0] = JSON.parse(Pizza_1);
    jSonArray[1] = JSON.parse(Pizza_2);

   // alert(jSonArray[0].Name);

    return jSonArray;

}