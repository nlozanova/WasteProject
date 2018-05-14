//This refreshes the partial view with wastes
function refreshWastes() {
    //alert("Dropdown value changed!");
    debugger;
    $.ajax({
        type: 'GET',
        url: "/History/WastesPartial",
        cache: false,
        data: {
            storageId: $(".startStorage").val()
        },
        success: function (data) {
            debugger;
            $("#wastesPartial").html(data);
            
        },
        error: function () {

        }
    });
    //function success(result) {
    //    $("#wastesPartial").html(result);
    //}
}

//changing the dropdown value with start storages
$(".startStorage").change(refreshWastes);

$(".startStorage").show(refreshWastes);