
$(document).ready(function () {

    GetDataModule();

});

function GetDataModule() {

    $('#moduleroll').html('');
    var filtermodel = new Object();
    filtermodel.DistrictId = $('#DistrictId').val() == '' ? '' : $('#DistrictId').val();
    filtermodel.BlockId = $('#BlockId').val() == '' ? '' : $('#BlockId').val();
    filtermodel.PanchayatId = $('#PanchayatId').val() == '' ? '' : $('#PanchayatId').val();
    filtermodel.VoId = $('#VOId').val() == '' ? '' : $('#VOId').val();
    // filtermodel.Year = $('#Year option:selected').text() == '' ? '' : $('#Year option:selected').text();
    filtermodel.Year = $('#Year').val() == '' ? '' : $('#Year').val();
    filtermodel.Month = $('#Month').val() == '' ? '' : $('#Month').val();
    var formData = $('#formid').serialize();

    $.ajax({
        type: "GET",
        url: document.baseURI + "/Home/GetModuleData",
        data: filtermodel,
        success: function (resp) {
            if (resp.IsSuccess) {
                debugger
                var resdata = JSON.parse(resp.Data);
                var resD = resdata;
                var DataList = [], DistName = [], DistN = [];
                var Datav1 = [], Datav2 = [], Datav3 = [], Datav4 = [];
                var Dobj = {};
                for (var i = 0; i < resD.length; i++) {
                    var isdist = DistN.filter(x => x.name == resD[i].DistrictName);
                    if (isdist == 0) {
                        DistN.push({ name: resD[i].DistrictName });
                        DistName.push(resD[i].DistrictName);

                    }

                    for (let key in resD[i]) {

                        if (key != 'DistrictName' && key != 'DistrictId_fk') {
                            console.log(key, resD[i][key]);
                            var index = DataList.findIndex(x => x.name == key);
                            if (index == -1) {
                                DataList.push({ name: key, data: [] })
                                index = DataList.length - 1;
                            }
                            DataList[index].data.push(resD[i][key] || 0);

                        }
                    }

                    //if (resD[i].FlipChart && resD[i].FlipChart != undefined) {
                    //    Datav1.push(resD[i].FlipChart);
                    //}
                    //else {
                    //    Datav1.push(0);
                    //}
                    //if (resD[i].Leaflet && resD[i].Leaflet != undefined) {
                    //    Datav2.push(resD[i].Leaflet);
                    //}
                    //else {
                    //    Datav2.push(0);
                    //}
                    //if (resD[i].Video && resD[i].Video != undefined) {
                    //    Datav3.push(resD[i].Video);
                    //}
                    //else {
                    //    Datav3.push(0);
                    //}
                    //if (resD[i].Games && resD[i].Games != undefined) {
                    //    Datav4.push(resD[i].Games);
                    //}
                    //else {
                    //    Datav4.push(0);
                    //}
                }
                //DataList.push({ name: "Flip Chart", data: Datav1 });
                //DataList.push({ name: "Leaflet", data: Datav2 });
                //DataList.push({ name: "Video", data: Datav3 });
                //DataList.push({ name: "Games", data: Datav4 });
                console.log(DataList);
                Highcharts.chart('moduleroll', {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: 'Medium of module rollout'
                    },
                    xAxis: {
                        categories: DistName
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Goals'
                        }
                    },
                    credits: {
                        enabled: false
                    },
                    legend: {
                        reversed: true
                    },
                    plotOptions: {
                        series: {
                            stacking: 'normal',
                            dataLabels: {
                                enabled: true
                            }
                        }
                    },
                    series: DataList
                    //    [{
                    //    name: 'Cristiano Ronaldo',
                    //    data: [3, 4, 6, 15, 12]
                    //}, {
                    //    name: 'Lionel Messi',
                    //    data: [5, 3, 12, 6, 11]
                    //}, {
                    //    name: 'Robert Lewandowski',
                    //    data: [5, 15, 8, 5, 8]
                    //}]
                });






            }
            else {
                $('#moduleroll').html('Data Not Found!!');
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
}