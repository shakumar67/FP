
$(document).ready(function () {

    GetDataChild();

});

function GetDataChild() {
    
    $('#childddmf').html('');
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
        url: document.baseURI + "/Home/GetChildData",
        data: filtermodel,
        success: function (resp) {
            if (resp.IsSuccess) {
               // debugger
                var resdata = JSON.parse(resp.Data);
                var resD = resdata;
                var DatalistMale = [], DatalistFemale = [], DList=[],DListM = [],DListF=[];
                var Dobj = {};
                for (var i = 0; i < resD.length; i++) {
                    DatalistMale.push({ name: "0 Child", value: resD[i].MaleC0 });//shortName
                    DatalistMale.push({ name: "1 Child", value: resD[i].MaleC1 });
                    DatalistMale.push({ name: "2 Child", value: resD[i].MaleC2 });
                    DatalistMale.push({ name: "2+ Child", value: resD[i].MaleCmore });

                    DatalistFemale.push({ name: "0 Child", value: resD[i].FemaleC0 });
                    DatalistFemale.push({ name: "1 Child", value: resD[i].FemaleC1 });
                    DatalistFemale.push({ name: "2 Child", value: resD[i].FemaleC2 });
                    DatalistFemale.push({ name: "2+ Child", value: resD[i].FemaleCmore });
                }
                DList.push({ name: "Male Child", data: DatalistMale });
                DList.push({ name: "Female Child", data: DatalistFemale });
                //Dobj = [DListM, DListF];

                Highcharts.chart('childddmf', {
                    chart: {
                        type: 'packedbubble',
                        height: '100%'
                    },
                    title: {
                        text: 'No of male & female child at present',
                        align: 'center'
                    },
                    subtitle: {
                       //  text: 'Line-Listing',
                        align: 'left'
                    },
                    tooltip: {
                        useHTML: true,
                        pointFormat: '<b>{point.name}:</b> {point.value}<sub></sub>'
                    },
                    credits: {
                        enabled: false
                    },
                    //legend: {
                    //    layout: 'vertical',
                    //    align: 'right',
                    //    verticalAlign: 'middle'
                    //},
                    //plotOptions: {
                    //    packedbubble: {
                    //        minSize: '15%',
                    //        maxSize: '50%',
                    //       // zMin: 0,
                    //       // zMax: 1000,
                    //        layoutAlgorithm: {
                    //           // gravitationalConstant: 0.05,
                    //            splitSeries: true,
                    //           // seriesInteraction: false,
                    //            dragBetweenSeries: true,
                    //            parentNodeLimit: true,
                    //            initialPositionRadius: 100,
                    //            parentNodeOptions: {
                    //                bubblePadding: 20
                    //            }
                    //        },
                    //        dataLabels: {
                    //            enabled: true,
                    //            format: '{point.value}',
                    //            parentNodeFormat: '{point.series.name}',
                    //            filter: {
                    //                property: 'y',
                    //                operator: '>',
                    //                value: 0
                    //            },
                    //            style: {
                    //                color: 'black',
                    //                textOutline: 'none',
                    //                fontWeight: 'normal'
                    //            }
                    //        }
                    //    }
                    //},
                    plotOptions: {
                        packedbubble: {
                            minSize: '15%',
                            maxSize: '50%',
                            layoutAlgorithm: {
                                initialPositionRadius: 100,
                                splitSeries: true,
                                parentNodeLimit: true,
                                dragBetweenSeries: true,
                                parentNodeOptions: {
                                    bubblePadding: 20
                                }
                            },
                            dataLabels: {
                                enabled: true,
                                // format: '{point.shortName}',
                                parentNodeFormat: '{point.series.name}'
                            }
                        }
                    },
                    series: DList,
                    //showInLegend: true

                });




            }
            else {
                $('#childddmf').html('Data Not Found!!');
            }
        },
        error: function (req, error) {
            if (error === 'error') { error = req.statusText; }
            var errormsg = 'There was a communication error: ' + error;
            //Do To Message display
        }
    });
}