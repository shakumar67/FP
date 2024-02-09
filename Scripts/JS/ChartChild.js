
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
                var Datalist = [], Datasum = [], DList=[],DListM = [],DListF=[];
                var Dobj = {};
                for (var i = 0; i < resD.length; i++) {
                   Datalist.push({ name: "P-0", value: resD[i].P0 });//shortName
                   Datalist.push({ name: "P-1", value: resD[i].P1 });
                   Datalist.push({ name: "P-2", value: resD[i].P2 });
                   Datalist.push({ name: "P-2+", value: resD[i].PPlus });
                   Datasum += resD[i].TotalPlus;
                  
                }
                DList.push({ name: "Eligible Couples", data: Datalist });
               // DList.push({ name: "Female Child", data: DatalistFemale });
                //Dobj = [DListM, DListF];

                Highcharts.chart('childddmf', {
                    chart: {
                        type: 'packedbubble',
                        height: '100%'
                    },
                    title: {
                        text: 'Eligible Couples (Parity - 0/1/2/2+)',
                        align: 'center'
                    },
                    subtitle: {
                        text: '(0 Child, 1 Child, 2 Child, 2+ Child) ' + '<b> Total : '+ Datasum+'</b>',
                        align: 'center'
                    },
                    tooltip: {
                        useHTML: true,
                        pointFormat: '<b>{point.name}: </b> {point.value}<sub></sub>'
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
                                parentNodeFormat: '{point.series.name}',
                                style: {
                                    color: 'black',
                                    textOutline: 'none',
                                    fontWeight: 'normal',
                                    fontSize: '18px'
                                }

                            },

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