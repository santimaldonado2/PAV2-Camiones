$(function () {
   
    if ($('#ContentPlaceHolder1_Mensaje').text() != "") {
        
        $("#ContentPlaceHolder1_Mensaje").dialog({
            autoOpen: true
        });

    }
   




});

function openDialog() {
    null;
    //setTimeout(function () { $("#dialog").dialog("open"); }, 1);
}