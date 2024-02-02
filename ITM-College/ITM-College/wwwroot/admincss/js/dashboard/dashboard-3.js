(function($) {
    /* "use strict" */

var dzChartlist = function(){
	
	var screenWidth = $(window).width();
	
	var sparkBar2 = function(){
		if(jQuery('#spark-bar-2').length > 0 ){	
			$("#spark-bar-2").sparkline([33, 22, 68, 54, 8, 30, 74, 7, 36, 5, 41, 19, 43, 29, 38], {
				type: "bar",
				height: "140",
				width: 100,
				barWidth: 10,
				barSpacing: 10,
				barColor: "rgb(200, 255, 135)"
			});
		}	
	}	
	var sparkLineChart = function(){
		if(jQuery('#sparkline12').length > 0 ){
			//Pie
			$("#sparkline12").sparkline([24, 61, 51], {
				type: "pie",
				height: "100",
				resize: !0,
				sliceColors: ["#8d95ff", "#d7daff", "#c7cbff"]
			});
			
			$(".bar1").peity("bar", {
				fill: ["rgb(216, 196, 255)", "rgb(216, 196, 255)", "rgb(216, 196, 255)"],  
				width: "100%",
				height: "140"
			});
			
			$(".peity-line-2").peity("line", {
				fill: "#ff3232", 
				stroke: "#fac2c2", 
				width: "100%",
				strokeWidth: "3",
				height: "150"
			});
		}
	}



	var barChart = function(){
		if(jQuery('#barChart_2').length > 0 ){
		//gradient bar chart
			const barChart_2 = document.getElementById("barChart_2").getContext('2d');
			//generate gradient
			const barChart_2gradientStroke = barChart_2.createLinearGradient(0, 0, 0, 250);
			barChart_2gradientStroke.addColorStop(0, "rgba(141, 149, 255, 1)");
			barChart_2gradientStroke.addColorStop(1, "rgba(102, 115, 253, 1)");

			// Get the reference to the existing chart with ID 'barChart_2'
			var existingChart = Chart.getChart(barChart_2.canvas.id);

			// Check if the chart exists
			if (existingChart) {
			  // Destroy the existing chart
			  existingChart.destroy();
			}

			barChart_2.height = 100;

			var config = {
				type: 'bar',
				data: {
					defaultFontFamily: 'Poppins',
					labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul"],
					datasets: [
						{
							label: "My First dataset",
							data: [65, 59, 80, 81, 56, 55, 40],
							borderColor: barChart_2gradientStroke,
							borderWidth: "0",
							barPercentage:.5,
							backgroundColor: barChart_2gradientStroke, 
							hoverBackgroundColor: barChart_2gradientStroke
						}
					]
				},
				options: {
					plugins:{
						legend: false,
					},
					scales: {
						y : {
							ticks: {
								beginAtZero: true
							},
						},
						x : {
							// Change here
							barPercentage: 0.5
						}
					}
				}
			};

			var barChart = new Chart(barChart_2, config);

			var element = document.querySelector('body');
			var observer = new MutationObserver(function(mutations) {
				mutations.forEach(function(mutation) {
					if(mutation.attributeName === "data-theme-version"){
						if($('body').attr('data-theme-version') === "dark"){
							config.options.scales.y.grid.color = '#3d3d4e'
							config.options.scales.x.grid.color = '#3d3d4e'
						}else{
							config.options.scales.y.grid.color = '#eee'
							config.options.scales.x.grid.color = '#eee'
						}
						barChart.update();
					}
				});
			});
			observer.observe(element, {
				attributes: true
			});
		}
	}
	var areaChart = function(){
		if(jQuery('#areaChart_1').length > 0 ){
			const areaChart_1 = document.getElementById("areaChart_1").getContext('2d');
			
			// Get the reference to the existing chart with ID 'areaChart_1'
			var existingChart = Chart.getChart(areaChart_1.canvas.id);

			// Check if the chart exists
			if (existingChart) {
			  // Destroy the existing chart
			  existingChart.destroy();
			}

			areaChart_1.height = 100;

			var config = {
				type: 'line',
				data: {
					defaultFontFamily: 'Poppins',
					labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul"],
					datasets: [
						{
							label: "My First dataset",
							data: [25, 20, 60, 41, 66, 45, 80],
							borderColor: 'rgba(102, 115, 253, 1)',
							borderWidth: "3",
							backgroundColor: 'rgba(102, 115, 253, .2)', 
							pointBackgroundColor: 'rgba(102, 115, 253, 1)',
							tension : .5,
							fill :true,
						}
					]
				},
				options: { 
					plugins:{
						legend: false,
					},
					scales: {
						y : {
							max: 100, 
							min: 0, 
							ticks: {
								beginAtZero: true, 
								stepSize: 20, 
								padding: 10
							}
						},
						x : { 
							ticks: {
								padding: 5
							}
						}
					}
				}
			};

			var lineChart = new Chart(areaChart_1, config);

			var element = document.querySelector('body');
			var observer = new MutationObserver(function(mutations) {
				mutations.forEach(function(mutation) {
					if(mutation.attributeName === "data-theme-version"){
						if($('body').attr('data-theme-version') === "dark"){
							config.options.scales.y.grid.color = '#3d3d4e'
							config.options.scales.x.grid.color = '#3d3d4e'
						}else{
							config.options.scales.y.grid.color = '#eee'
							config.options.scales.x.grid.color = '#eee'
						}
						lineChart.update();
					}
				});
			});
			observer.observe(element, {
				attributes: true
			});
		}
	}

		
	
	/* Function ============ */
		return {
			init:function(){
				sparkBar2();
				sparkLineChart();
				barChart();
				areaChart();
			},
			
			
			load:function(){
				sparkBar2();
				sparkLineChart();
				barChart();
				areaChart();
			},
			
			resize:function(){
				sparkBar2();
				sparkLineChart();
				barChart();
				areaChart();
			}
		}
	
	}();

		
	jQuery(window).on('load',function(){
		dzChartlist.load();
	});
 
})(jQuery);