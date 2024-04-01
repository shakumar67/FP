function ServiceContraceptionChart(DataList) {
    if (DataList) {
        var obj = new Object; var Dlist = [];
        for (var i = 0; i < DataList.length; i++) {
            obj = [DataList[i].ColumnName, DataList[i].NoofData];
            Dlist.push(obj);
        }
        console.log(Dlist);
        // Set up the chart
        Highcharts.chart('servicecontraceptionchart', {
            chart: {
                type: 'pyramid3d',
                options3d: {
                    enabled: true,
                    alpha: 10,
                    depth: 50,
                    viewDistance: 50
                }
            },
            title: {
                text: 'Service Contraception - Adopted'
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                series: {
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b> ({point.y:,.0f})',
                        allowOverlap: true,
                        x: 10,
                        y: -5
                    },
                    width: '60%',
                    height: '80%',
                    center: ['50%', '45%']
                }
            },
            series: [{
                name: 'Service Adopted',
                data: Dlist,
                point: {
                    events: {
                        click: function (event) {
                            //alert(this.name);
                            ServiceUseMethodChart(resDataUseMlist, this.name);
                        }
                    }
                },
            }]
        });
    }
}

function ServiceUseMethodChart(DataList, param) {
    if (param != '' && param != undefined) {
        DataList = DataList.filter(x => x.Contraceptive_Name == param);
    }
    if (DataList) {
        var obj = new Object; var Dlist = [];
        for (var i = 0; i < DataList.length; i++) {
            obj = [DataList[i].Use_MethodName, DataList[i].NoofData];
            Dlist.push(obj);
        }

        Highcharts.chart('serviceusemethodchart', {
            chart: {
                type: 'columnpyramid'
            },
            title: {
                text: 'Service Contraception - Adopted '
            },
            subtitle: {
                text: 'Use Method'
            },
            credits: {
                enabled: false
            },
            colors: ['#C79D6D', '#B5927B', '#CE9B84', '#B7A58C', '#C7A58C'],
            xAxis: {
                crosshair: true,
                labels: {
                    style: {
                        fontSize: '14px'
                    }
                },
                type: 'category'
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'No Of Service - Adopted'
                }
            },

            tooltip: {
                valueSuffix: ' '
            },
            series: [{
                name: 'Use Method',
                colorByPoint: true,
                data: Dlist,
                showInLegend: false,
                
            }]
        });

    }
}

