function BFYDetailView(BFYId) { 
    $("#div-View").html(''); $("#div-View").css('color', '');
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
                    $("#div-View").html(res.Data);
                    $('#myModalview').modal('show');
                }
                else {
                    $("#div-View").html(res.Data);
                    $("#div-View").css('color', 'red');
                }
            },
            error: function (req, error) {
                if (error === 'error') { error = req.statusText; }
                var errormsg = 'There was a communication error: ' + error;
                //Do To Message display
                $("#div-View").html(errormsg);
                $("#div-View").css('color', 'red');
            }
        });
    }
    else {
        $("#div-View").html('Record Not found!!');
        $("#div-View").css('color', 'red');
    }
}