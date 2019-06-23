google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    $("body").on("click", "#addGraph", function () {

        var json = {
            Start: $('#start').val(),
            Finish: $('#end').val()
        }
        $.ajax({
        url: "/Manager/GetStatistic",
        dataType: "json",
        data: { jDiagram: JSON.stringify(json) } ,
        success: function (jsonData) {
            var data = new google.visualization.DataTable();
            data.addColumn('date', 'date');
            data.addColumn('number', 'count');

            for (var i = 0; i < jsonData.length; i++) {
                var dateStr = jsonData[i].date.substr(0, 4) + "-" + jsonData[i].date.substr(5, 2) + "-" + jsonData[i].date.substr(8, 2);
                data.addRow([ new Date(dateStr),parseInt(jsonData[i].count)]);
            }

            var options = {
                title: 'Статистика количества подачи заявок',
                curveType: 'function',
                lineWidth: 4,
                intervals: { 'style': 'line' },
                legend: 'none'
            };
            var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        }
        });
    })
}