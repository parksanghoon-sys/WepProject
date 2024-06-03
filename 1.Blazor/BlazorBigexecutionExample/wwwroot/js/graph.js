function setGrowthGraph(data) {
    var dt = new google.visualization.DataTable();
    dt.addColumn('string', 'Date');
    dt.addColumn('number', 'Temp. (C)');
    dt.addColumn('number', 'Temp. (F)');

    dt.addRows([
        ...data
    ]);

    // pointVisible, pointSize로 라인 차트에 동그라미 표시합니다.
    // Set chart options
    var options = {
        'title': '',
        pointsVisible: true,
        pointSize: '11px',
        legend: 'none',
        colors: ['#F2994A', '#5DB682']
    };

    var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
    chart.draw(dt, options);
}

export function drawGrowthGraph(data) {
    google.charts.setOnLoadCallback(setGrowthGraph(data))
}