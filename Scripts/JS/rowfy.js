/**
* Dynamically add/remove table row using jquery
*
* @author Risul Islam risul321@gmail.com
**/

/*Add row event*/
$(document).on('click', '.rowfy-addrow', function () {
    let rowfyable = $(this).closest('table');
    let lastRow = $('tbody tr:last', rowfyable).clone(true).off();
    var index = parseInt($('.index', lastRow).text()) + 1;
    var dataId = $(lastRow).attr('data-id');

    if (dataId != "" && dataId != "00000000-0000-0000-0000-000000000000") {
        $('button', $('tbody tr:last', rowfyable)).remove();
    }

    $(lastRow).attr('data-index', index);
    $(lastRow).attr('data-id', '');
    $('input', lastRow).val('');


    $('.index', lastRow).text(index);

    $('input.mdt', lastRow).attr('id', 'mdt-' + index);
    $('select', lastRow).attr('id', 'void-' + index);
    $('input.noofpart', lastRow).attr('id', 'noofpart-' + index);

    $('input', lastRow).removeClass('hasDatepicker');

    $('#mdt-' + index, lastRow).datepicker({
        dateFormat: 'dd-mm-yy',
        maxDate: '0',
        //maxDate: "+1M +10D",
        changeMonth: true,
        changeYear: true,
    });

    $('tbody', rowfyable).append(lastRow);
    $(this).removeClass('rowfy-addrow btn-success').addClass('rowfy-deleterow btn-danger').text('-');

});


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
$('.rowfy').each(function () {
    $('tbody', this).find('tr td:last-child').each(function (i, row) {
        $('.index', row).text(i + 1);
        var dataId = $(row).attr('data-id');
        if (dataId == "" || dataId == "00000000-0000-0000-0000-000000000000") {
            $(this).append('<button type="button" class="btn btn-sm '
                + ($(this).is(":last-child") ?
                    'rowfy-addrow btn-success">+' :
                    'rowfy-deleterow btn-danger">-')
                + '</button>');
        } else {
            if (i > 0) {
                //$(this).append('<td><button type="button" class="btn btn-sm '
                //    + ($(this).is(":last-child") ?
                //        'rowfy-addrow btn-success">+' :
                //        'rowfy-deleterow btn-danger">-')
                //    + '</button></td>');
            } else {
                $(this).append('<button type="button" class="btn btn-sm '
                    + ($(this).is(":last-child") ?
                        'rowfy-addrow btn-success">+' :
                        'rowfy-deleterow btn-danger">-')
                    + '</button>');
            }
        }

    });
});

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
                var row = $(".rowfy tbody tr:last").clone(true).off();
                $(".rowfy tbody tr").remove();
                //$('table#tbl > tbody > tr').not(':last').remove();
                
                $(resdata).each(function (index, item) {
                    index = index + 1;
                    $(row).attr('data-index', index);
                    $(row).attr('data-id', item.AchieveId_pk);
                    $('.index', row).text(index);

                    $('input.mdt', row).attr('id', 'mdt-' + index);
                    $('select', row).attr('id', 'void-' + index);
                    $('input.noofpart', row).attr('id', 'noofpart-' + index);

                    $('select[id=void-' + index + ']', row).val(item.VoId_fk);
                    $('input[id=mdt-' + index + ']', row).val(moment(item.Meetingheld).format("DD-MM-YYYY"));
                    $('input[id=noofpart-' + index + ']', row).val(item.Noofparticipant);

                    $(".rowfy tbody").append(row);

                    $('input', row).removeClass('hasDatepicker');
                    $('#mdt-' + index, row).datepicker({
                        dateFormat: 'dd-mm-yy',
                        maxDate: '0',
                        changeMonth: true,
                        changeYear: true,
                    });

                    if (resdata.length == index) {
                        $('td.action', row).append('<button type="button" class="btn btn-sm rowfy-addrow btn-success">+</button>');
                        //$('button', row).removeClass('rowfy-addrow btn-success').addClass('rowfy-deleterow btn-danger').text('-');
                    } else {
                        $('button', row).remove();
                    }

                    row = row.clone(true).off();

                });

            }
            else {
                $("#msg").html(data.res);
                var row = $(".rowfy tbody tr:last").clone(true).off();
                $(".rowfy tbody tr").remove();
                var index = 1;
                $(row).attr('data-index', index);
                $(row).attr('data-id', '');
                $('.index', row).text(index);

                $('input.mdt', row).attr('id', 'mdt-' + index);
                $('select', row).attr('id', 'void-' + index);
                $('input.noofpart', row).attr('id', 'noofpart-' + index);

                $('select[id=void-' + index + ']', row).val('');
                $('input[id=mdt-' + index + ']', row).val('');
                $('input[id=noofpart-' + index + ']', row).val('');

                $(".rowfy tbody").append(row);
                //$('table#tbl > tbody > tr').not(':last').remove();
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
