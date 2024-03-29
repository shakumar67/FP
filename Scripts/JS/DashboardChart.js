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
                data: Dlist
            }]
        });
    }
}