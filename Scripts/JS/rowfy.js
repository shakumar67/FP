/**
* Dynamically add/remove table row using jquery
*
* @author Risul Islam risul321@gmail.com
**/

/*Add row event*/
$(document).on('click', '.rowfy-addrow', function () {
    var tbl = $(this).closest('table');
    addRow(tbl);

});

function addRow(tbl) {
    let rowfyable = tbl;
    let lastRow = $('tfoot tr:last', rowfyable).clone(true).off();
    var index = parseInt($('tbody tr', rowfyable).length) + 1;
    if (index <= 10) {
        $(lastRow).attr('data-index', index);
        $(lastRow).attr('data-id', '');
        $('input', lastRow).val('');

        $('.index', lastRow).text(index);

        $('input.mdt', lastRow).attr('id', 'mdt-' + index);
        $('select.ActivityId_fk', lastRow).attr('id', 'actid-' + index);
        $('select.Void_fk', lastRow).attr('id', 'void-' + index);
        $('input.noofpart', lastRow).attr('id', 'noofpart-' + index);

        $('tbody', rowfyable).append(lastRow);
        $('tbody tr button', rowfyable).hide();
        if (index > 1) {
            $('tbody tr:not(:last) button.rowfy-deleterow', rowfyable).show();
        }
        $('tbody tr:last button.rowfy-addrow', rowfyable).show();
    } else {
        toastr.error("Error", "You can not add more than 10 row per month.");
        return false;
    }
}

addRow($("#tbl"));

/*Delete row event*/
$(document).on('click', '.rowfy-deleterow', function () {
    $(this).closest('tr').remove();
    $('.rowfy').each(function () {
        $('tbody', this).find('tr').each(function (i, row) {
            $('.index', row).text(i + 1);
        });
    });
});

/*Initialize all rowfy tables*/
//$('.rowfy').each(function () {
//    $('tbody', this).find('tr td:last-child').each(function (i, row) {
//        $('.index', row).text(i + 1);
//        var dataId = $(row).attr('data-id');
//        if (dataId == "" || dataId == "00000000-0000-0000-0000-000000000000") {
//            $(this).append('<button type="button" class="btn btn-sm '
//                + ($(this).is(":last-child") ?
//                    'rowfy-addrow btn-success">+' :
//                    'rowfy-deleterow btn-danger">-')
//                + '</button>');
//        } else {
//            if (i > 0) {
//                //$(this).append('<td><button type="button" class="btn btn-sm '
//                //    + ($(this).is(":last-child") ?
//                //        'rowfy-addrow btn-success">+' :
//                //        'rowfy-deleterow btn-danger">-')
//                //    + '</button></td>');
//            } else {
//                $(this).append('<button type="button" class="btn btn-sm '
//                    + ($(this).is(":last-child") ?
//                        'rowfy-addrow btn-success">+' :
//                        'rowfy-deleterow btn-danger">-')
//                    + '</button>');
//            }
//        }

//    });
//});

