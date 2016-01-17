var allOrdersItems = [];

$(document).ready(function () {

    getAllOrders();
 
});

function goToNextEtap(elem) {

    if (elem.name == "InProgress") {
        putNewState("Done", elem.id);
    }
    else if(elem.name == "Created") {
        putNewState("InProgress", elem.id);
    }
}

function putNewState(newstate, id) {

    var order = {
        status: newstate
    };

    var orderJson = JSON.stringify(order);
  
    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/order/' + id,
        type: "PUT",
        data: orderJson,
        success: function () {
            location.reload();
        }
    });
}


function getAllOrders() {
  
    $.ajax({
        dataType: "json",
        contentType: "application/json",
        url: 'http://localhost:5413/api/order',
        type: 'GET',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                allOrdersItems[i] = data[i];
                var pizzas = "";
                var plState = "";
                var btnLbl = "";

                for (var j = 0; j < data[i].pizzas.length; j++) {
                    pizzas += data[i].pizzas[j].name + "</br>";
                }

                if (data[i].status == "InProgress") {
                    plState = "W trakcie realizacji";
                    btnLbl = "Zrealizowano";
                    $("#orders").append("<tr><th><font size=\"1\"> " + pizzas + "</font></br></th><th>" + data[i].address + "</th><th>" + data[i].price.toFixed(2) + "</th><th>" + plState + "</th>" +
                "<th> <button type=\"button\" name=\"" + data[i].status + "\" id=\"" + data[i].id + "\" onclick=\"goToNextEtap(this)\">" + btnLbl + "</th></tr>");
                }
                else if (data[i].status == "Created") {
                    plState = "Czeka na realizację";
                    btnLbl = "Realizuj";
                    $("#orders").append("<tr><th><font size=\"1\"> " + pizzas + "</font></br></th><th>" + data[i].address + "</th><th>" + data[i].price.toFixed(2) + "</th><th>" + plState + "</th>" +
                "<th> <button type=\"button\" name=\"" + data[i].status + "\" id=\"" + data[i].id + "\" onclick=\"goToNextEtap(this)\">" + btnLbl + "</th></tr>");
                }
                else if(data[i].status == "Done")
                {
                    plState = "Zrealizowano";
                    $("#orders").append("<tr><th><font size=\"1\"> " + pizzas + "</font></br></th><th>" + data[i].address + "</th><th>" + data[i].price.toFixed(2) + "</th><th>" + plState + "</th>");
                }
             }
        }
    });



}