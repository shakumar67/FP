function BFYDetailView(BFYId) { 
    $("#data-modelview").html(''); $("#data-modelview").css('color', '');
    var filtermodel = new Object();
    filtermodel.BFYId = BFYId;
    debugger;
    if (BFYId != '' && BFYId != undefined) {
        $.ajax({
            type: "GET",
            url: document.baseURI + "/Beneficiary/GetBFYDetailView",
            data: filtermodel,
            //cache: false,
            success: function (res) {
                if (res.IsSuccess) {
                    $("#data-modelview").html(res.Data);
                    $('#myModalviewBFYFollow').modal('show');
                }
                else {
                    $("#data-modelview").html(res.Data);
                    $("#data-modelview").css('color', 'red');
                }
            },
            error: function (req, error) {
                if (error === 'error') { error = req.statusText; }
                var errormsg = 'There was a communication error: ' + error;
                //Do To Message display
                $("#data-modelview").html(errormsg);
                $("#data-modelview").css('color', 'red');
            }
        });
    }
    else {
        $("#div-View").html('Record Not found!!');
        $("#div-View").css('color', 'red');
    }
}