function BindDataTable() {
    $("#msg").html(''); //$('table#tbl > tbody > tr').not(':last').remove();
    var AchvPlanModel = new Object();
    AchvPlanModel.DistrictId_fk = $('#DistrictId_fk').val() == '' ? '' : $('#DistrictId_fk').val();
    AchvPlanModel.BlockId_fk = $('#BlockId_fk').val() == '' ? '' : $('#BlockId_fk').val();
    AchvPlanModel.ClusterId_fk = $('#ClusterId_fk').val() == '' ? '' : $('#ClusterId_fk').val();
    AchvPlanModel.PanchayatId_fk = $('#PanchayatId_fk').val() == '' ? '' : $('#PanchayatId_fk').val();
    //AchvPlanModel.VoId = $('#VOId').val() == '' ? '' : $('#VOId').val();
    AchvPlanModel.PlanYear = $('#PlanYear').val() == '' ? '' : $('#PlanYear').val();
    AchvPlanModel.PlanMonth = $('#PlanMonth').val() == '' ? '' : $('#PlanMonth').val();
    var formData = $('#formid').serialize();

    $.ajax({
        type: "GET",
        url: document.baseURI + "/Achievement/GetEditAchPlanList",
        data: AchvPlanModel,//JSON.stringify({ 'Roles': '' }),
        //cache: false,
        success: function (data) {
            if (data.IsSuccess) {
                // $("#subdata").html(res.Data);
                var resdata = JSON.parse(data.res);
                var row = $(".rowfy tfoot tr:last").clone(true).off();
                $(".rowfy tbody tr").remove();
                var rowfyable = $("#tbl");
                //$('table#tbl > tbody > tr').not(':last').remove();

                $(resdata).each(function (index, item) {
                    index = index + 1;
                    $(row).attr('data-index', index);
                    $(row).attr('data-id', item.AchieveId_pk);
                    $('.index', row).text(index);

                    $('input.mdt', row).attr('id', 'mdt-' + index);
                    $('select.ActivityId_fk', row).attr('id', 'actid-' + index);
                    $('select.Void_fk', row).attr('id', 'void-' + index);
                    $('input.noofpart', row).attr('id', 'noofpart-' + index);

                    $('select[id=actid-' + index + ']', row).val(item.ActivityId_fk);
                    $('select[id=void-' + index + ']', row).val(item.VoId_fk);
                    $('input[id=mdt-' + index + ']', row).val(moment(item.Meetingheld).format("DD/MM/YYYY"));
                    $('input[id=noofpart-' + index + ']', row).val(item.Noofparticipant);

                    //$('input', row).removeClass('hasDatepicker');
                    //$('#mdt-' + index, row).datepicker({
                    //    dateFormat: 'dd-mm-yy',
                    //    maxDate: '0',
                    //    changeMonth: true,
                    //    changeYear: true,
                    //});

                    $(".rowfy tbody").append(row);
                    //if (resdata.length == index) {
                    //    $('td.action', row).append('<button type="button" class="btn btn-sm rowfy-addrow btn-success">+</button>');
                    //    //$('button', row).removeClass('rowfy-addrow btn-success').addClass('rowfy-deleterow btn-danger').text('-');
                    //} else {
                    //    $('button', row).remove();
                    //}

                    $('tbody tr button', rowfyable).hide();
                    if (index > 1) {
                        $('tbody tr:not(:last) button.rowfy-deleterow', rowfyable).show();
                    }
                    $('tbody tr:last button.rowfy-addrow', rowfyable).show();

                    row = row.clone(true).off();

                });

            }
            else {
                $("#msg").html(data.res);
                var row = $(".rowfy tfoot tr:last").clone(true).off();
                $(".rowfy tbody tr").remove();
                var index = 1;
                $(row).attr('data-index', index);
                $(row).attr('data-id', '');
                $('.index', row).text(index);

                $('input.mdt', row).attr('id', 'mdt-' + index);
                $('select.ActivityId_fk', row).attr('id', 'actid-' + index);
                $('select.Void_fk', row).attr('id', 'void-' + index);
                $('input.noofpart', row).attr('id', 'noofpart-' + index);

                $('select[id=actid-' + index + ']', row).val('');
                $('select[id=void-' + index + ']', row).val('');
                $('input[id=mdt-' + index + ']', row).val('');
                $('input[id=noofpart-' + index + ']', row).val('');

                $(".rowfy tbody").append(row);
                //$('table#tbl > tbody > tr').not(':last').remove();

                var rowfyable = $("#tbl");
                $('tbody tr button', rowfyable).hide();
                if (index > 1) {
                    $('tbody tr:not(:last) button.rowfy-deleterow', rowfyable).show();
                }
                $('tbody tr:last button.rowfy-addrow', rowfyable).show();
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
            $("#msg").html(errormsg);
        }
    });
}
